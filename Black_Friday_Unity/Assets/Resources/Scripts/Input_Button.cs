using UnityEngine;
using System.Collections;

public class Input_Button : MonoBehaviour
{
	private GameObject 	player;
	public string 		functionName;
	
	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void OnPress(bool isPressed)
	{
		player.SendMessage(functionName, true, SendMessageOptions.DontRequireReceiver);
	}
	
	void OnHover(bool isOver)
	{
		
	}
	
	void OnSelect(bool selected)
	{

	}
}
