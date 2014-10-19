using UnityEngine;
using System.Collections;

public class Master_Control : MonoBehaviour
{
	private bool inGame;

	//AI is currently not in so it is commented out from the master currently
	//artificial intelligence
	private HAL ai;
	
	//this keeps track of player changes from the webstore to apply them at game time
	private Player_Data player;

	//shoulmd only be done one during runtie at the beginning of the application start
	void Start()
	{
		inGame = false;

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
		//call at the beginning of every level
		ai.SetPlayer();
		
		//call at the beginning of every level
		ai.SetupLevel();
	}

	public void SetInGame(bool source)
	{
		inGame = source;
	}
}
