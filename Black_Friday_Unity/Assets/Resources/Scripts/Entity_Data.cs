﻿using UnityEngine;
using System.Collections;

public class Entity_Data : MonoBehaviour
{
	private Transform 		self;
	private Vector3	 		velocity;
	public float			maxRotation;
	public float 			maxSpeed;
	public float 			maxAccel;

	//behavior variables, currently setting up for Shopper1
	private bool 			newDestination;
	private Vector3 		destination;
	//used for stationary guards and the way they should face when at their post
	private Vector3			postRotation;
	private int 			animRepeat;
	private bool 			playingAnim;
	private bool 			stopped;

	//pathing variables
	private int				lastCheckPoint;
	public bool				pathForward;
	public int 				pathRoute;

	//everything but the player will have a personality
	public BehaviorType		behavior;

	//steering will be used to translate every entity after updating
	private SteeringOutput	output;

	//radius of agro distance
	public float 			agroRad;
	
	//has NPC been agro'd by player?
	private bool 			actAgro;

	//collision avoidance variables
	public float[] 			rayDist;
	public float 			viewAngle;
	public float 			collRad;

	public bool 			alive;
	public bool 			doneShopping;

	public float 			timer1;
	public float 			trigger1;
	public float 			timer2;
	public float 			trigger2;

	public Transform GetSelf()
	{
		return self;
	}

	public Vector3 GetVelocity()
	{
		return velocity;
	}

	public Vector3 GetPostRotation()
	{
		return postRotation;
	}

	public void SetNewDestination(bool source)
	{
		newDestination = source;
	}

	public bool GetNewDestination()
	{
		return newDestination;
	}

	public void SetDestination(Vector3 source)
	{
		destination = source;
	}
	
	public Vector3 GetDestination()
	{
		return destination;
	}

	public void SetPlayingAnim(bool source)
	{
		playingAnim = source;
	}
	
	public bool GetPlayingAnim()
	{
		return playingAnim;
	}
	
	public void SetStopped(bool source)
	{
		stopped = source;
	}
	
	public bool GetStopped()
	{
		return stopped;
	}
	
	public void SetAnimRepeat(int source)
	{
		animRepeat = source;
	}
	
	public int GetAnimRepeat()
	{
		return animRepeat;
	}

	public void SetLastCheckPoint(int source)
	{
		lastCheckPoint = source;
	}
	
	public int GetLastCheckPoint()
	{
		return lastCheckPoint;
	}

	public void SetOutput(SteeringOutput source)
	{
		output = source;
	}
	
	public SteeringOutput GetOutput()
	{
		return output;
	}

	public void SetActAgro(bool source)
	{
		actAgro = source;
	}
	
	public bool GetActAgro()
	{
		return actAgro;
	}

	void Awake()
	{
		self 				= this.gameObject.transform;
		output 				= new SteeringOutput();
	}

	// Use this for initialization
	void Start()
	{
		//vector3s
		postRotation 		= self.rotation.eulerAngles;
		destination 		= self.position;

		//bools
		actAgro 			= false;
		newDestination 		= true;
		stopped				= false;
		alive 				= true;
		doneShopping 		= false;

		//ints
		pathRoute 			= 0;
		animRepeat 			= 1;

		//floats
		maxSpeed 			= 3.0f;
		maxRotation 		= 65.0f;
		maxAccel 			= 100.0f;
		timer1 				= 0.0f;
		timer2 				= 0.0f;

		//triggers will be set in the inspector as it means different things for differnt behaviors
		//lastCheckPoint is set by steering during runtime
		//agroRad will be set in inspector for testing
		trigger2 = Random.Range(30, 60);
	}
	
	//thinking of changing this to a custom call so HAL controls when NPC's update, or to let HAL update their decisions and each entity updates their actual location
	void Update()
	{
		self.position += velocity * Time.deltaTime;
		//if we're not stopped then continue movement
		if(!stopped)
		{
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
			self.Rotate(new Vector3(0.0f, 1.0f, 0.0f), output.angle * Time.deltaTime);

			if(output.linear != Vector3.zero)
			{
				//currently all object move in the direction they are facing, no side stepping
				velocity += self.forward * maxAccel * Time.deltaTime;

				if(velocity.magnitude > maxSpeed)
				{
					velocity = Vector3.Normalize(velocity);
					velocity *= maxSpeed;
				}
			}
			else
			{
				velocity = Vector3.zero;
			}
		}
	}

	public void RandomTrigger()
	{
		trigger1 = Random.Range(30.0f, 45.0f) - Random.Range(0.0f, 15.0f);
	}
}
