using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZZZZ : MonoBehaviour
{
	//private
	private float 				minDistance;
	private float 				maxDistance;
	private int 				maxObjects;
	private int 				objectCount;
	private Transform 			playerTransform;
	private List<GameObject> 	sceneObjects;
	private List<GameObject>	npcObjects;

	//public
	public List<Object>			objectPrefabs;
	public List<Object>			npcPrefabs;

	// Use this for initialization
	void Start()
	{
		minDistance 	= 5.0f;
		maxDistance 	= 10.0f;
		maxObjects		= 30;
		objectCount 	= 0;
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		sceneObjects 	= new List<GameObject>();
		npcObjects 		= new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}
}
