using UnityEngine;
using System.Collections;

public class Camera_Control : MonoBehaviour
{
	private Vector3 	offset;
	private Transform 	player;
	private bool 		active;


	// Use this for initialization
	void Start()
	{
		offset = new Vector3(0.0f, 0.0f, 0.0f);
		player = GameObject.FindGameObjectWithTag("Player").transform;
		active = false;
	}
	
	// Update is called once per frame
	void LateUpdate()
	{
		if(active)
		{
			this.transform.position = player.position + offset;
		}
	}

	public void SetActive(bool source)
	{
		active = source;
	}
}
