using UnityEngine;
using System.Collections;

public class Powerup_Control : MonoBehaviour
{
	//public
	public Powerups powerup;
	public float rotateAngle;

	//private
	private Player_Control 	playerScript;
	
	void Start()
	{
		playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Control>();
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
			playerScript.PowerupObtained(powerup);
			playerScript.SetPowerupDisplay(this.gameObject.name);
			Destroy(this.gameObject);
		}
	}
}
