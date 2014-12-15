using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Entity_Data : MonoBehaviour
{
	private Animator			charAnimations;
	private Transform 			self;
	private Vector3	 			velocity;
	private float 				currentSpeed;
	private bool 				newDestination;
	private Vector3 			destination;
	private Vector3				postRotation;
	private int 				animRepeat;
	private bool 				playingAnim;
	private float 				animTimer;
	private float 				animTrigger;
	private bool 				slowed;
	private bool 				tackle;
	private int					lastCheckPoint;
	private SteeringOutput		output;
	private bool 				actAgro;
	private List<Interaction> 	action;
	private List<float>			actionTimers;
	private List<float>			actionTriggers;
	private Vector3				search;

	public float			maxRotation;
	public float 			runSpeed;
	public float 			walkSpeed;
	public float 			slowSpeed;
	public float 			maxAccel;
	public bool 			stopped;
	public bool				pathForward;
	public int 				pathRoute;
	public BehaviorType		behavior;
	public float 			agroRad;
	public float[] 			rayDist;
	public float 			viewAngle;
	public float 			collRad;
	public bool 			alive;
	public bool 			doneShopping;
	public float 			timer1;
	public float 			trigger1;
	public float 			timer2;
	public float 			trigger2;
	public float			stopTimer;
	public float			stopTrigger;
	public bool 			blocked;

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
		if(actAgro)
		{
			currentSpeed = runSpeed;
		}
		else
		{
			currentSpeed = walkSpeed;
		}
	}
	
	public bool GetActAgro()
	{
		return actAgro;
	}

	public List<Interaction> GetAction()
	{
		return action;
	}
	
	public void SetAction(Interaction source, float trigger)
	{
		action.Add(source);
		actionTriggers.Add(trigger);
		actionTimers.Add(0.0f);
		if(source == Interaction.Stop)
		{
			stopped = true;
		}
		else if(source == Interaction.Slow)
		{
			currentSpeed = slowSpeed;
			slowed = true;
		}
	}

	public void SetTackle(bool source)
	{
		tackle = source;
		charAnimations.SetBool("tackle", true);
	}

	public void AddTime()
	{
		for(int i = 0; i < actionTimers.Count; i++)
		{
			actionTimers[i] += Time.deltaTime;

			if(actionTimers[i] >= actionTriggers[i])
			{
				if(action[i] == Interaction.Stop)
				{
					stopped = false;
				}
				else if(action[i] == Interaction.Slow)
				{
					currentSpeed = walkSpeed;
					slowed = false;
				}
				actionTimers.RemoveAt(i);
				actionTriggers.RemoveAt(i);
				action.RemoveAt(i);
			}
		}
	}
	
	void Awake()
	{
		self 			= this.gameObject.transform;
		output 			= new SteeringOutput();
		action 			= new List<Interaction>();
		actionTimers 	= new List<float>();
		actionTriggers 	= new List<float>();
		charAnimations	= this.gameObject.GetComponent<Animator>();
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
		slowed 				= false;
		tackle 				= false;
		blocked 			= false;

		//ints
		if(this.gameObject.tag == "Shoppers")
		{
			pathRoute = Random.Range(0, 2);
		}
		else
		{
			pathRoute 		= 0;
		}
		animRepeat 			= 1;

		//floats
		runSpeed 			= 3.0f;
		walkSpeed			= 3.0f;
		slowSpeed			= 1.5f;
		currentSpeed		= walkSpeed;
		maxRotation 		= 100.0f;
		maxAccel 			= 100.0f;
		timer1 				= 0.0f;
		timer2 				= 0.0f;
		stopTimer			= 0.0f;
		animTimer  			= 0.0f;
		trigger2 			= Random.Range(120, 180);
	}

	public void SetGrab()
	{
		charAnimations.SetBool("grab", true);

	}

	public void SetInspect()
	{
		charAnimations.SetBool("inspect", true);
	}

	public void Crash(Player_Control source)
	{
		charAnimations.SetBool("fall", true);
		stopped = true;
		source.Crash(velocity);
		animTrigger = 0.1f;
	}

	public void SetTaunt()
	{
		charAnimations.SetBool("taunt", true);
	}
	public void SetBlock()
	{
		charAnimations.SetBool("block", true);
	}
	public void SetDodge()
	{
		charAnimations.SetBool("dodge", true);
	}
	public void SetEmo()
	{
		charAnimations.SetBool("emo", true);
	}
	public void SetAttack(int source)
	{
		charAnimations.SetInteger("attack", source);
	}
	public void SetTackle()
	{
		charAnimations.SetBool("tackle", true);
	}
	public void SetLook()
	{
		charAnimations.SetBool("look", true);
	}
	public void SetListen()
	{
		charAnimations.SetBool("listen", true);
	}
	public void SetRight()
	{
		charAnimations.SetBool("right", true);
	}
	public void SetLeft()
	{
		charAnimations.SetBool("left", true);
	}

	void Update()
	{
		self.position += velocity * Time.deltaTime;
	
		if(this.tag == "Shoppers")
		{
			if(this.name.Contains("Fat Shopper"))
			{
				charAnimations.SetBool("taunt", false);
			}
			else if(this.name.Contains("Bob"))
			{
				charAnimations.SetBool("block", false);
				charAnimations.SetBool("dodge", false);
				charAnimations.SetBool("emo", false);
				charAnimations.SetInteger("attack", 0);
			}
			else
			{
				charAnimations.SetBool("grab", false);
				charAnimations.SetBool("inspect", false);
				charAnimations.SetBool("fall", false);
			}
		}
		else if(this.tag == "Guards")
		{

			if(this.name.Contains("Fat Shopper"))
			{
				charAnimations.SetBool("tackle", false);
				charAnimations.SetBool("look", false);
				charAnimations.SetBool("listen", false);
			}
			else if(this.name.Contains("Bob"))
			{
				charAnimations.SetBool("left", false);
				charAnimations.SetBool("right", false);
			}
		}
	
		if(!stopped)
		{
			if(!tackle)
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
			}
		
			if(output.linear != Vector3.zero)
			{
				//currently all object move in the direction they are facing, no side stepping
				velocity += self.forward * maxAccel * Time.deltaTime;

				if(velocity.magnitude > currentSpeed)
				{
					velocity = Vector3.Normalize(velocity);
					velocity *= currentSpeed;
				}
				if(actAgro && !slowed)
				{
					charAnimations.SetBool("run", true);
				}
				else
				{
					charAnimations.SetBool("walk", true);
				}
			}
			else
			{
				velocity = Vector3.zero;
				charAnimations.SetBool("walk", false);
				charAnimations.SetBool("run", false);
			}
		}
		else
		{
			charAnimations.SetBool("fall", false);
			if(!blocked)
			{
				stopTimer += Time.deltaTime;
				if(stopTimer >= stopTrigger)
				{
					stopped = false;
				}
			}
			else
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
			}
			velocity = Vector3.zero;
			charAnimations.SetBool("walk", false);
			charAnimations.SetBool("run", false);
		}
	}

	public void RandomTrigger()
	{
		trigger1 = Random.Range(30.0f, 60.0f) - Random.Range(0.0f, 15.0f);
	}

	public void RandomizeStopTrigger()
	{
		stopTrigger = Random.Range(15.0f, 30.0f) - Random.Range(0.0f, 15.0f);
	}

	void OnTriggerStay(Collider other)
	{
		if(!slowed && !stopped)
		{
			if(other.tag == "Slower")
			{
				SetAction(Interaction.Slow, 5.0f);
			}
			else if(other.tag == "Stopper")
			{
				SetAction(Interaction.Stop, 5.0f);
			}
		}
	}
}
