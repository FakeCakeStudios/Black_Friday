using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Webstore_Control : Scene_Control
{
	//private
	private bool 			back,
							change,
							kartMenu,
							powerupMenu;
	private List<int> 		itemCost;
	private int				selection;
	private GameObject 		kart;
	private UIPanel[] 		panels;

	void Initialize()
	{
		//default values
		back 			= false;
		change 			= false;
		kartMenu		= false;
		powerupMenu 	= false;
		itemCost 		= new List<int>();
		selection 		= 0;
		panels 			= new UIPanel[3];
		panels[0] 		= GameObject.Find("Webstore 1").GetComponent<UIPanel>();
		panels[1] 		= GameObject.Find("Kart 1").GetComponent<UIPanel>();
		panels[2] 		= GameObject.Find("Powerups 1").GetComponent<UIPanel>();
	}
	
	// Update is called once per frame
	void MyUpdate()
	{
		//only make changes if a change has been indicated from the user input
		if(change)
		{
			change 				= false;
			panels[0].enabled 	= true;
			panels[1].enabled 	= true;

			if(kartMenu)
			{
				panels[4].enabled = true;
				SetupCosts(0);
			}
			else if(powerupMenu)
			{
					panels[7].enabled = true;
				SetupCosts(1);
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
				DontDestroyOnLoad(master);
				Application.LoadLevel("Menu");
			}
		}
	}
	
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

	//Define what will happen for values 8 and above
	override public void SceneAction(int source)
	{

	}
}
