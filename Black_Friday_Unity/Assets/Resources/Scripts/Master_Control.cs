using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Master_Control : MonoBehaviour
{
	public GameObject playerObject;
	private GameObject thePlayer;
	private Transform spawn;

	private bool inGame;
	private bool pause;
	public float endWait = 5;
	private bool endScene = false;
	private EndScene EndSceneObject;
	private bool tutorialUp;

	//AI is currently not in so it is commented out from the master currently
	//artificial intelligence
	private HAL ai;
	
	//this keeps track of player changes from the webstore to apply them at game time
	private Player_Data 	player;
	private Player_Control 	playerControl;

	private List<Input_Button> buttons;

	private GameObject 		resumeButton;

	//shoulmd only be done one during runtie at the beginning of the application start
	void Awake()
	{
		inGame = false;
		pause = false;
		endScene = false;

		//only call at the beginning of the application
		ai = new HAL();

		//only call at the beginning of the application
		ai.Initialize();

		//create new instance
		player = new Player_Data();
		player.Initialize();

		tutorialUp = false;
	}
	
	// Update is called once per frame
	void Update()
	{
		if(endScene){
			Camera.main.transform.position = EndSceneObject.cameraPosition;
			Camera.main.transform.LookAt(EndSceneObject.scorePosition);
			if(Time.time > EndSceneObject.time+endWait){
				Application.LoadLevel(1);
			}
		}
		else if(inGame)
		{
			ai.MyUpdate();
		}
	}

	//get the data of the player
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

		thePlayer = Instantiate(playerObject, spawn.position, spawn.rotation) as GameObject;
		// names the player's game object
		thePlayer.name = "Player(Prefab)";

		//call at the beginning of every level
		ai.SetPlayer();
		
		//call at the beginning of every level
		ai.SetupLevel();

		buttons = new List<Input_Button>();
		GameObject[] temp;
		temp = new GameObject[GameObject.FindGameObjectsWithTag("Button").Length];
		temp = GameObject.FindGameObjectsWithTag("Button");
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

		EndSceneObject = GameObject.FindGameObjectWithTag("EndScene").GetComponent<EndScene>();

		playerControl = thePlayer.GetComponent<Player_Control>();
	}

	public void EndScene(){
		endScene = true;
	}
	public bool isEnd(){
		return endScene;
	}
	public void SetInGame(bool source)
	{
		inGame = source;
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
}
