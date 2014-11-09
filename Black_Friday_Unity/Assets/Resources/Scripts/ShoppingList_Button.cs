using UnityEngine;
using System.Collections;

public class ShoppingList_Button : MonoBehaviour
{
	private Master_Control 	masterScript;
	private Level_Control  	sceneControl;
	private GameObject 		pauseOverlay;
	public int 				itemIndex;
	private float 			buttonTimer;
	private float 			buttontrigger;
	private UIAnchor		currentItemAnchor;
	private GameObject		currentItemBG;

	void Start()
	{
		masterScript 		= GameObject.FindGameObjectWithTag("Master").GetComponent<Master_Control>();
		sceneControl 		= GameObject.Find("Scene Control").GetComponent<Level_Control>();
		pauseOverlay 		= GameObject.Find("Pause Overlay");
		currentItemAnchor	= GameObject.Find("Current Item").GetComponent<UIAnchor>();
		currentItemBG		= GameObject.Find("Current Item Background");
		buttonTimer 		= 0.0f;
		buttontrigger 		= 0.2f;

		if(this.name == "Current Item")
		{
			pauseOverlay.SetActive(false);
		}
	}
	
	void OnPress(bool isPressed)
	{
		if(!masterScript.GetTutorialUp() && isPressed == true)
		{
			masterScript.SetPause();
			if(masterScript.GetPause())
			{
				sceneControl.DisplayShoppingList();
				pauseOverlay.SetActive(true);
				currentItemAnchor.relativeOffset.x = 0.175f;
				currentItemBG.SetActive(false);
			}
			else
			{
				sceneControl.SetCurrentListItem(itemIndex);
				sceneControl.CondenseShoppingList();
				pauseOverlay.SetActive(false);
				currentItemAnchor.relativeOffset.x = 0.4f;
				currentItemBG.SetActive(true);
			}
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
}
