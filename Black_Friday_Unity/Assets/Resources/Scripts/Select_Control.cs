using UnityEngine;
using System.Collections;

public class Select_Control : MonoBehaviour
{
	//ints
	private int 		selection;

	//bools
	private bool 		back;

	//master game object
	private GameObject 	master;

	// Use this for initialization
	void Start()
	{
		//default values
		selection 	= 0;
		back 		= false;

		//obtain master object
		master 		= GameObject.FindGameObjectWithTag("Master");
	}
	
	//change to appropiate level indicated by user input
	//never destroy the master object
	void Update()
	{
		switch(selection)
		{
		case(1):
		{
			DontDestroyOnLoad(master);
			Application.LoadLevel("Level_1");
			break;
		}
		case(2):
		{
			DontDestroyOnLoad(master);
			Application.LoadLevel("Level_2");
			break;
		}
		case(3):
		{
			//uncomment when level 3 is in the project, but commented out to prevent any errors
			//DontDestroyOnLoad(master);
			//Application.LoadLevel("Level_3");
			break;
		}
		}

		//back changes scene to the Main Menu
		if(back)
		{
			DontDestroyOnLoad(master);
			Application.LoadLevel("Menu");
		}
	}

	public void SetSelection(int source)
	{
		selection = source;
	}

	public void SetBack(bool source)
	{
		back = source;
	}
}
