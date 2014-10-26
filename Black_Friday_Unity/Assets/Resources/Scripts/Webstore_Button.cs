using UnityEngine;
using System.Collections;

public class Webstore_Button : MonoBehaviour
{
	private Menu_Control menuScene;
	
	void Start()
	{
		menuScene = GameObject.Find("Scene Control").GetComponent<Menu_Control>();
	}
	
	void OnPress(bool isPressed)
	{
		menuScene.SetWebstore(true);
	}
	
	void OnHover(bool isOver)
	{
		
	}
	
	void OnSelect(bool selected)
	{
		
	}
}
