using UnityEngine;
using System.Collections;

public class Selection_Button : MonoBehaviour
{
	public int 				number;
	private Select_Control 	selectScene;
	
	
	void Start()
	{
		selectScene = GameObject.Find("Scene Control").GetComponent<Select_Control>();
	}
	
	void OnPress(bool isPressed)
	{
		selectScene.SetSelection(index);
	}
	
	void OnHover(bool isOver)
	{
		
	}
	
	void OnSelect(bool selected)
	{
		
	}
}