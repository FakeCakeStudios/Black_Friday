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
		runSpeed 		= 6.0f;
		walkSpeed		= 3.0f;
		slowSpeed		= 1.5f;
		maxRotation 	= 100.0f;
		maxAccel 		= 100.0f;
		cooldown 		= 0.0f;
		currentSpeed	= walkSpeed;
		leftOn 			= false;
		rightOn 		= false;
		powerUp 		= false;
		playing 		= false;
		usedPowerup		= false;
		target 			= new Vector3();
		powerupList 	= masterScript.GetPlayerData().GetPowerups();
		powerupDisplay	= GameObject.Find("Powerup Button").GetComponentInChildren<UISprite>();
		powerupOverlay  = GameObject.Find("Powerup Overlay");
		triggerDrop 	= GameObject.Find ("Trigger Drop");

		powerupOverlay.SetActive(false);
		triggerDrop.SetActive(false);
	}

	void Update()
	{
		charAnimations.SetBool("DropItem", false);
		charAnimations.SetBool("Mask", false);

		if(playing && !masterScript.GetPause())
		{
			if(usedPowerup)
			{
				cooldown -= Time.deltaTime;
				if(cooldown <= 0.0f)
				{
					cooldown 	= 0.0f;
					usedPowerup = false;
					currentSpeed = walkSpeed;
					powerupOverlay.SetActive(false);
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

				charAnimations.SetBool("running", true);
						
				if(velocity.magnitude > currentSpeed)
				{
					velocity = Vector3.Normalize(velocity);
					velocity *= currentSpeed;
				}
			}
			else
			{
				charAnimations.SetBool("running", false);
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
			charAnimations.SetBool("DropItem", true);
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
			charAnimations.SetBool("DropItem", true);
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
			charAnimations.SetBool("DropItem", true);
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
			charAnimations.SetBool("Mask", true);
			cooldown = 8.0f;
			break;
		}
		case(Powerups.Megacubes):
		{
			charAnimations.SetBool("DropItem", true);
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
			charAnimations.SetBool("Skate", true);
			currentSpeed 	= runSpeed;
			cooldown 		= 30.0f;
			break;
		}
		case(Powerups.StickyHand):
		{
			charAnimations.SetBool("Sticky", true);
			cooldown = 10.0f;
			break;
		}
		case(Powerups.Tacks):
		{
			charAnimations.SetBool("DropItem", true);
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

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Guards")
		{
			//for now just end the game, but need to activate losing animations and what not here before actually going back to menu
			masterScript.GameOver();
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

	public void PowerupObtained(Powerups source)
	{
		powerupList.Add(source);
	}

	public void SetPowerupDisplay(string source)
	{
		powerupDisplay.spriteName = source;
	}
}

