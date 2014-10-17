using UnityEngine;
using System.Collections;

public class Confirm_Button : MonoBehaviour
{
	private GameObject sceneControl;
	
	void Start()
	{
		sceneControl = GameObject.Find("Scene Control");
	}
	
	void OnPress(bool isPressed)
	{
		sceneControl.SendMessage("SetBack", true, SendMessageOptions.DontRequireReceiver);
	}
	
	void OnHover(bool isOver)
	{
		
	}
	
	void OnSelect(bool selected)
	{
		
	}
}
