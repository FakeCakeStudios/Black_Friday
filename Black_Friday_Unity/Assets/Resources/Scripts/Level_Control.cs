using UnityEngine;
using System.Collections;

public class Level_Control : MonoBehaviour
{
	//public
	public int 		levelNumber;
	public float 	playTime;

	//private
	private GameObject 		master;
	private Master_Control 	masterScript;
	private UILabel			uiTimer;

	void Awake()
	{
		master 			= GameObject.Find("Master Control");
		masterScript 	= master.GetComponent<Master_Control>();
		uiTimer = new UILabel();
		uiTimer 		= GameObject.FindGameObjectWithTag("Timer").GetComponent<UILabel>();
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
