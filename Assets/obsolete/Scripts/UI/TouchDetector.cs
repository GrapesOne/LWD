using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDetector : MonoBehaviour {
    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered

    public GameObject canvas;
    public bool ch = false;

    private Vector3 stp;
    private Vector3 flp;

    private bool boxColider;

    

 
    void Start()
    {
        dragDistance = Screen.height * 15 / 100; //dragDistance is 15% height of the screen
    }
 
    void Update()
    {
        
        Debug.Log(canvas.GetComponent<RectTransform>().sizeDelta.y);
 
        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                stp =  Camera.main.ScreenToWorldPoint(fp);                          
                 lp = touch.position;
                flp =   Camera.main.ScreenToWorldPoint(lp);         
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;

                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {//It's a drag
                 //check if the drag is vertical or horizontal
                    if (Mathf.Abs(lp.x - fp.x) < Mathf.Abs(lp.y - fp.y))
                    {   
                       // Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        //canvas.GetComponent<RectTransform>().position. = Input.mousePosition.x;
                        canvas.GetComponent<RectTransform>().position = new Vector3(0, flp.y - 18f + flp.y - stp.y, 0);
                    /*     if (lp.y > fp.y)  //If the movement was up
                        {   //Up swipe
                            Debug.Log("Up Swipe");
                            
                        }
                        else
                        {   //Down swipe
                            Debug.Log("Down Swipe");
                        } */
                    }
                }
                else
                {   //It's a tap as the drag distance is less than 20% of the screen height
                    Debug.Log("Tap");
                }
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position;  //last touch position. Ommitted if you use list
 
                //Check if drag distance is greater than 20% of the screen height
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {//It's a drag
                 //check if the drag is vertical or horizontal
                    if (Mathf.Abs(lp.x - fp.x) < Mathf.Abs(lp.y - fp.y))
                    {   
                         if (lp.y > fp.y)  //If the movement was up
                        {   //Up swipe
                            Debug.Log("Up Swipe");
                            
                        }
                        else
                        {   //Down swipe
                            Debug.Log("Down Swipe");
                        }
                    }
                }
                else
                {   //It's a tap as the drag distance is less than 20% of the screen height
                    Debug.Log("Tap");
                }
            }
        }
    }
}
