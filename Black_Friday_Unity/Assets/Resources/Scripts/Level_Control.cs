using UnityEngine;
using System.Collections;

public class Level_Control : MonoBehaviour
{
	//public
	public int levelNumber;

	//private
	private GameObject 		master;
	private Master_Control 	masterScript;

	void Awake()
	{
		//master = GameObject.Find("Master Control");
		//masterScript = master.GetComponent<Master_Control>();
	}

	// Use this for initialization
	void Start()
	{
		//masterScript.LevelSetup();
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}
}
