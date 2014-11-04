using UnityEngine;
using System.Collections;


public class Menu_Control : Scene_Control
{
	// Use this for initialization
	override public void Initialize()
	{
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}
}
