using UnityEngine;
using System.Collections;

public class RunTime
{
	private float 	fpsTimer;
	private float 	fpsTrigger;
	private int 	fps;
	
	// Use this for initialization
	public void Initialization()
	{
		fpsTimer 	= 0.0f;
		fpsTrigger 	= 1.0f;
		fps 		= 0;
	}
	
	// Update is called once per frame
	public void MyUpdate()
	{
		fpsTimer 	+= Time.deltaTime;
		fps 		+= 1;
		if(fpsTimer >= fpsTrigger)
		{
			Debug.Log(fps.ToString());
			fpsTimer -= 1.0f;
			fps = 0;
		}
	}

	public int GetFPS()
	{
		return fps;
	}
}
