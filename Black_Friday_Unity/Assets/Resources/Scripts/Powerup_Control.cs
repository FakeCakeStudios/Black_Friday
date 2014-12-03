using UnityEngine;
using System.Collections;

public class Powerup_Control : MonoBehaviour
{
	//public
	public Powerups powerup;
	public float rotateAngle;

	//private
	private Vector3 axis;

	//private
	private Player_Control 	playerScript;
	
	void Start()
	{
		playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Control>();
		axis = new Vector3(0.0f, 1.0f, 0.0f);
	}
	
	void Update()
	{
		this.transform.Rotate(axis, rotateAngle * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			playerScript.PowerupObtained(powerup, this.gameObject.name);
			Destroy(this.gameObject);
		}
	}
}
