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
	private int					sceneIndex;
	//public float endWait = 5;
	//private bool endScene = false;
	//private EndScene EndSceneObject;
	private HAL 				ai;
	private Player_Data 		player;
	private List<Input_Button> 	buttons;
	private GameObject 			resumeButton;
	private RunTime				fpsCounter;
	
	void Awake()
	{
		inGame 		= false;
		pause 		= false;
		tutorialUp 	= false;
		player 		= new Player_Data();
		fpsCounter	= new RunTime();
		sceneIndex = 0;
		//endScene = false;
		
		ai = new HAL();
		ai.Initialize();
		player.Initialize();
		InitializeAtLevelLoad();
	}
	
	void Update()
	{
		//if(endScene){
			//Camera.main.transform.position = EndSceneObject.cameraPosition;
			//Camera.main.transform.LookAt(EndSceneObject.scorePosition);
			//if(Time.time > EndSceneObject.time+endWait){
				//Application.LoadLevel(1);
			//}
		//}
		fpsCounter.MyUpdate();

		if(inGame)
		{
			StartCoroutine(ai.MyUpdate());
		}
	}

	public void InitializeAtLevelLoad()
	{
		fpsCounter.Initialization();
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

		//EndSceneObject = GameObject.FindGameObjectWithTag("EndScene").GetComponent<EndScene>();
	}

	//public void EndScene(){
	//	endScene = true;
	//}
	//public bool isEnd(){
	//	return endScene;
	//}

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
		}
		else
		{
			pause = true;
			Time.timeScale = 0.0f;
		}
	}

	public bool GetPause()
	{
		return pause;
	}
	
	public void GameOver()
	{
		inGame = false;
		DontDestroyOnLoad(this.gameObject);
		Application.LoadLevel("Menu");
	}

	public void AblerResumeButton(bool source)
	{
		resumeButton.SetActive(source);
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
		///GameObject[] temp = GameObject.FindGameObjectsWithTag("Cart Upgrade");
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
		temp[2].SetActive(true);
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
}
