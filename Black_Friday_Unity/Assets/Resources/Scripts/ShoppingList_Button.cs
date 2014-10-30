using UnityEngine;
using System.Collections;

public class ShoppingList_Button : MonoBehaviour
{
	private Master_Control masterScript;
	private Level_Control  sceneControl;
	public int itemIndex;
	
	void Start()
	{
		masterScript = GameObject.FindGameObjectWithTag("Master").GetComponent<Master_Control>();
		sceneControl = GameObject.Find("Scene Control").GetComponent<Level_Control>();
	}
	
	void OnPress(bool isPressed)
	{
		masterScript.SetPause();
		if(masterScript.GetPause())
		{
			sceneControl.DisplayShoppingList();
		}
		else
		{
			sceneControl.SetCurrentListItem(itemIndex);
			sceneControl.CondenseShoppingList();
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
