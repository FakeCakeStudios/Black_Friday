using UnityEngine;
using System.Collections;

public class Play_Button : Button_Base
{
	private Menu_Control menuScene;

	override public void Initialize()
	{
		menuScene = GameObject.Find("Scene Control").GetComponent<Menu_Control>();
	}
	
	override public void OnPress(bool isPressed)
	{
		menuScene.SetPlay(true);
	}
	
	override public void OnHover(bool isOver)
	{
		
	}
	
	override public void OnSelect(bool selected)
	{
		
	}
}
