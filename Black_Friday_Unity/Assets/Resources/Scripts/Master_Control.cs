using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Master_Control : MonoBehaviour
{
	public GameObject playerObject;
	private GameObject thePlayer;

	private bool inGame;
	private bool pause;

	//AI is currently not in so it is commented out from the master currently
	//artificial intelligence
	private HAL ai;
	
	//this keeps track of player changes from the webstore to apply them at game time
	private Player_Data player;

	private List<Input_Button> buttons;

	//shoulmd only be done one during runtie at the beginning of the application start
	void Awake()
	{
		inGame = false;
		pause = false;

		//only call at the beginning of the application
		ai = new HAL();

		//only call at the beginning of the application
		ai.Initialize();

		//create new instance
		player = new Player_Data();
		player.Initialize();
	}
	
	// Update is called once per frame
	void Update()
	{
		if(inGame)
		{
			ai.MyUpdate();
		}
	}

	//get the data of the player
	public Player_Data GetPlayerData()
	{
		return player;
	}

	public void LevelSetup()
	{
		thePlayer = Instantiate(playerObject, transform.position, transform.rotation) as GameObject;
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
			Debug.Log (temp[i].ToString());
			buttons.Add(temp[i].GetComponent<Input_Button>()); 
		}

		for(int i = 0; i < temp.Length; i++)
		{
			buttons[i].Initialize(); 
		}
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
	
	public void GameOver()
	{
		inGame = false;
		DontDestroyOnLoad(this.gameObject);
		Application.LoadLevel("Menu");
	}
}
