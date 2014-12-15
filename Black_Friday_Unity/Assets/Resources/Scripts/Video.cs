using UnityEngine;
using System.Collections;

public class Video : MonoBehaviour
{
	//MovieTexture credits;
	// Use this for initialization
	void Start()
	{

		//#if UNITY_ANDROID
		Handheld.PlayFullScreenMovie("Assets/Resources/Video/Credits.mp4");
		//#else
		//	credits = renderer.material.mainTexture as MovieTexture;
		//	credits.Play();
		//#endif
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}
}
