using UnityEngine;
using System.Collections;

public class Customization_Button : MonoBehaviour
{
	private GameObject sceneControl;
	
	void Start()
	{
		sceneControl = GameObject.Find("Scene Control");
	}
	
	void OnPress(bool isPressed)
	{
		sceneControl.SendMessage("SetKartMenu", true, SendMessageOptions.DontRequireReceiver);
	}
	
	void OnHover(bool isOver)
	{
		
	}
	
	void OnSelect(bool selected)
	{
		
	}
}
