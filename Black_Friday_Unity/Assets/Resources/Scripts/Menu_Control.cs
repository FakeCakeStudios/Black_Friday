using UnityEngine;
using System.Collections;

public class Menu_Control : MonoBehaviour
{
	//bools
	private bool 		credits,
						play,
						webstore;

	//master game object
	private GameObject 	master;

	// Use this for initialization
	void Start()
	{
		//default values
		credits 	= false;
		play 		= false;
		webstore 	= false;

		//obtain the master object
		master = GameObject.FindGameObjectWithTag("Master");
	}
	
	// Update is called once per frame
	void Update()
	{
		//change scenes on user input
		//never destroy the master object
		if(credits)
		{
			DontDestroyOnLoad(master);
			Application.LoadLevel("Credits");
		}
		if(play)
		{
			DontDestroyOnLoad(master);
			Application.LoadLevel("Select");
		}
		if(webstore)
		{
			DontDestroyOnLoad(master);
			Application.LoadLevel("Webstore");
		}
	}
}
