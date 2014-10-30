using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level_Control : MonoBehaviour
{
	//public
	public int 							levelNumber;
	public float 						playTime;
	public Object[]						levelItems;

	//private
	private GameObject 					master;
	private Master_Control 				masterScript;
	private UILabel						uiTimer;
	private List<UILabel>				uiShoppingItem;
	private List<ShoppingList_Button>	shoppingButtons;
	private List<Vector3> 				itemLocations;
	private List<GameObject> 			shoppingList;

	void Awake()
	{
		master 				= GameObject.Find("Master Control");
		masterScript 		= master.GetComponent<Master_Control>();
		uiTimer 			= GameObject.FindGameObjectWithTag("Timer").GetComponent<UILabel>();
		uiShoppingItem 		= new List<UILabel>();
		shoppingButtons 	= new List<ShoppingList_Button>();
		itemLocations 		= new List<Vector3>();
		shoppingList 		= new List<GameObject>();

		UILabel[] tempList = GameObject.Find("Shopping List").GetComponentsInChildren<UILabel>();
		GameObject[] temp = GameObject.FindGameObjectsWithTag("Item Location");
		for(int i = 0 ; i < temp.Length; i++)
		{
			itemLocations.Add(temp[i].transform.position);
			Destroy(temp[i]);

			int index = Random.Range(0, levelItems.Length);
			shoppingList.Add(Instantiate(levelItems[index], itemLocations[i], Quaternion.identity)as GameObject);
			shoppingList[i].name = levelItems[index].name;
			uiShoppingItem.Add(tempList[i]);
			uiShoppingItem[i].text = shoppingList[i].name;
			shoppingButtons.Add(tempList[i].GetComponentInParent<ShoppingList_Button>());
			shoppingButtons[i].SetActive(false);
		}
		shoppingButtons[0].SetActive(true);

		for(int i = temp.Length; i < tempList.Length; i++)
		{
			tempList[i].GetComponentInParent<ShoppingList_Button>().DestroyExtras();
		}

		//GameObject[] buttonObjects = GameObject.FindGameObjectsWithTag("Shopping List");
		//for(int i = 0; i < buttonObjects.Length; i++)
		//{
			//uiShoppingButtons.Add(buttonObjects[i]);
			//uiShoppingButtons[i].SetActive(false);
		//}

		//for(int i = temp.Length; i < uiShoppingButtons.Count; i++)
		//{
		//	Destroy(uiShoppingButtons[i]);
			//uiShoppingButtons.RemoveAt(i);
		//}
	}

	// Use this for initialization
	void Start()
	{
		masterScript.LevelSetup();
		masterScript.SetInGame(true);
	}
	
	// Update is called once per frame
	void Update()
	{
		playTime -= Time.deltaTime;

		if(playTime > 0.0f)
		{
			int minutes = (int)playTime / 60;
			int seconds = (int)playTime % 60;
			uiTimer.text = minutes.ToString() + ":" + seconds.ToString();
		}
	}

	public void SetCurrentListItem(int source)
	{
		for(int i = 0; i < shoppingList.Count; i++)
		{
			uiShoppingItem[i].text = shoppingList[source + i].name;
		}
	}

	public void DisplayShoppingList()
	{
		for(int i = 0; i < shoppingButtons.Count; i++)
		{
			shoppingButtons[i].SetActive(true);
		}
	}

	public void CondenseShoppingList()
	{
		for(int i = 1; i < shoppingButtons.Count; i++)
		{
			shoppingButtons[i].SetActive(false);
		}
	}
}
