using UnityEngine;
using System.Collections;

public class Splash_Control : Scene_Control
{
	private float timer;
	public float displayTime;

	// Use this for initialization
	override public void Initialize()
	{
		timer = 0.0f;
	}
	
	// Update is called once per frame
	override public void MyUpdate()
	{
		timer += Time.deltaTime;

		if(timer > displayTime)
		{
			SetSelection(1);
		}
	}
}
