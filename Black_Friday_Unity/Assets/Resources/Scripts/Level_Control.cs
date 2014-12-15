using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level_Control : Scene_Control
{
	//public
	public int 							levelNumber;
	public float 						playTime;
	public Object[]						levelItems;
	public int							collectedCash;

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
	private List<UISprite>				scratchouts;
	private int							collectedAmount;
	private Transform					endPosition;
	private List<string>				inCart;
	private Transform					beltStart;

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
		scratchouts			= new List<UISprite>();
		itemIndicator		= GameObject.Find("Item Indicator").GetComponent<Indicator_Control>();
		cameraScript 		= Camera.main.GetComponent<Camera_Control>();
		UILabel[] tempList 	= GameObject.Find("Shopping List").GetComponentsInChildren<UILabel>();
		UISprite[] tempS  	= GameObject.Find("Shopping List").GetComponentsInChildren<UISprite>();
		GameObject[] temp 	= GameObject.FindGameObjectsWithTag("Item Location");
		collectedAmount		= 0;
		inCart				= new List<string>();

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
			scratchouts.Add(tempS[i]);
			scratchouts[i].gameObject.SetActive(false);
		}
		scratchouts[0].gameObject.SetActive(true);
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
	override public void MyUpdate()
	{
		scoreBoard.text = collectedCash.ToString();

		if(playTime > 0.0f && masterScript.GetInGame())
		{
			playTime -= Time.deltaTime;
			int minutes = (int)playTime / 60;
			int seconds = (int)playTime % 60;
			uiTimer.text = minutes.ToString() + ":" + seconds.ToString();
		}
		else if(playTime <= 0.0f)
		{
			masterScript.GameOver();
		}
	}

	public void SetCurrentListItem(int source)
	{
		itemControl[source].SetActive(true);
		KeyItem_Control tempScript 	= itemControl[source];
		itemControl[source] 		= itemControl[0];
		itemControl[0] 				= tempScript;

		GameObject tempObject 	= shoppingList[source];
		shoppingList[source] 	= shoppingList[0];
		shoppingList[0] 		= tempObject;

		uiShoppingItem[source].text = shoppingList[source].name;
		uiShoppingItem[0].text 		= shoppingList[0].name;

		itemControl[0].SetActive(true);
		itemControl[source].SetActive(false);
		itemIndicator.SetTarget(itemControl[0].transform.position);
	}

	public void DisplayShoppingList()
	{
		for(int i = 0; i < shoppingButtons.Count; i++)
		{
			itemControl[i].SetActive(false);
			shoppingButtons[i].SetActive(true);
		}
		//showScore = false;
	}

	public void CondenseShoppingList()
	{
		shoppingButtons[0].ResetPosition();
		for(int i = 1; i < shoppingButtons.Count; i++)
		{
			shoppingButtons[i].SetActive(false);
		}
		//showScore = true;
	}

	public void CollectedItem(int source)
	{
		collectedAmount += 1;
		collectedCash += source;
		inCart.Add(shoppingList[0].name.ToString());
		SetCurrentListItem(shoppingButtons.Count - collectedAmount);
		scratchouts[shoppingButtons.Count - collectedAmount].gameObject.SetActive(true);
		shoppingButtons[shoppingButtons.Count - collectedAmount].collider.enabled = false;
	}

	public void SetupCheckout()
	{
		beltStart = GameObject.Find("Belt Start").transform;
	}

	private float 	itemTimer 	= 3.0f, 
					itemTrigger = 5.0f, 
					scanTimer 	= 0.0f, 
					scanTrigger = 1.0f;
	private int index 			= 0;
	List<GameObject> cartItems = new List<GameObject>();
	KeyItem_Control	itemScript;
	Vector3 move;
	float speed = 0.5f;

	public void RunCheckout()
	{
		itemTimer += Time.deltaTime;

		for(int i = 0; i < cartItems.Count; i++)
		{
			move = cartItems[i].transform.position;
			move += beltStart.transform.forward * speed * Time.deltaTime;
			cartItems[i].transform.position = move;
		}

		if(itemTimer >= itemTrigger && inCart.Count > index)
		{
			cartItems.Add(GameObject.Instantiate(Resources.Load("Prefabs/Key Items/" + inCart[index]), beltStart.position, beltStart.rotation)as GameObject);
			cartItems[index].gameObject.SetActive(true);
			itemScript = cartItems[index].GetComponent<KeyItem_Control>();
			itemScript.SetActive(true);
			itemTimer = 0.0f;
			index += 1;
		}
		else if(index >= inCart.Count)
		{
			for(int i = 0; i < cartItems.Count; i++)
			{
				if(!cartItems[i].renderer.isVisible)
				{
					cartItems.RemoveAt(i);
					i--;
				}
			}
			if(cartItems.Count == 0)
			{
				masterScript.AddCash(collectedCash);
				SetSelection(1);
			}
		}
	}

	private float 	lostTimer 	= 0.0f, 
					lostTrigger;

	public void PlayerLost()
	{
		lostTrigger = 5.0f * inCart.Count;

		lostTimer += Time.deltaTime;
		if(lostTimer >= lostTrigger)
		{
			SetSelection(1);
		}
	}
}
