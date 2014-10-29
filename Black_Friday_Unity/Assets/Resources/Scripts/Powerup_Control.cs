using UnityEngine;
using System.Collections;

public class Powerup_Control : MonoBehaviour
{
	public Powerups powerup;
	public float rotateAngle;

	private Master_Control masterScript;
	
	void Start()
	{
		masterScript = GameObject.FindGameObjectWithTag("Master").GetComponent<Master_Control>();
	}
	
	void Update()
	{
		Vector3 axis = new Vector3(0.0f, 1.0f, 0.0f);
		this.transform.Rotate(axis, rotateAngle * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			masterScript.PowerupObtained(powerup);
			Destroy(this.gameObject);
		}
	}
}
