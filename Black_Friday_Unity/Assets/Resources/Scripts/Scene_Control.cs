using UnityEngine;
using System.Collections;

public class Scene_Control : MonoBehaviour
{
	//master game object
	public GameObject master;

	// Use this for initialization
	void Start()
	{
		//obtain the master object
		master = GameObject.FindGameObjectWithTag("Master");

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
