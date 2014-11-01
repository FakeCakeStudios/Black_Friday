using UnityEngine;
using System.Collections;

public class Tutorial_Triggers : MonoBehaviour
{
	public string 			tutorialMessage;
	private UILabel			uiTextMessage;
	private Master_Control 	masterScript;

	// Use this for initialization
	void Start()
	{
		uiTextMessage = GameObject.Find("Tutorial").GetComponent<UILabel>();
		masterScript = GameObject.FindGameObjectWithTag("Master").GetComponent<Master_Control>();
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			uiTextMessage.text = tutorialMessage;
			masterScript.SetPause();
			masterScript.SetTurorial(true);
			masterScript.AblerResumeButton(true);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.tag == "Player")
		{
			uiTextMessage.text = "";
			Destroy(this.gameObject);
		}
	}


	public void DestroyTrigger()
	{
		Destroy(this.gameObject);
	}
}
