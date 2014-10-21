using UnityEngine;
using System.Collections;

public class PCPLayerController : MonoBehaviour {
	public SpeedsStats speeds;
	public CameraStats cameraSet;

	private CharacterController controller;
	private Vector3 moveDirection;
	private Vector3 rotDirection;

	void Start(){
		controller = GetComponent<CharacterController>();
	}

	void Update(){
		// -------------------------------------------------- //
		// --------------- Camera Settings ------------------ //
		// -------------------------------------------------- //
		Camera.main.transform.localPosition = cameraSet.offset;
		Camera.main.transform.LookAt (cameraSet.cameraObject.transform.position + cameraSet.lookOffset);

		// -------------------------------------------------- //
		// -------------- Rotation Settings ----------------- //
		// -------------------------------------------------- //
		rotDirection = new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
		rotDirection *= speeds.mouseSensitivity;

		// -------------------------------------------------- //
		// --------------- Moving Settings ------------------ //
		// -------------------------------------------------- //
		moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		moveDirection *= speeds.speed;
		moveDirection = transform.TransformDirection(moveDirection);

		moveDirection.y -= speeds.gravity * Time.deltaTime;

		// -------------------------------------------------- //
		// ---------- Actually Moving/Rotating -------------- //
		// -------------------------------------------------- //
		transform.Rotate(new Vector3(0, rotDirection.y, 0));
		cameraSet.cameraObject.transform.Rotate(new Vector3(rotDirection.x, 0, 0));
		controller.Move (moveDirection * Time.deltaTime);
	}
}
