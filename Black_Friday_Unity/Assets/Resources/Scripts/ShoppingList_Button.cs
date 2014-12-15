using UnityEngine;
using System.Collections;

public class ShoppingList_Button : MonoBehaviour
{
	private Master_Control 	masterScript;
	private Level_Control  	sceneControl;
	private GameObject 		pauseOverlay;
	public int 				itemIndex;
	private UIAnchor		currentItemAnchor;
	private GameObject		currentItemBG;

	void Start()
	{
		masterScript 		= GameObject.FindGameObjectWithTag("Master").GetComponent<Master_Control>();
		sceneControl 		= GameObject.Find("Scene Control").GetComponent<Level_Control>();
		pauseOverlay 		= GameObject.Find("Pause Overlay");
		currentItemAnchor	= GameObject.Find("Current Item").GetComponent<UIAnchor>();
		currentItemBG		= GameObject.Find("Current Item Background");

		if(this.name == "Current Item")
		{
			pauseOverlay.SetActive(false);
			//currentItemBG		= GameObject.Find("Current Item Background");
			currentItemBG = GameObject.FindWithTag("BG");
		}
	}
	
	void OnPress(bool isPressed)
	{
		if(!masterScript.GetTutorialUp() && isPressed == true)
		{
			if(!masterScript.GetPause())
			{
				masterScript.SetPause();
				sceneControl.DisplayShoppingList();
				pauseOverlay.SetActive(true);
			}
			else
			{
				sceneControl.SetCurrentListItem(itemIndex);
			}
			ResetPosition();
		}
	}
	
	void OnHover(bool isOver)
	{
		
	}
	
	void OnSelect(bool selected)
	{
		
	}

	public void SetActive(bool source)
	{
		this.gameObject.SetActive(source);
	}

	public void DestroyExtras()
	{
		Destroy(this.gameObject);
	}

	public void ResetPosition()
	{
		if(!masterScript.GetPause())
		{
			currentItemAnchor.relativeOffset.x = 0.4f;
		}
		else
		{
			currentItemAnchor.relativeOffset.x = 0.175f;
		}
	}
}
