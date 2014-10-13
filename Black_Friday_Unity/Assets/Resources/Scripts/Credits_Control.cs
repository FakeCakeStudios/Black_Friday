using UnityEngine;
using System.Collections;

public class Credits_Control : MonoBehaviour
{
	//bools
	private bool 		back;

	//master game object
	private GameObject 	master;

	// Use this for initialization
	void Start()
	{
		//default values
		back 	= false;

		//obtain master
		master 	= GameObject.FindGameObjectWithTag("Master");
	}
	
	// Update is called once per frame
	void Update()
	{
		//when user input indicates to go back with the back button go back to Main Menu
		if(back)
		{
			DontDestroyOnLoad(master);
			Application.LoadLevel("Menu");
		}
	}
}
