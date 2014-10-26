using UnityEngine;
using System.Collections;

public class Splash_Control : MonoBehaviour
{
	private float timer;
	public float displayTime;

	// Use this for initialization
	void Start()
	{
		timer = 0.0f;
	}
	
	// Update is called once per frame
	void Update()
	{
		timer += Time.deltaTime;

		if(timer > displayTime)
		{
			Application.LoadLevel("Menu");
		}
	}
}
