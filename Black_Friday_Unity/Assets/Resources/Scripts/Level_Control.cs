﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level_Control : Scene_Control
{
	//public
	public int 							levelNumber;
	public float 						playTime;
	public Object[]						levelItems;

	//private
	private UILabel						uiTimer;
	private List<UILabel>				uiShoppingItem;
	private List<ShoppingList_Button>	shoppingButtons;
	private List<Vector3> 				itemLocations;
	private List<GameObject> 			shoppingList;
	private List<KeyItem_Control>		itemControl;
	private UILabel						scoreBoard;
	private bool						showScore;
	private Indicator_Control			itemIndicator;
	private Camera_Control				cameraScript;

	override public void Initialize()
	{
		uiTimer 			= GameObject.FindGameObjectWithTag("Timer").GetComponent<UILabel>();
		uiShoppingItem 		= new List<UILabel>();
		shoppingButtons 	= new List<ShoppingList_Button>();
		itemLocations 		= new List<Vector3>();
		shoppingList 		= new List<GameObject>();
		scoreBoard 			= GameObject.Find("Score").GetComponent<UILabel>();
		itemControl 		= new List<KeyItem_Control>();
		showScore 			= true;
		itemIndicator		= GameObject.Find("Item Indicator").GetComponent<Indicator_Control>();
		cameraScript 		= Camera.main.GetComponent<Camera_Control>();

		UILabel[] tempList = GameObject.Find("Shopping List").GetComponentsInChildren<UILabel>();
		GameObject[] temp = GameObject.FindGameObjectsWithTag("Item Location");
		for(int i = 0 ; i < temp.Length; i++)
		{
			itemLocations.Add(temp[i].transform.position);
			Destroy(temp[i]);

			int index = Random.Range(0, levelItems.Length);
			shoppingList.Add(Instantiate(levelItems[index], itemLocations[i], Quaternion.identity)as GameObject);
			shoppingList[i].name = levelItems[index].name;
			itemControl.Add(shoppingList[i].GetComponent<KeyItem_Control>());
			uiShoppingItem.Add(tempList[i]);
			uiShoppingItem[i].text = shoppingList[i].name;
			shoppingButtons.Add(tempList[i].GetComponentInParent<ShoppingList_Button>());
			shoppingButtons[i].SetActive(false);
		}
		shoppingButtons[0].SetActive(true);
		itemControl[0].SetActive(true);
		itemIndicator.SetTarget(itemControl[0].transform.position);

		for(int i = temp.Length; i < tempList.Length; i++)
		{
			tempList[i].GetComponentInParent<ShoppingList_Button>().DestroyExtras();
		}

		masterScript.LevelSetup();
		
		cameraScript.SetPlayer();
		cameraScript.SetFollowPlayer(false);

	}


		
	
	// Update is called once per frame
	void MyUpdate()
	{
		if(showScore)
		{
			scoreBoard.text = masterScript.GetPlayerData().GetCash().ToString();
		}
		else
		{
			scoreBoard.text = "";
		}

		if(playTime > 0.0f && masterScript.GetInGame())
		{
			playTime -= Time.deltaTime;
			int minutes = (int)playTime / 60;
			int seconds = (int)playTime % 60;
			uiTimer.text = minutes.ToString() + ":" + seconds.ToString();
		}
	}

	public void SetCurrentListItem(int source)
	{
		itemControl[source].SetActive(true);
		itemIndicator.SetTarget(itemControl[source].transform.position);

		int adjuster = 0;
		for(int i = 0; i < shoppingList.Count; i++)
		{
			if(source + i >= shoppingList.Count)
			{
				adjuster = source + i - shoppingList.Count;
				uiShoppingItem[i].text = shoppingList[adjuster].name;
			}
			else
			{
				uiShoppingItem[i].text = shoppingList[source + i].name;
			}
		}
	}

	public void DisplayShoppingList()
	{
		for(int i = 0; i < shoppingButtons.Count; i++)
		{
			itemControl[i].SetActive(false);
			shoppingButtons[i].SetActive(true);
		}
		showScore = false;
	}

	public void CondenseShoppingList()
	{
		for(int i = 1; i < shoppingButtons.Count; i++)
		{
			shoppingButtons[i].SetActive(false);
		}
		showScore = true;
	}
}
