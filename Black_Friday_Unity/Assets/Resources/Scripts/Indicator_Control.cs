using UnityEngine;
using System.Collections;

public class Indicator_Control : MonoBehaviour
{
	private Vector3 	target;
	private float 		maxRotation;
	private Transform 	camera;
	private float		distance;
	private float 		height;

	// Use this for initialization
	void Start()
	{
		maxRotation = 360.0f;
		camera 		= GameObject.Find("Main Camera").GetComponent<Transform>();
		distance 	= 3.2f;
		height 		= 1.25f;
	}
	
	// Update is called once per frame
	void Update()
	{
		SteeringOutput output = Steering.Face2D(this.transform, target);

		float temp = Mathf.Abs(output.angle);
		if(temp > maxRotation)
		{
			if(output.angle < 0.0f)
			{
				output.angle = -maxRotation;
			}
			else
			{
				output.angle = maxRotation;
			}
		}
		else if(temp < 1.0f)
		{
			output.angle = 0.0f;
		}
		this.transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f), output.angle * Time.deltaTime);

		Vector3 pos 			= camera.position + (camera.forward * distance);
		pos.y 					+= height;
		this.transform.position = pos;
		//this.transform.LookAt(target);
	}

	public void SetTarget(Vector3 source)
	{
		target = source;
	}
}
