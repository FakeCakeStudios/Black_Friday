using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Select_Control : Scene_Control
{
	private int 			selection;
	private GameObject		levelSelectPanel;
	private GameObject 		characterSelectPanel;
	private bool			levelSelectState;
	private int				levelSelection;
	private List<UISprite> 	levelButtons;
	private List<string>	levelSprites;
	private List<UISprite> 	characterButtons;
	private List<string>	characterSprites;

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

		levelButtons.Add(GameObject.Find("Current Level Image").GetComponent<UISprite>());
		levelButtons.Add(GameObject.Find("Top Next Level Button").GetComponentInChildren<UISprite>());
		levelButtons.Add(GameObject.Find("Bottom Next Level Button").GetComponentInChildren<UISprite>());

		for(int i = 0; i < levelButtons.Count; i++)
		{
			levelSprites.Add(levelButtons[i].spriteName);
		}

		characterButtons.Add(GameObject.Find("Current Character Image").GetComponent<UISprite>());
		characterButtons.Add(GameObject.Find("Character 2 Button").GetComponentInChildren<UISprite>());
		for(int i = 0; i < characterButtons.Count; i++)
		{
			characterSprites.Add(characterButtons[i].spriteName);
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
		int index = 0;
		for(int i = 0; i < levelButtons.Count; i++)
		{
			if(source + i < levelButtons.Count)
			{
				levelButtons[i].spriteName = levelSprites[source + i];
			}
			else
			{
				levelButtons[i].spriteName = levelSprites[index];
				index += 1;
			}
		}
		for(int i = 0; i < levelButtons.Count; i++)
		{
			levelSprites[i] = levelButtons[i].spriteName;
		}

		//TODO Replace these strings to the names of the images used to represent each level
		switch(levelButtons[0].spriteName)
		{
		case("purple_32x32"):
		{
			levelSelection = 5;
			break;
		}
		case("green_32x32"):
		{
			levelSelection = 6;
			break;
		}
		case("teal_32x32"):
		{
			levelSelection = 7;
			break;
		}
		}
	}

	void ChangeCharacterSelection(int source)
	{
		int index = 0;
		for(int i = 0; i < characterButtons.Count; i++)
		{
			if(source + i < characterButtons.Count)
			{
				characterButtons[i].spriteName = characterSprites[source + i];
			}
			else
			{
				characterButtons[i].spriteName = characterSprites[index];
				index += 1;
			}
		}
		for(int i = 0; i < characterButtons.Count; i++)
		{
			characterSprites[i] = characterButtons[i].spriteName;
		}
	}
}
