using UnityEngine;
using System.Collections;

public class Pause_Button : MonoBehaviour
{
	private Master_Control 	masterScript;
	private Level_Control  	sceneControl;
	private GameObject 		pauseOverlay;
	
	void Start()
	{
		masterScript = GameObject.FindGameObjectWithTag("Master").GetComponent<Master_Control>();
		sceneControl = GameObject.Find("Scene Control").GetComponent<Level_Control>();
		pauseOverlay = GameObject.Find("Pause Overlay");
	}
	
	void OnPress(bool isPressed)
	{
		masterScript.SetPause();
		masterScript.SetTurorial(false);
		sceneControl.CondenseShoppingList();
		pauseOverlay.SetActive(false);
	}
	
	void OnHover(bool isOver)
	{
		
	}
	
	void OnSelect(bool selected)
	{

	}
}
