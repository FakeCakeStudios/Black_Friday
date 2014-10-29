using UnityEngine;
using System.Collections;

public class Player_Control : MonoBehaviour
{
	//joshua's
	private Animator		charAnimations;
	private Transform 		self;
	private Vector3	 		velocity;
	public float			maxRotation;
	public float 			runSpeed;
	public float 			walkSpeed;
	public float 			slowSpeed;
	private float 			currentSpeed;
	public float 			maxAccel;
	//cody's
	public static bool camDone = false;
	public CameraOverView OverView;
	private GameObject CameraObject;
	private bool leftOn = false;
	private bool rightOn = false;
	private bool powerUp = false;
	private Vector3 target;

	public float camDistance = 1.0f;
	public float camHeight = 3.0f;
	public float camRaise = 3.0f;

	private Master_Control masterScript;



	//everything but the player will have a personality
	public BehaviorType		behavior;
	
	//steering will be used to translate every entity after updating
	private SteeringOutput	output;

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

	void Awake()
	{
		self 			= this.gameObject.transform;
		output 			= new SteeringOutput();
		charAnimations	= this.gameObject.GetComponent<Animator>();
		masterScript = GameObject.FindGameObjectWithTag("Master").GetComponent<Master_Control>();
	}
	
	// Use this for initialization
	void Start()
	{
		//floats
		runSpeed 			= 6.0f;
		walkSpeed			= 3.0f;
		slowSpeed			= 1.5f;
		currentSpeed		= walkSpeed;
		maxRotation 		= 100.0f;
		maxAccel 			= 100.0f;
		target = new Vector3();
		//camDistance = 1.0f;
		//camHeight = 3.0f;

		OverView.ViewStart();
		CameraObject = Camera.main.gameObject;
	}

	void Update()
	{
		if(!OverView.allDone){
			OverView.ViewUpdate(CameraObject);
		}
		else
		{
			if(!camDone)
			{
				camDone = true;
			}
			else
			{
				if(!masterScript.isEnd()){
					//CameraObject.transform.localPosition = this.transform.position + cameraPosition;
					Vector3 temp = this.transform.position + (-this.transform.forward * camDistance);
					temp.y += camHeight;
					CameraObject.transform.localPosition = temp;
					CameraObject.transform.LookAt(this.transform.position+new Vector3(0,camRaise,0));
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
					
					if(velocity.magnitude > currentSpeed)
					{
						velocity = Vector3.Normalize(velocity);
						velocity *= currentSpeed;
					}
				}
			}
			target = Vector3.zero;
			target = self.position + (self.forward * 2.0f);
			output.angle = 0.0f;
			output.linear = self.position + (self.forward * 2.0f);

			if(leftOn)
			{
				target += -self.right * 2.0f;
				output.angle = -100;
			}
			if(rightOn)
			{
				target += self.right * 2.0f;
				output.angle = 100;
			}
			if(powerUp)
			{
				//not setup yet
			}

			if(target != Vector3.zero)
			{
				//output = Steering.Seek(self.position, target);
			}
			//always look in the direction we are moving
			//output = Steering.AddSteeringOutputs(output, Steering.LookInDir(self, output.linear));
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

	public void PowerupUsed(Powerups source)
	{
		switch(source)
		{
		case(Powerups.Box):
		{

			break;
		}
		case(Powerups.Glue):
		{
			
			break;
		}
		case(Powerups.Horn):
		{
			
			break;
		}
		case(Powerups.Jawbreakers):
		{
			
			break;
		}
		case(Powerups.Marbles):
		{
			
			break;
		}
		case(Powerups.Mask):
		{
			
			break;
		}
		case(Powerups.Megacubes):
		{
			
			break;
		}
		case(Powerups.Repellent):
		{
			
			break;
		}
		case(Powerups.Tacks):
		{
			
			break;
		}
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
}

