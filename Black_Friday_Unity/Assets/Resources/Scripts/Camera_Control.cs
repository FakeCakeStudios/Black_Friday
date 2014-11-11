using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Camera_Control : MonoBehaviour
{
	//private
	private int 				viewIndex;
	private float 				cameraDistance;
	private float 				cameraHeight;
	private float 				cameraTargetHeight;
	private float				viewTimer;
	private bool 				followPlayer;
	private bool				changeView;
	private Transform 			player;
	private Quaternion 			localPos;
	private List<Transform> 	levelViewPoints;
	private Player_Control		playerScript;
	private Indicator_Control 	itemIndicator;

	//public
	//public float 			speed;
	//public float 			angleAdj;
	public float			timePerView;

	void Awake()
	{
		levelViewPoints = new List<Transform>();
	}

	void Start()
	{
		cameraDistance 		= 2.0f;;
		cameraHeight 		= 2.0f;
		cameraTargetHeight 	= 1.0f;
		viewTimer 			= 0.0f;
		viewIndex 			= 0;
		followPlayer 		= false;
		changeView			= true;
		localPos 			= transform.localRotation;
		playerScript 		= GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Control>();
		itemIndicator		= GameObject.Find("Item Indicator").GetComponent<Indicator_Control>();

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
				Vector3 adjustment = levelViewPoints[viewIndex].forward * 0.5f;
				adjustment += levelViewPoints[viewIndex].position;
				this.transform.position = adjustment;
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
					itemIndicator.SetShown(true);
				}
			}
			//Vector3 rotAxis = new Vector3(0.0f, 1.0f, 0.0f);
			//transform.Rotate(rotAxis, speed * Time.deltaTime);
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