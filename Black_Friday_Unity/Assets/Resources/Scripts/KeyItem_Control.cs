using UnityEngine;
using System.Collections;

public class KeyItem_Control : MonoBehaviour
{
	public float 			rotateAngle;
	public int 				savings;
	private bool 			active;

	private MeshRenderer 	selfMats;
	public string 			listName;
	private Level_Control	levelControl;

	void Awake()
	{
		SetActive(false);
		levelControl 		= GameObject.Find("Scene Control").GetComponent<Level_Control>();
	}

	void Update()
	{
		if(active)
		{
			Vector3 axis = new Vector3(0.0f, 1.0f, 0.0f);
			this.transform.Rotate(axis, rotateAngle * Time.deltaTime);
		}
	}

	public int GetSavings()
	{
		return savings;
	}

	public void SetActive(bool source)
	{
		active = source;
		if(active)
		{
			this.gameObject.SetActive(true);
		}
		else
		{
			this.gameObject.SetActive(false);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			levelControl.CollectedItem(savings);
		}
		else if(other.gameObject.tag == "Scanner")
		{
	
		}
	}
}
