using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Select_Control : Scene_Control
{
	private int 			selection;
	private UIPanel 		levelSelectPanel;
	private UIPanel 		characterSelectPanel;
	private bool			levelSelectState;
	private int				levelSelection;
	private List<UISprite> 	levelButtons;
	private List<string>	levelSprites;

	override public void Initialize()
	{
		selection 				= 0;
		levelSelectPanel 		= GameObject.Find("Level Select Panel").GetComponent<UIPanel>();
		characterSelectPanel 	= GameObject.Find("Character Select Panel").GetComponent<UIPanel>();
		levelSelectState 		= true;
		levelSelection 			= 5;
		levelButtons 			= new List<UISprite>();
		levelSprites			= new List<string>();

		levelButtons.Add(GameObject.Find("Current Level Button").GetComponentInChildren<UISprite>());
		levelButtons.Add(GameObject.Find("Top Next Level Button").GetComponentInChildren<UISprite>());
		levelButtons.Add(GameObject.Find("Bottom Next Level Button").GetComponentInChildren<UISprite>());

		for(int i = 0; i < levelButtons.Count; i++)
		{
			levelSprites.Add(levelButtons[i].spriteName);
		}
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
		}
	}

	override public void SceneAction(int source)
	{
		selection = source;
	}

	void ChangePanels()
	{
		if(levelSelectState)
		{
			levelSelectPanel.enabled 		= true;
			characterSelectPanel.enabled 	= false;
		}
		else
		{
			levelSelectPanel.enabled 		= false;
			characterSelectPanel.enabled 	= true;
		}
	}

	void ChangeLevelButtons(int source)
	{
		for(int i = 0; i < levelButtons.Count; i++)
		{
			int index = 0;
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
		switch(levelSprites[source])
		{
		case("purple_32x32"):
		{
			levelSelection = 5;
			break;
		}
		/*TODO Replace these strings, including the one above, to the names of the images used to represent each level
		case("purple_32x32"):
		{
			levelSelection = 6;
			break;
		}
		case("purple_32x32"):
		{
			levelSelection = 7;
			break;
		}
		*/
		}
	}
}
