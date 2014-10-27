using UnityEngine;
using System.Collections;

public class Input_Button : MonoBehaviour
{
	private Player_Control 	playerControl;
	public string 			functionName;
	
	public void Initialize()
	{
		playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Control>();
	}
	
	void OnPress(bool isPressed)
	{
		playerControl.SendMessage(functionName, true, SendMessageOptions.DontRequireReceiver);
	}
	
	void OnHover(bool isOver)
	{
		
	}
	
	void OnSelect(bool selected)
	{

	}
}
