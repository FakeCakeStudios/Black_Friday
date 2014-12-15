using UnityEngine;
using System.Collections;

public class Player_Victory : MonoBehaviour
{
	private Animator		charAnimations;

	// Use this for initialization
	void Start()
	{
		charAnimations	= this.gameObject.GetComponentInChildren<Animator>();
		//when animation is connected, set this to loop the victory animation
	}
}
