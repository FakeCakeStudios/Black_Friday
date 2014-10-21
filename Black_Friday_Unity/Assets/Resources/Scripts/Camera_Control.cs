using UnityEngine;
using System.Collections;

public class Camera_Control : MonoBehaviour
{
	private Vector3 offset;
	private Transform player;

	// Use this for initialization
	void Start()
	{
		offset = new Vector3(0.0f, 0.0f, 0.0f);
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void LateUpdate()
	{
		this.transform.position = player.position + offset;
	}
}
