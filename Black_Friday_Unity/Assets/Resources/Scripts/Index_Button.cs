using UnityEngine;
using System.Collections;

public class Index_Button : MonoBehaviour
{
	public int 			index;
	private GameObject 	sceneControl;
	
	
	void Start()
	{
		sceneControl = GameObject.Find("Scene Control");
	}
	
	void OnPress(bool isPressed)
	{
		sceneControl.SendMessage("SetSelection", index, SendMessageOptions.DontRequireReceiver);
	}
	
	void OnHover(bool isOver)
	{
		
	}
	
	void OnSelect(bool selected)
	{
		
	}
}
