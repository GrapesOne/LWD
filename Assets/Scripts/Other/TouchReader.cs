using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameZale.World
{
    public class TouchReader : MonoBehaviour
    {
        private InputData inputData;

        private Vector3
            temp,
            start,
            move;

        private readonly float sqrtScreenSize = Mathf.Sqrt(Screen.width * Screen.height);
        private bool lr;
        private int frameCountOfTap;
#if UNITY_EDITOR
        private readonly Vector3 tripledScreenCenter= new Vector3(Screen.width*3 / 2, Screen.height*3 / 2);
#endif
        public void Update()
        {

#if UNITY_EDITOR
            
            if (!CustomStandaloneInputModule.InputModule().IsPointerOverGameObject<GraphicRaycaster>())
            {
                if (Input.GetMouseButtonDown(0)) OnPointerDown();
                if (Input.GetMouseButtonUp(0)) OnPointerUp();
                if (Input.GetMouseButton(0)) OnDrag();
            } 
#else     
            //if (Input.touchCount == 0) return;

             //if (CustomStandaloneInputModule.InputModule().
             //IsPointerOverGameObject<GraphicRaycaster>(Input.touches[0].fingerId)) return;

            if (!EventSystem.current.currentSelectedGameObject)
            {
                //if (EventSystem.current.IsPointerOverGameObject()) return;
                if (Input.touches[Input.touchCount - 1].phase == TouchPhase.Began) OnPointerDown();
                if (Input.touches[Input.touchCount - 1].phase == TouchPhase.Moved) OnDrag();
                if (Input.touches[Input.touchCount - 1].phase == TouchPhase.Ended) OnPointerUp();
            }
#endif
        }
        private void OnPointerUp()
        {
#if UNITY_EDITOR
            inputData.UpPosition = Input.mousePosition;
#else
            inputData.UpPosition = Input.touches[Input.touchCount-1].position;
#endif
            if(frameCountOfTap<10) Click?.Invoke(inputData);
            Up?.Invoke(inputData);
            inputData.wasDrag = false;
            inputData.DownMagnitude = 0;
            inputData.UpdateMagnitude = 0;
            inputData.SwipeSize.Set(0,0,0);
            inputData.UpPosition.Set(0,0,0);
            inputData.DownPosition.Set(0,0,0);
            inputData.UpdatePosition.Set(0,0,0);
        }


        private void OnPointerDown()
        {
#if UNITY_EDITOR
            start = Input.mousePosition;
            temp.Set(tripledScreenCenter.x - 2 * start.x, 0, tripledScreenCenter.y - 2 * start.y);
#else
            inputData.wasDrag = false;
            start = Input.touches[Input.touchCount-1].position;
            if(Input.touchCount == 2)
            {
                lr = Input.touches[0].position.x < Input.touches[1].position.x;
                if (lr)
                    temp.Set( Input.touches[1].position.x - Input.touches[0].position.x,0,
                        Input.touches[1].position.y - Input.touches[0].position.y);
                else
                    temp.Set( Input.touches[0].position.x - Input.touches[1].position.x,0,
                        Input.touches[0].position.y - Input.touches[1].position.y);
            }
#endif
            inputData.DownMagnitude = Vector3.Magnitude(temp);
            inputData.DownPosition.Set(temp.x, 0, temp.z);
            frameCountOfTap = 0;
            Down?.Invoke(inputData);
        }

        
        private void OnDrag()
        {
#if UNITY_EDITOR
            move = Input.mousePosition;
            if (Input.GetKey(KeyCode.Space))
            {
                temp.Set(tripledScreenCenter.x - 2 * move.x, 0, tripledScreenCenter.y - 2 * move.y);
#else
            inputData.wasDrag = true;
            move = Input.touches[Input.touchCount-1].position;
            if (Input.touchCount == 2){
               if (lr)
                    temp.Set( Input.touches[1].position.x - Input.touches[0].position.x,0,
                        Input.touches[1].position.y - Input.touches[0].position.y);
                else
                    temp.Set( Input.touches[0].position.x - Input.touches[1].position.x,0,
                        Input.touches[0].position.y - Input.touches[1].position.y);
#endif
                inputData.UpdatePosition.Set(temp.x, 0, temp.z);
                inputData.UpdateMagnitude =
                    (inputData.DownMagnitude - Vector3.Magnitude(inputData.UpdatePosition)) / sqrtScreenSize;
            }
            else
            {
                inputData.SwipeSize.Set((move.x - start.x) / Screen.height,0,(move.y - start.y) / Screen.height);
            }

            frameCountOfTap++;
            Drag?.Invoke(inputData);
        }
        private bool IsUI()
        {
            foreach (Touch touch in Input.touches)
            {
                int id = touch.fingerId;
                if (EventSystem.current.IsPointerOverGameObject(id))
                {
                    return true;
                }
            }
            return false;
        }
    public delegate void TouchHandle(InputData inputData);
        public static event TouchHandle Down, Drag, Up, Click;
    }

    
    public struct InputData
    {
        public Vector3
            DownPosition,
            UpdatePosition,
            UpPosition,
            SwipeSize;

        public float
            DownMagnitude,
            UpdateMagnitude;
        public bool wasDrag;
        
    }
}
