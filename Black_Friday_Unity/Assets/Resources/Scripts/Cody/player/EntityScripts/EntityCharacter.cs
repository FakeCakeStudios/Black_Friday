using UnityEngine;
using System.Collections;

public class EntityCharacter : MonoBehaviour{
	public static bool camDone = false;

	public CameraOverView OverView;
	public EntityType type;
	//----------------- Character's Mesh ----------------------
	public GameObject CharacterMesh;
	//---------------- Gravity Variables ----------------------
	//public float gravity = 8f;
	//--------------- Speed Rate Variables --------------------
	public float speed = 0;
	public float speedMax = 5f;
	public float speedInc = 0.1f;
	//--------------- Turning Rate Variables --------------------
	public float turn = 0;
	public float turnMax = 3f;
	public float turnInc = 0.1f;
	//-------------- Camera position Variables ------------------
	public Vector3 cameraPosition = new Vector3(0, 3, -7);
	public Vector3 lookAtOffset = new Vector3(0, 1, 0);

	private Vector3 moveDirection = Vector3.zero;
	private Vector3 rotDirection = Vector3.zero;

	private GameObject temp;
	private CharacterController controller;
	private GameObject CameraObject;

	//private Vector2 leftRef;
	private int leftFingerId = -1;
	//private Vector2 rightRef;
	private int rightFingerId = -1;
	private bool leftOn = false;
	private bool rightOn = false;
	private bool powerUp = false;
	private bool shoppingList = false;
	
	void Start()
	{
		CameraObject = Camera.main.transform.gameObject;

		if(CharacterMesh != null){
			//temp = Instantiate(CharacterMesh, this.transform.position, this.transform.rotation) as GameObject;
			//temp.transform.parent = this.transform;
			//temp.name = "Entity(Mesh)";
		}
		OverView.ViewStart();
		CameraObject = Camera.main.gameObject;
	}

	public virtual void EntityControl(){
		if(!OverView.allDone){
			OverView.ViewUpdate(CameraObject);
		}
		else{
			if(!camDone){
				camDone = true;
			}
			else{
				controller = GetComponent<CharacterController>();
				CameraObject.transform.localPosition = this.transform.position + cameraPosition;
				CameraObject.transform.LookAt(this.transform.position + lookAtOffset);


				//--------------- Android Touch Inputs--------------------
				/*
			if(controller.isGrounded){
				if(Input.touchCount>0){
					// Both Buttons Condition
					foreach(Touch touch in Input.touches){
						if(touch.phase == TouchPhase.Began){
								// add origin for this touch to array
								if(UI.LeftSide1.Contains(touch.position) || UI.LeftSide2.Contains(touch.position)){
									// touch instance 1
									leftFingerId = touch.fingerId;
									leftOn = true;// turns boolean on if position is in the left side of the screen
								}
								if(UI.RightSide1.Contains(touch.position) || UI.RightSide2.Contains(touch.position)){
									// touch instance 2
									rightFingerId = touch.fingerId;
									rightOn = true;// turns boolean on if position is in the right side of the screen
								}
							}
							if(touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled){
								if(touch.fingerId == leftFingerId){
									// touch instance 1
									leftFingerId = -1;
									leftOn = false;// turns boolean off if position is in the left side of the screen

								}
								if(touch.fingerId == rightFingerId){
									// touch instance 2
									rightFingerId = -1;
									rightOn = false;// turns boolean off if position is in the left side of the screen
								}
							}
						}
*/
						if(leftOn && rightOn){
							motor(Motor.SlowDown);
						}
						else{
							if(leftOn){
								Direction(Buttons.Left);
							}
							else if(rightOn){
								Direction(Buttons.Right);
							}
							else{
								Direction(Buttons.None);
							}
							motor(Motor.SpeedUp);
						}
					}
				}
				MoveAndRotate();
			}
		//}
	//}
	//--------------- Function to speed up or down --------------------
	public void motor(Motor den){
		if(den == Motor.SpeedUp && speed<speedMax){
			speed+=speedInc;
		}
		if(den == Motor.SlowDown && speed>0){
			speed-=speedInc;
		}
	}
	//--------------- Function that controls turning --------------------
	public void Direction(Buttons dir){
		if(dir == Buttons.Left && turn > -turnMax){
			turn -= turnInc;
		}
		if(dir == Buttons.Right && turn < turnMax){
			turn += turnInc;
		}
		if(dir == Buttons.None){
			turn = 0;
		}
	}
	//--------------- Function called to actually put everthing in motion or rotation  --------------------
	public void MoveAndRotate(){
		if(controller.isGrounded){
			moveDirection = new Vector3(0, 0, speed);
			moveDirection = this.transform.TransformDirection(moveDirection);
			rotDirection = new Vector3(0, turn, 0);
			this.transform.Rotate(rotDirection);
			//rotDirection *= turn;
		}
		//moveDirection.y -= gravity * Time.deltaTime;
		this.controller.Move(moveDirection * Time.deltaTime);
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
	
	public void SetShoppingList(bool source)
		
	{
		shoppingList = source;
	}

}