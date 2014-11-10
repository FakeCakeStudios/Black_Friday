using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Camera_Control : MonoBehaviour
{
	//private
	private int 			viewIndex;
	private float 			cameraDistance;
	private float 			cameraHeight;
	private float 			cameraTargetHeight;
	private float			viewTimer;
	private bool 			followPlayer;
	private bool			changeView;
	private Transform 		player;
	private Quaternion 		localPos;
	private List<Transform> levelViewPoints;
	private Player_Control	playerScript;

	//public
	public float 			speed;
	public float 			angleAdj;
	public float			timePerView;

	void Awake()
	{
		levelViewPoints = new List<Transform>();
	}

	void Start()
	{
		cameraDistance 		= 2.0f;;
		cameraHeight 		= 2.5f;
		cameraTargetHeight 	= 1.0f;
		viewTimer 			= 0.0f;
		viewIndex 			= 0;
		followPlayer 		= false;
		changeView			= true;
		localPos 			= transform.localRotation;
		playerScript 		= GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Control>();

		GameObject[] viewPoints = GameObject.FindGameObjectsWithTag("View Point");
		for(int i = 0; i < viewPoints.Length; i++)
		{
			levelViewPoints.Add(viewPoints[i].transform);
		}
	}

	void LateUpdate()
	{
		if(followPlayer)
		{
			Vector3 temp 			= player.position;
			temp 					+= -player.forward * cameraDistance;
			temp.y 					+= cameraHeight;
			this.transform.position = temp;
			temp 					= player.position;
			temp.y 					+= cameraTargetHeight;
			this.transform.LookAt(temp);
		}
		else
		{
			if(changeView)
			{
				this.transform.position = levelViewPoints[viewIndex].position;
				this.transform.rotation = levelViewPoints[viewIndex].rotation;
				changeView 				= false;
			}

			viewTimer += Time.deltaTime;
			if(viewTimer >= timePerView)
			{
				viewIndex++;
				viewTimer 	= 0.0f;
				changeView 	= true;
				if(viewIndex >= levelViewPoints.Count)
				{
					followPlayer = true;
					playerScript.SetPlaying(true);
				}
			}
			transform.localRotation = Quaternion.Euler(0, this.transform.eulerAngles.y + (Mathf.Sin(Time.realtimeSinceStartup * speed) * angleAdj), 0);
		}
	}

	public void SetFollowPlayer(bool source)
	{
		followPlayer = source;
	}

	public void SetPlayer()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}
}