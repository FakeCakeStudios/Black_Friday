using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level_Control : MonoBehaviour
{
	//public
	public int 					levelNumber;
	public float 				playTime;

	//private
	private GameObject 			master;
	private Master_Control 		masterScript;
	private UILabel				uiTimer;
	private List<UILabel>		uiShoppingItem;
	private List<GameObject>	uiShoppingButtons;

	private List<Vector3> 		itemLocations;
	public Object[]				levelItems;
	private List<GameObject> 	shoppingList;

	void Awake()
	{
		master 			= GameObject.Find("Master Control");
		masterScript 	= master.GetComponent<Master_Control>();
		uiTimer 		= GameObject.FindGameObjectWithTag("Timer").GetComponent<UILabel>();
		uiShoppingItem 	= new List<UILabel>();
		uiShoppingButtons = new List<GameObject>();

		itemLocations 	= new List<Vector3>();
		shoppingList 	= new List<GameObject>();

		UILabel[] tempList = GameObject.Find("Shopping List").GetComponentsInChildren<UILabel>();
		for(int i = 0; i < tempList.Length; i++)
		{
			uiShoppingItem.Add(tempList[i]);
		}

		GameObject[] buttonObjects = GameObject.FindGameObjectsWithTag("Shopping List");
		int k = 0;
		for(int i = buttonObjects.Length - 1; i >= 0; i--)
		{
			uiShoppingButtons.Add(buttonObjects[i]);
			uiShoppingButtons[k].SetActive(false);
			k += 1;
		}
		uiShoppingButtons[0].SetActive(true);

		GameObject[] temp = GameObject.FindGameObjectsWithTag("Item Location");
		for(int i = 0 ; i < temp.Length; i++)
		{
			itemLocations.Add(temp[i].transform.position);
			Destroy(temp[i]);
		}

		for(int i = 0; i < temp.Length; i++)
		{
			int index = Random.Range(0, levelItems.Length);
			shoppingList.Add(Instantiate(levelItems[index], itemLocations[i], Quaternion.identity)as GameObject);
			shoppingList[i].name = levelItems[index].name;
			uiShoppingItem[i].text = shoppingList[i].name;
		}

		for(int i = temp.Length; i < uiShoppingButtons.Count; i++)
		{
			Destroy(uiShoppingButtons[i]);
		}
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
		for(int i = 0; i < uiShoppingButtons.Count; i++)
		{
			uiShoppingButtons[i].SetActive(true);
		}
	}

	public void CondenseShoppingList()
	{
		for(int i = 1; i < uiShoppingButtons.Count; i++)
		{
			uiShoppingButtons[i].SetActive(false);
		}
	}
}
