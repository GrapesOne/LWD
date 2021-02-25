using UnityEngine;

namespace Assets.obsolete.Scripts.UI
{
	public class Button : MonoBehaviour {
	   
		private Vector3 vector;
	   
		void Start(){
			vector =  gameObject.GetComponent<RectTransform>().localScale;
		}

		void OnMouseDown()
		{
			gameObject.GetComponent<RectTransform>().localScale = new Vector3(1.3f,1.3f,0);
		}

		void OnMouseUp(){
			gameObject.GetComponent<RectTransform>().localScale = vector;
			Debug.Log(gameObject.GetComponent<RectTransform>().localScale);
		}
	}
}
