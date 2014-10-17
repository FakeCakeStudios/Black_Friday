using UnityEngine;
using System.Collections;

public class Back_Button : MonoBehaviour
{
	private Select_Control menuScene;
	
	void Start()
	{
		selectScene = GameObject.Find("Scene Control").GetComponent<Select_Control>();
	}
	
	void OnPress(bool isPressed)
	{
		selectScene.SetBack(true);
	}
	
	void OnHover(bool isOver)
	{
		
	}
	
	void OnSelect(bool selected)
	{
		
	}
}
