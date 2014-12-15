using UnityEngine;
using System.Collections;

public class Scene_Control : MonoBehaviour
{
	//master game object
	public GameObject 		master;
	public Master_Control 	masterScript;

	// Use this for initialization
	void Start()
	{
		//obtain the master object
		if(GameObject.FindGameObjectWithTag("Master") != null)
		{
			master 			= GameObject.FindGameObjectWithTag("Master");
			masterScript 	= master.GetComponent<Master_Control>();
		}
		masterScript.InitializeAtLevelLoad();
		Initialize();
	}
	
	// Update is called once per frame
	void Update()
	{
		MyUpdate();
	}

	public void SetSelection(int source)
	{
		if(source <= 7)
		{
			if(source != -1)
			{
				masterScript.SetInGame(false);

				DontDestroyOnLoad(master);
				Application.LoadLevel(source);
			}
			else
			{
				Screen.sleepTimeout = SleepTimeout.SystemSetting;
				Application.Quit();
			}
		}
		else
		{
			SceneAction(source);
		}
	}

	public virtual void Initialize(){}

	public virtual void MyUpdate(){}

	public virtual void SceneAction(int source){}
}
