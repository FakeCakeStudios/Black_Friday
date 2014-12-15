using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Master_Control : MonoBehaviour
{
	private GameObject 			thePlayer;
	private Transform 			spawn;
	private bool 				inGame;
	private bool 				pause;
	private bool 				tutorialUp;
	private bool				checkout;
	private bool				lost;
	private int					sceneIndex;
	private HAL 				ai;
	private Player_Data 		player;
	private List<Input_Button> 	buttons;
	private GameObject 			resumeButton;
	private GameObject 			restartButton;
	private GameObject 			quitButton;
	private RunTime				fpsCounter;
	private Level_Control		levelControl;
	
	void Awake()
	{
		//if(Application.loadedLevel == 0)
		//{
			inGame 		= false;
			pause 		= false;
			tutorialUp 	= false;
			checkout 	= false;
			lost 		= false;
			player 		= new Player_Data();
			fpsCounter	= new RunTime();
			sceneIndex = 0;
		//}

		ai = new HAL();
		ai.Initialize();
		player.Initialize();
		InitializeAtLevelLoad();
	}
	
	void Update()
	{
		fpsCounter.MyUpdate();

		if(inGame)
		{
			StartCoroutine(ai.MyUpdate());
		}
		else if(checkout)
		{
			levelControl.RunCheckout();
		}
		else if(lost)
		{
			levelControl.PlayerLost();
		}
	}

	public void InitializeAtLevelLoad()
	{
		fpsCounter.Initialization();
		if(Application.loadedLevel >= 5)
		{
			levelControl = GameObject.Find("Scene Control").GetComponent<Level_Control>();
		}
		else if(Application.loadedLevel == 1)
		{
			inGame 		= false;
			pause 		= false;
			tutorialUp 	= false;
			checkout 	= false;
			lost 		= false;
			sceneIndex = 1;
		}
	}

	void AIUpdate()
	{
		ai.MyUpdate();
	}
	
	public Player_Data GetPlayerData()
	{
		return player;
	}

	public void PowerupObtained(Powerups source)
	{
		player.AddPowerup(source);
	}

	public void AddCash(int source)
	{
		player.AddCash(source);
	}

	public void SubCash(int source)
	{
		player.SubtractCash(source);
	}

	public void LevelSetup()
	{
		spawn = GameObject.FindGameObjectWithTag("Spawn Point").transform;
		CreatePlayer();
		ai.SetPlayer();
		ai.SetupLevel();

		buttons = new List<Input_Button>();
		GameObject[] temp;
		temp 	= new GameObject[GameObject.FindGameObjectsWithTag("Button").Length];
		temp 	= GameObject.FindGameObjectsWithTag("Button");
		for(int i = 0; i < temp.Length; i++)
		{
			buttons.Add(temp[i].GetComponent<Input_Button>()); 
		}
		for(int i = 0; i < temp.Length; i++)
		{
			buttons[i].Initialize(); 
		}

		resumeButton = GameObject.Find("Resume Button");
		resumeButton.SetActive(false);
		restartButton = GameObject.Find("Restart Button");
		restartButton.SetActive(false);
		quitButton = GameObject.Find("Quit Button");
		quitButton.SetActive(false);
	}

	public void SetInGame(bool source)
	{
		inGame = source;
	}

	public bool GetInGame()
	{
		return inGame;
	}

	public void SetPause()
	{
		if(pause)
		{
			pause = false;
			Time.timeScale = 1.0f;
			resumeButton.SetActive(false);
			restartButton.SetActive(false);
			quitButton.SetActive(false);
		}
		else
		{
			pause = true;
			Time.timeScale = 0.0f;
			resumeButton.SetActive(true);
			restartButton.SetActive(true);
			quitButton.SetActive(true);
		}
	}

	public bool GetPause()
	{
		return pause;
	}
	
	public void GameOver()
	{
		GameReset();
		lost 	= true;
	}

	public void AblerResumeButton(bool source)
	{
		resumeButton.SetActive(source);
		quitButton.SetActive(source);
		restartButton.SetActive(source);
	}

	public void SetTurorial(bool source)
	{
		tutorialUp = source;
	}

	public bool GetTutorialUp()
	{
		return tutorialUp;
	}

	public void EffectEntities(Interaction source, float triggerTime)
	{
		ai.SetEntitiesInteractions(source, triggerTime);
	}

	void CreatePlayer()
	{
		thePlayer = Instantiate(Resources.Load("Prefabs/Characters/" + player.GetPlayerModel().ToString() + " and Carts"), spawn.position, spawn.rotation) as GameObject;
		switch(player.GetCartModel())
		{
		case(CartModel.Drifter):
		{
			GameObject.Find("Starter Cart").SetActive(false);
			GameObject.Find("Offroad Cart").SetActive(false);
			break;
		}
		case(CartModel.Offroad):
		{
			GameObject.Find("Starter Cart").SetActive(false);
			GameObject.Find("Drifter Cart").SetActive(false);	
			break;
		}
		case(CartModel.Starter):
		{
			GameObject.Find("Drifter Cart").SetActive(false);
			GameObject.Find("Offroad Cart").SetActive(false);	
			break;
		}
		}
		//Fans, Glove, Oil, Repeller, Scroll, WD4000
		GameObject[] temp = new GameObject[6];
		temp[0] = GameObject.Find("Fans");
		temp[1] = GameObject.Find("Glove");
		temp[2] = GameObject.Find("Oil");
		temp[3] = GameObject.Find("Repeller");
		temp[4] = GameObject.Find("Scroll");
		temp[5] = GameObject.Find("WD4000");

		for(int i = 0; i < temp.Length; i++)
		{
			temp[i].SetActive(false);
		}

		List<KartUpgrades> cartUpgrades = player.GetKartUpgrades();
		for(int i = 0; i < cartUpgrades.Count; i++)
		{
			switch(cartUpgrades[i])
			{
			case(KartUpgrades.Fans):
			{
				temp[0].SetActive(true);
				break;
			}
			case(KartUpgrades.Glove):
			{
				temp[1].SetActive(true);
				break;
			}
			case(KartUpgrades.Oil):
			{
				temp[2].SetActive(true);
				break;
			}
			case(KartUpgrades.Repeller):
			{
				temp[3].SetActive(true);
				break;
			}
			case(KartUpgrades.Scroll):
			{
				temp[4].SetActive(true);
				break;
			}
			case(KartUpgrades.WD4000):
			{
				temp[5].SetActive(true);
				break;
			}
			}
		}
	}

	public void GameReset()
	{
		ai.Initialize();
		inGame 		= false;
		pause 		= false;
		tutorialUp 	= false;
		checkout 	= false;
	}

	public void RunEndScene()
	{
		inGame 		= false;
		checkout 	= true;
		Destroy(thePlayer);
		thePlayer 	= Instantiate(Resources.Load("Prefabs/Characters/" + player.GetPlayerModel().ToString()), ai.GetEndPosition().position, ai.GetEndPosition().rotation)as GameObject;
		thePlayer.SendMessage("SetHappy");
		levelControl.SetupCheckout();
	}
}