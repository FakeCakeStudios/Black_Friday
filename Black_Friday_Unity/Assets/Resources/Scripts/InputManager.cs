using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : MonoBehaviour
{
	public LayerMask mask;
	private List<GameObject> touchList;
	private GameObject[] oldList;
	private RaycastHit hit;

	public void Initialize()
	{
		touchList = new List<GameObject>();
	}

	public void InputUpdate()
	{
#if UNITY_EDITOR
		if(Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
		{
			oldList = new GameObject[touchList.Count];
			touchList.CopyTo(oldList);
			touchList.Clear();
			
			foreach(Touch touch in Input.touches)
			{
				Ray ray = camera.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				
				if(Physics.Raycast(ray, out hit, mask))
				{
					GameObject reciever = hit.transform.gameObject;
					touchList.Add (reciever);
					
					if(Input.GetMouseButtonDown(0))
					{
						reciever.SendMessage("OnTouchDown", SendMessageOptions.DontRequireReceiver);
					}
					else if(Input.GetMouseButtonUp(0))
					{
						reciever.SendMessage("OnTouchUp", SendMessageOptions.DontRequireReceiver);
					}
					else if(Input.GetMouseButton(0))
					{
						reciever.SendMessage("OnTouchStay", SendMessageOptions.DontRequireReceiver);
					}
				}
			}
			foreach(GameObject g in oldList)
			{
				if(!touchList.Contains(g))
				{
					g.SendMessage("OnTouchExit", SendMessageOptions.DontRequireReceiver);
				}
			}
		}
#endif
		if(Input.touchCount > 0)
		{
			oldList = new GameObject[touchList.Count];
			touchList.CopyTo(oldList);
			touchList.Clear();

			foreach(Touch touch in Input.touches)
			{
				Ray ray = camera.ScreenPointToRay(touch.position);
				RaycastHit hit;

				if(Physics.Raycast(ray, out hit, mask))
				{
					GameObject reciever = hit.transform.gameObject;
					touchList.Add (reciever);

					if(touch.phase == TouchPhase.Began)
					{
						reciever.SendMessage("OnTouchDown", SendMessageOptions.DontRequireReceiver);
					}
					else if(touch.phase == TouchPhase.Ended)
					{
						reciever.SendMessage("OnTouchUp", SendMessageOptions.DontRequireReceiver);
					}
					else if(touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
					{
						reciever.SendMessage("OnTouchStay", SendMessageOptions.DontRequireReceiver);
					}
					else if(touch.phase == TouchPhase.Canceled)
					{
						reciever.SendMessage("OnTouchExit", SendMessageOptions.DontRequireReceiver);
					}
				}
			}
			foreach(GameObject g in oldList)
			{
				if(!touchList.Contains(g))
				{
					g.SendMessage("OnTouchExit", SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	}
}
