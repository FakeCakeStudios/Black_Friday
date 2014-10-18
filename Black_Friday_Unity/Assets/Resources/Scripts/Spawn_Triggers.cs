using UnityEngine;
using System.Collections;

/*Description
 * This trigger script is setup for a one time spawn when the player tagged object collides with the collider.
 * Functionality is within the script to allow for more than one spawn from a trigger point with timers and triggers to prevent
 * to quick of spawning. HAL will control creation of the spawn once triggered.
 * */
public class Spawn_Triggers : MonoBehaviour
{
	//number of spawns allowed from this trigger
	public int 			numOfSpawns;

	//number of guards that have spawned from this trigger
	private int 		spawned;

	//timer for trigger use
	private float 		timer;

	//amount of time between spawns is capable of spawning more than one
	public float 		spawnDelay;

	//used to let HAL know to spawn a guard
	private bool 		spawnNPC;

	//used to let HAL know where to spawn the guard from this trigger
	private Vector3 	spawnPos;

	//used to let HAL know the rotation of the guard from this trigger
	private Quaternion 	spawnRot;
	
	void Start()
	{
		//starting values for comparison variables
		numOfSpawns = 1;
		spawned 	= 0;
		timer 		= 0.0f;
		spawnNPC 	= false;

		//all other variables will be set in inspector for now and for testing
	}

	void Update()
	{
		//add change in time to the timer
		timer += Time.deltaTime;
	}

	void OnTriggerEnter(Collider other)
	{
		//only want to continue if the player is what has triggered this
		if(other.gameObject.tag == "Player")
		{
			//if HAL still hasn't spawned the guard from the last call, don't ask for more than one
			if(!spawnNPC)
			{
				//only spawn if more are allowed to be spawned from here and if the time delay has passed
				if(timer > spawnDelay && numOfSpawns > spawned)
				{
					spawned 	+= 1;
					spawnNPC 	= true;
					//collider is turned off, for now we only spawn one max, but functionality is setup for more than one spawn if wanted later
					this.collider.enabled = false;
				}
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		//to try and prevent more than one being spawned from a single penetration of the trigger box, don't reset the timer until the player leaves the trigger box
		if(other.gameObject.tag == "Player")
		{
			timer = 0.0f;
		}
	}

	public bool GetSpawnNPC()
	{
		return spawnNPC;
	}

	public void SetSpawnNPC(bool source)
	{
		spawnNPC = source;
	}

	public int GetNumOfSpawns()
	{
		return numOfSpawns;
	}

	public int GetSpawnCount()
	{
		return spawned;
	}
}
