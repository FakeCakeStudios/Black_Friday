using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level_Control : MonoBehaviour
{
	//public
	public int 					levelNumber;
	public float 				playTime;

	//private
	private GameObject 			master;
	private Master_Control 		masterScript;
	private UILabel				uiTimer;

	private List<Vector3> 		itemLocations;
	public Object[]				levelItems;
	private List<GameObject> 	shoppingList;

	void Awake()
	{
		master 			= GameObject.Find("Master Control");
		masterScript 	= master.GetComponent<Master_Control>();
		uiTimer = new UILabel();
		uiTimer 		= GameObject.FindGameObjectWithTag("Timer").GetComponent<UILabel>();

		itemLocations 	= new List<Vector3>();
		shoppingList 	= new List<GameObject>();

		GameObject[] temp = GameObject.FindGameObjectsWithTag("Item Location");
		for(int i = 0 ; i < temp.Length; i++)
		{
			itemLocations.Add(temp[i].transform.position);
			Destroy(temp[i]);
		}

		for(int i = 0; i < temp.Length; i++)
		{
			int index = Random.Range(0, levelItems.Length);
			shoppingList.Add(Instantiate(levelItems[index], itemLocations[i], Quaternion.identity)as GameObject);
		}
	}

	// Use this for initialization
	void Start()
	{
		masterScript.LevelSetup();
		masterScript.SetInGame(true);
	}
	
	// Update is called once per frame
	void Update()
	{
		playTime -= Time.deltaTime;

		if(playTime > 0.0f)
		{
			int minutes = (int)playTime / 60;
			int seconds = (int)playTime % 60;
			uiTimer.text = minutes.ToString() + ":" + seconds.ToString();
		}
	}
}
