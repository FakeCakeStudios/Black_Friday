using UnityEngine;
using System.Collections;

public class Pause_Button : MonoBehaviour
{
	private Master_Control masterScript;
	
	void Start()
	{
		masterScript = GameObject.FindGameObjectWithTag("Master").GetComponent<Master_Control>();
	}
	
	void OnPress(bool isPressed)
	{
		masterScript.SetPause();
		this.gameObject.SetActive(false);
	}
	
	void OnHover(bool isOver)
	{
		
	}
	
	void OnSelect(bool selected)
	{

	}
}
