using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//this is the base class for all behaviorr types, being;
//guard1, guard2, shopper1
public class Behavior
{
	//public Transform self;

	//virtual function to b overridden by child classes to polymorph
	virtual public void Initialize()
	{
	}

	//use this function after creating an instance of this object
	public void Start()
	{
		//base class calls its children's Start function, so never need to call Start
		Initialize();
	}
}