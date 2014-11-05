using UnityEngine;
using System.Collections;

public class Camera_Control : MonoBehaviour
{
	private float 		cameraDistance;
	private float 		cameraHeight;
	private float 		cameraTargetHeight;
	private Transform 	player;
	private bool 		followPlayer;
	
	void Start()
	{
		cameraDistance 		= 2.0f;;
		cameraHeight 		= 2.0f;
		cameraTargetHeight 	= 1.0f;
		followPlayer 		= false;
	}

	void LateUpdate()
	{
		if(followPlayer)
		{
			Vector3 temp = player.position;
			temp += -player.forward * cameraDistance;
			temp.y += cameraHeight;
			this.transform.position = temp;
			temp = player.position;
			temp.y += cameraTargetHeight;
			this.transform.LookAt(temp);
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
