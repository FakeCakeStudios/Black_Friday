using UnityEngine;
using System.Collections;

public class Webstore_Button : Button_Base
{
	private Menu_Control menuScene;
	
	override public void Initialize()
	{
		menuScene = GameObject.Find("Scene Control").GetComponent<Menu_Control>();
	}
	
	override public void OnPress(bool isPressed)
	{
		menuScene.SetWebstore(true);
	}
	
	override public void OnHover(bool isOver)
	{
		
	}
	
	override public void OnSelect(bool selected)
	{
		
	}
}
