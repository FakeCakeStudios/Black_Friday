using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Webstore_Control : MonoBehaviour
{
	//bools
	private bool 			back,
							change,
							kartMenu,
							powerupMenu;

	//ints
	private int				state;
	private List<int> 		itemCost;
	private int				selection;

	//Game Objects
	private GameObject 		kart;
	private GameObject 		master;

	//scripts
	private UIPanel[] 		panels;
	private Master_Control	masterScript;
	private Player_Data 	player;

	
	// Use this for initialization
	void Start()
	{
		//default values
		back 			= false;
		change 			= false;
		kartMenu		= false;
		powerupMenu 	= false;

		//default values
		state 			= 0;
		itemCost 		= new List<int>();
		selection 		= 0;

		//obtain game objects needed
		kart 			= GameObject.Find("Kart");
		master 			= GameObject.FindGameObjectWithTag("Master");

		//panels is not a dynamic element and will be hard coded
		panels 			= new UIPanel[10];
		panels[0] 		= GameObject.Find("Store Select").GetComponent<UIPanel>();
		panels[1] 		= GameObject.Find("Webstore 1").GetComponent<UIPanel>();
		panels[2] 		= GameObject.Find("Webstore 2").GetComponent<UIPanel>();
		panels[3] 		= GameObject.Find("Webstore 3").GetComponent<UIPanel>();
		panels[4] 		= GameObject.Find("Kart 1").GetComponent<UIPanel>();
		panels[5] 		= GameObject.Find("Kart 2").GetComponent<UIPanel>();
		panels[6] 		= GameObject.Find("Kart 3").GetComponent<UIPanel>();
		panels[7] 		= GameObject.Find("Powerups 1").GetComponent<UIPanel>();
		panels[8] 		= GameObject.Find("Powerups 2").GetComponent<UIPanel>();
		panels[9] 		= GameObject.Find("Powerups 3").GetComponent<UIPanel>();
		masterScript 	= master.GetComponent<Master_Control>();
		player 			= masterScript.GetPlayerData();
	}
	
	// Update is called once per frame
	void Update()
	{
		//only make changes if a change has been indicated from the user input
		if(change)
		{
			//reset all panels
			for(int i = 0; i < panels.Length; i++)
			{
				panels[i].enabled = false;
			}

			//only iterate through once when changes are made
			change = false;

			//switch statement is based on which webstore is shown or store select
			switch(state)
			{
			case(0):
			{
				//enable the panel currently shown
				panels[0].enabled = true;
				break;
			}
			case(1):
			{
				//enable the panel currently shown
				panels[1].enabled = true;

				//check if a submenu needs to be displayed
				if(kartMenu)
				{
					//enable the panel currently shown
					panels[4].enabled = true;

					//setup item costs for items from the current webstore
					SetupCosts(0);
				}
				else if(powerupMenu)
				{
					//enable the panel currently shown
					panels[7].enabled = true;

					//setup item costs for items from the current webstore
					SetupCosts(1);
				}
				break;
			}
			case(2):
			{
				//enable the panel currently shown
				panels[2].enabled = true;

				//check if a submenu needs to be displayed
				if(kartMenu)
				{
					//enable the panel currently shown
					panels[5].enabled = true;

					//setup item costs for items from the current webstore
					SetupCosts(2);
				}
				else if(powerupMenu)
				{
					//enable the panel currently shown
					panels[8].enabled = true;

					//setup item costs for items from the current webstore
					SetupCosts(3);
				}
				break;
			}
			case(3):
			{
				//enable the panel currently shown
				panels[3].enabled = true;

				//check if a submenu needs to be displayed
				if(kartMenu)
				{
					//enable the panel currently shown
					panels[6].enabled = true;

					//setup item costs for items from the current webstore
					SetupCosts(4);
				}
				else if(powerupMenu)
				{
					//enable the panel currently shown
					panels[9].enabled = true;

					//setup item costs for items from the current webstore
					SetupCosts(5);
				}
				break;
			}
			}
		}

		//controls all actions for the back button no matter current state of scene
		if(back)
		{
			//if displaying a submenu then back out to previous
			if(kartMenu || powerupMenu)
			{
				change 		= true;
				kartMenu 	= false;
				powerupMenu = false;

				//always clear item cost list so it will be clean when setting back up
				itemCost.Clear();
			}
			//if a submenu isn't displaying
			else
			{
				//if not at base panel display for the scene then rturn to base display, otherwise load Main Menu
				if(state > 0)
				{
					change 	= true;
					state 	= 0;
				}
				else
				{
					DontDestroyOnLoad(master);
					Application.LoadLevel("Menu");
				}
			}
		}
	}

	//this function needs to be completed with the intended prices for the items
	//put the prices in numerical order, so the first Add call is related to the first button item
	//case(0) is Webstore 1's Kart upgrades
	//case(1) is Webstore 1's Powerups
	//case(2) is Webstore 2's Kart upgrades
	//case(3) is Webstore 2's Powerups
	//case(4) is Webstore 3's Kart upgrades
	//case(5) is Webstore 3's Powerups
	//TODO need to complete with the prices of items organized as stated above
	void SetupCosts(int source)
	{
		switch(source)
		{
		case(0):
		{
			itemCost.Add(0);//only an example to copy paste for each cost that needs to be added
			break;
		}
		case(1):
		{
			itemCost.Add(0);
			break;
		}
		case(2):
		{
			itemCost.Add(0);
			break;
		}
		case(3):
		{
			itemCost.Add(0);
			break;
		}
		case(4):
		{
			itemCost.Add(0);
			break;
		}
		case(5):
		{
			itemCost.Add(0);
			break;
		}
		}
	}

	public void SetKartMenu(bool source)
	{
		kartMenu = source;
	}

	public void SetPowerupMenu(bool source)
	{
		powerupMenu = source;
	}

	public void SetBack(bool source)
	{
		back = source;
	}

	public void SetChange(bool source)
	{
		change = source;
	}

	public void SetSelection(int source)
	{
		selection = source;
	}
}
