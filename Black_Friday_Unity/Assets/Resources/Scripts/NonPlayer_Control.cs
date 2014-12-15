using UnityEngine;
using System.Collections;

public class NonPlayer_Control : MonoBehaviour
{
	public float 			rotationSpeed;
	private Animator		charAnimations;

	// Use this for initialization
	void Start()
	{
		charAnimations	= this.gameObject.GetComponentInChildren<Animator>();
		charAnimations.SetBool("idle2", true);
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}

	public void SetHappy()
	{
		charAnimations.SetBool("happy", true);
	}
}
