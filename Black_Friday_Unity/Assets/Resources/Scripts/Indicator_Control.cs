using UnityEngine;
using System.Collections;

public class Indicator_Control : MonoBehaviour
{
	private Vector3 	target;
	private float 		maxRotation;
	private Transform 	camera;
	private Vector3 	offset;

	// Use this for initialization
	void Start()
	{
		maxRotation = 25.0f;
		camera = GameObject.Find("Main Camera").GetComponent<Transform>();
		offset = new Vector3(0.0f, 1.5f, 3.2f);
	}
	
	// Update is called once per frame
	void LateUpdate()
	{
		SteeringOutput output = Steering.Face2D(this.transform, target);

		if(Mathf.Abs(output.angle) > maxRotation)
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
		this.transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f), output.angle * Time.deltaTime);

		Vector3 pos 			= camera.position + offset;
		this.transform.position = pos;
	}

	public void SetTarget(Vector3 source)
	{
		target = source;
	}
}
