using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Select_Control : Scene_Control
{
	private int 				selection;
	private GameObject			levelSelectPanel;
	private GameObject 			characterSelectPanel;
	private bool				levelSelectState;
	private int					levelSelection;
	private List<UISprite> 		levelButtons;
	private List<string>		levelSprites;
	private List<UISprite> 		characterButtons;
	private List<string>		characterSprites;
	private List<Collider> 		levelColliders;
	private List<Index_Button>	levelScripts;
	private List<Collider> 		characterColliders;
	private List<Index_Button>	characterScripts;
	private Vector3				shownModelPoint;
	private Vector3				hiddenModelPoint;
	private GameObject			nerdModel;
	private GameObject			psychoModel;

	override public void Initialize()
	{
		selection 				= 0;
		levelSelectPanel 		= GameObject.Find("Level Select Panel");
		characterSelectPanel 	= GameObject.Find("Character Select Panel");
		levelSelectState 		= true;
		levelSelection 			= 5;
		levelButtons 			= new List<UISprite>();
		levelSprites			= new List<string>();
		characterButtons 		= new List<UISprite>();
		characterSprites		= new List<string>();
		levelColliders 			= new List<Collider>();
		levelScripts 			= new List<Index_Button>();
		characterColliders 		= new List<Collider>();
		characterScripts 		= new List<Index_Button>();

		levelButtons.Add(GameObject.Find("Current Level Image").GetComponent<UISprite>());
		GameObject temp = GameObject.Find("Top Next Level Button");
		levelButtons.Add(temp.GetComponentInChildren<UISprite>());
		levelColliders.Add(temp.collider);
		levelScripts.Add(temp.GetComponent<Index_Button>());
		temp = GameObject.Find("Bottom Next Level Button");
		levelButtons.Add(temp.GetComponentInChildren<UISprite>());
		levelColliders.Add(temp.collider);
		levelScripts.Add(temp.GetComponent<Index_Button>());
		shownModelPoint = GameObject.Find("Nerd").transform.position;
		hiddenModelPoint = GameObject.Find("Psycho").transform.position;
		nerdModel = GameObject.Find("Nerd");
		psychoModel = GameObject.Find("Psycho");

		for(int i = 0; i < levelButtons.Count; i++)
		{
			levelSprites.Add(levelButtons[i].spriteName);
		}

		characterButtons.Add(GameObject.Find("Current Character Image").GetComponent<UISprite>());
		temp = GameObject.Find("Character 2 Button");
		characterButtons.Add(temp.GetComponentInChildren<UISprite>());
		characterColliders.Add(temp.collider);
		characterScripts.Add(temp.GetComponent<Index_Button>());
		temp = GameObject.Find("Character 3 Button");
		characterButtons.Add(temp.GetComponentInChildren<UISprite>());
		characterColliders.Add(temp.collider);
		characterScripts.Add(temp.GetComponent<Index_Button>());
		temp = GameObject.Find("More Characters Button");
		characterButtons.Add(temp.GetComponentInChildren<UISprite>());
		characterColliders.Add(temp.collider);
		characterScripts.Add(temp.GetComponent<Index_Button>());

		for(int i = 0; i < characterButtons.Count; i++)
		{
			characterSprites.Add(characterButtons[i].spriteName);
		}

		Player_Data playerUnlocks = masterScript.GetPlayerData();
		List<bool> levelUnlocks = new List<bool>();
		levelUnlocks = playerUnlocks.GetLevelUnlocks();

		for(int i = 0; i < levelButtons.Count - 1; i++)
		{
			if(!levelUnlocks[i])
			{
				levelColliders[i].enabled 	= false;
				levelScripts[i].enabled 	= false;
			}
		}

		List<bool> characterUnlocks = new List<bool>();
		characterUnlocks = playerUnlocks.GetCharacterUnlocks();
		
		for(int i = 0; i < characterButtons.Count - 1; i++)
		{
			if(!characterUnlocks[i])
			{
				characterColliders[i].enabled 	= false;
				characterScripts[i].enabled 	= false;
			}
		}
		characterSelectPanel.SetActive(false);
	}

	override public void MyUpdate()
	{
		switch(selection)
		{
		case(100):
		{
			ChangePanels();
			break;
		}
		case(101):
		{
			ChangeLevelButtons(selection - 100);
			break;
		}
		case(102):
		{
			ChangeLevelButtons(selection - 100);
			break;
		}
		case(200):
		{
			SetSelection(levelSelection);
			break;
		}
		case(201):
		{
			ChangeCharacterSelection(selection - 200);
			break;
		}
		}
		selection = 0;
	}

	override public void SceneAction(int source)
	{
		selection = source;
	}

	void ChangePanels()
	{
		if(levelSelectState)
		{
			levelSelectPanel.SetActive(false);
			characterSelectPanel.SetActive(true);
			levelSelectState = false;
		}
		else
		{
			levelSelectPanel.SetActive(true);
			characterSelectPanel.SetActive(false);
			levelSelectState = true;
		}
	}

	void ChangeLevelButtons(int source)
	{
		string temp 					= levelButtons[0].spriteName;
		levelButtons[0].spriteName 		= levelSprites[source];
		levelButtons[source].spriteName = temp;
		levelSprites[0] 				= levelButtons[0].spriteName;
		levelSprites[source] 			= levelButtons[source].spriteName;

		//TODO Replace these strings to the names of the images used to represent each level
		switch(levelButtons[0].spriteName)
		{
		case("level1_worstbuy"):
		{
			levelSelection = 5;
			break;
		}
		case("level2_derpmart"):
		{
			levelSelection = 6;
			break;
		}
		//case("teal_32x32"):
		//{
		//	levelSelection = 7;
		//	break;
		//}
		}
	}

	void ChangeCharacterSelection(int source)
	{
		string temp 						= characterButtons[0].spriteName;
		characterButtons[0].spriteName 		= characterSprites[source];
		characterButtons[source].spriteName = temp;
		characterSprites[0] 				= characterButtons[0].spriteName;
		characterSprites[source] 			= characterButtons[source].spriteName;
	}
}
