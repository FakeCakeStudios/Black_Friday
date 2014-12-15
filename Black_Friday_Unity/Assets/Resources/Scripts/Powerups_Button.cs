using UnityEngine;
using System.Collections;

public class Powerups_Button : MonoBehaviour
{
	private GameObject sceneControl;
	
	void Start()
	{
		sceneControl = GameObject.Find("Scene Control");
	}
	
	void OnPress(bool isPressed)
	{
		sceneControl.SendMessage("SetPowerupMenu", true, SendMessageOptions.DontRequireReceiver);
	}
	
	void OnHover(bool isOver)
	{
		
	}
	
	void OnSelect(bool selected)
	{
		
	}
}
