using UnityEngine;
using System.Collections;

public class Button_Base : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{
		Initialize();
	}

	virtual public void Initialize()
	{

	}

	virtual public void OnPress(bool isPressed)
	{

	}

	virtual public void OnHover(bool isOver)
	{

	}

	virtual public void OnSelect(bool selected)
	{

	}
}
