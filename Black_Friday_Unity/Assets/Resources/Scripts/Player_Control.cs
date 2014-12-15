using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player_Control : MonoBehaviour
{
	//private
	private float 			currentSpeed;
	private float			cooldown;
	private bool 			leftOn;
	private bool 			rightOn;
	private bool 			powerUp;
	private bool			playing;
	private bool 			usedPowerup;
	private bool			end;
	private Animator		charAnimations;
	private Transform 		self;
	private Vector3	 		velocity;
	private Vector3			target;
	private Master_Control 	masterScript;
	private SteeringOutput	output;
	private List<Powerups>	powerupList;
	private UISprite		powerupDisplay;
	private GameObject		powerupOverlay;
	private GameObject		triggerDrop;
	private float 			animTimer;
	private float 			animTrigger;
	private bool			animPlaying;
	
	//public
	public float			maxRotation;
	public float 			runSpeed;
	public float 			walkSpeed;
	public float 			slowSpeed;
	public float 			maxAccel;
	public Object			boxPrefab;

	void Awake()
	{
		self 			= this.gameObject.transform;
		output 			= new SteeringOutput();
		charAnimations	= this.gameObject.GetComponentInChildren<Animator>();
		masterScript 	= GameObject.FindGameObjectWithTag("Master").GetComponent<Master_Control>();
	}
	
	// Use this for initialization
	void Start()
	{
		runSpeed 		= 9.0f;
		walkSpeed		= 6.0f;
		slowSpeed		= 3.0f;
		maxRotation 	= 100.0f;
		maxAccel 		= 100.0f;
		cooldown 		= 0.0f;
		animTimer 		= 0.0f;
		currentSpeed	= walkSpeed;
		leftOn 			= false;
		rightOn 		= false;
		powerUp 		= false;
		playing 		= false;
		usedPowerup		= false;
		end 			= false;
		animPlaying 	= false;
		target 			= new Vector3();
		powerupList 	= masterScript.GetPlayerData().GetPowerups();
		powerupDisplay	= GameObject.Find("Powerup Button").GetComponentInChildren<UISprite>();
		powerupOverlay  = GameObject.Find("Powerup Overlay");
		triggerDrop 	= GameObject.Find("Trigger Drop");

		powerupOverlay.SetActive(false);
		triggerDrop.SetActive(false);

		charAnimations.SetBool("grab", false);
		charAnimations.SetBool("run", false);
		charAnimations.SetBool("hard", false);
		charAnimations.SetBool("light", false);
		charAnimations.SetBool("sticky", false);
		charAnimations.SetBool("skate", false);
		charAnimations.SetBool("drop", false);
		charAnimations.SetBool("mask", false);
		charAnimations.SetBool("listening", false);
		charAnimations.SetBool("throw", false);
		charAnimations.SetBool("angry", false);
		charAnimations.SetBool("sad", false);
		charAnimations.SetBool("happy", false);
		charAnimations.SetBool("victory", false);
		charAnimations.SetBool("ride", false);
		charAnimations.SetBool("stop", false);
	}

	void Update()
	{
		this.rigidbody.velocity = Vector3.zero;
		this.rigidbody.angularVelocity = Vector3.zero;

		if(playing && !masterScript.GetPause())
		{
			if(animPlaying)
			{
				animTimer += Time.deltaTime;
				if(animTimer >= animTrigger)
				{
					charAnimations.SetBool("drop", false);
					charAnimations.SetBool("mask", false);
					charAnimations.SetBool("hard", false);
					charAnimations.SetBool("light", false);
					animTimer 		= 0.0f;
					currentSpeed 	= runSpeed;
					animPlaying 	= false;
				}
			}

			if(usedPowerup)
			{
				cooldown -= Time.deltaTime;
				if(cooldown <= 0.0f)
				{
					charAnimations.SetBool("skate", false);
					cooldown 	= 0.0f;
					usedPowerup = false;
					currentSpeed = walkSpeed;
					powerupOverlay.SetActive(false);
					SetPowerupDisplay("CoverLayer");
				}
			}

			self.position += velocity * Time.deltaTime;
			//if we're not stopped then continue movement
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

				charAnimations.SetBool("run", true);
						
				if(velocity.magnitude > currentSpeed)
				{
					velocity = Vector3.Normalize(velocity);
					velocity *= currentSpeed;
				}
			}
			else
			{
				charAnimations.SetBool("run", false);
			}
		
			target 			= Vector3.zero;
			target 			= self.position + (self.forward * 2.0f);
			output.angle 	= 0.0f;
			output.linear 	= self.position + (self.forward * 2.0f);

			currentSpeed = walkSpeed;

			if(leftOn && rightOn)
			{
				currentSpeed = slowSpeed;
			}
			else if(leftOn)
			{
				target += -self.right * 2.0f;
				output.angle = -100;
			}
			else if(rightOn)
			{
				target += self.right * 2.0f;
				output.angle = 100;
			}
			if(powerUp && cooldown == 0.0f)
			{
				if(powerupList.Count > 0)
				{
					PowerupUsed();
				}
			}
		}
	}

	public void SetLeft(bool source)
	{
		leftOn = source;
	}
	
	public void SetRight(bool source)
	{
		rightOn = source;
	}
	
	public void SetPowerup(bool source)
	{
		powerUp = source;
	}
	
	void PowerupUsed()
	{
		powerupOverlay.SetActive(true);
		usedPowerup = true;
		switch(powerupList[0])
		{
		case(Powerups.BlackCoffee):
		{
			currentSpeed 	= runSpeed;
			cooldown 		= 10.0f;
			break;
		}
		case(Powerups.Box):
		{
			masterScript.EffectEntities(Interaction.Undetectable, 15.0f);
			cooldown = 15.0f;
			break;
		}
		case(Powerups.Glue):
		{
			charAnimations.SetBool("drop", true);
			animTrigger = 1.083f;
			triggerDrop.SetActive(true);
			Vector3 tempPos 				= self.position;
			tempPos 						+= -self.forward * 2.0f;
			tempPos.y 						+= 0.6f;
			triggerDrop.transform.position 	= tempPos;
			triggerDrop.tag 				= "Slower";
			cooldown 						= 5.0f;
			break;
		}
		case(Powerups.Horn):
		{
			masterScript.EffectEntities(Interaction.Runaway, 8.0f);
			cooldown = 8.0f;
			break;
		}
		case(Powerups.Jawbreakers):
		{
			charAnimations.SetBool("drop", true);
			animTrigger = 1.083f;
			triggerDrop.SetActive(true);
			Vector3 tempPos 				= self.position;
			tempPos 						+= -self.forward * 2.0f;
			tempPos.y 						+= 0.6f;
			triggerDrop.transform.position 	= tempPos;
			triggerDrop.tag 				= "Slower";
			cooldown 						= 300.0f;
			break;
		}
		case(Powerups.Marbles):
		{
			charAnimations.SetBool("drop", true);
			animTrigger = 1.083f;
			triggerDrop.SetActive(true);
			Vector3 tempPos 				= self.position;
			tempPos 						+= self.forward * 3.0f;
			tempPos.y 						+= 0.6f;
			triggerDrop.transform.position 	= tempPos;
			triggerDrop.tag 				= "Stopper";
			cooldown 						= 1.0f;
			break;
		}
		case(Powerups.Mask):
		{
			charAnimations.SetBool("mask", true);
			animTrigger = 1.625f;
			cooldown = 8.0f;
			break;
		}
		case(Powerups.Megacubes):
		{
			charAnimations.SetBool("drop", true);
			animTrigger = 1.083f;
			triggerDrop.SetActive(true);
			Vector3 tempPos 				= self.position;
			tempPos 						+= -self.forward * 2.0f;
			tempPos.y 						+= 0.6f;
			triggerDrop.transform.position 	= tempPos;
			triggerDrop.tag 				= "Stopper";
			cooldown 						= 3.0f;
			break;
		}
		case(Powerups.RollerSkates):
		{
			charAnimations.SetBool("skate", true);
			currentSpeed 	= runSpeed;
			cooldown 		= 30.0f;
			break;
		}
		case(Powerups.StickyHand):
		{
			charAnimations.SetBool("sticky", true);
			animTrigger = 0.417f;
			cooldown = 10.0f;
			break;
		}
		case(Powerups.Tacks):
		{
			charAnimations.SetBool("drop", true);
			animTrigger = 1.083f;
			triggerDrop.SetActive(true);
			Vector3 tempPos 				= self.position;
			tempPos 						+= -self.forward * 2.0f;
			tempPos.y 						+= 0.6f;
			triggerDrop.transform.position 	= tempPos;
			triggerDrop.tag 				= "Slower";
			cooldown 						= 3.0f;
			break;
		}
		}
		powerupList.RemoveAt(0);
	}

	public void SetPlaying(bool source)
	{
		playing = source;
		if(playing)
		{
			masterScript.SetInGame(true);
		}
	}

	public Transform GetSelf()
	{
		return self;
	}
	
	public Vector3 GetVelocity()
	{
		return velocity;
	}
	
	public void SetOutput(SteeringOutput source)
	{
		output = source;
	}
	
	public SteeringOutput GetOutput()
	{
		return output;
	}

	public void PowerupObtained(Powerups source, string name)
	{
		if(powerupList.Count > 0)
		{
			powerupList.RemoveAt(0);
		}
		powerupList.Add(source);
		SetPowerupDisplay(name);
	}

	void SetPowerupDisplay(string source)
	{
		powerupDisplay.spriteName = source;
	}

	public void SetEnd(bool source)
	{
		end = source;
	}

	public bool GetEnd()
	{
		return end;
	}

	public void Crash(Vector3 source)
	{
		float temp = Mathf.Abs(Vector3.Distance(source, velocity));
		if(temp > 6.01f)
		{
			charAnimations.SetBool("hard", true);
			animTrigger = 0.875f;
			currentSpeed = 0.0f;
			animPlaying = true;
		}
		else
		{
			charAnimations.SetBool("light", true);
			animTrigger = 0.333f;
			currentSpeed = walkSpeed;
			animPlaying = true;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Guards")
		{
			masterScript.GameOver();
		}
		else if(other.tag == "EndScene")
		{
			masterScript.RunEndScene();
		}
	}

	void OnTriggerStay(Collider other)
	{
		if(other.tag == "Guards")
		{
			masterScript.GameOver();
		}
		else if(other.tag == "EndScene")
		{
			masterScript.RunEndScene();
		}
	}

	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Shoppers")
		{
			other.gameObject.SendMessage("Crash", this, SendMessageOptions.DontRequireReceiver);
		}
		else
		{
			Crash(Vector3.zero);
		}
	}
}