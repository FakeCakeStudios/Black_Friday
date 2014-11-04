using UnityEngine;
using System.Collections;

public class CamRotation : MonoBehaviour {
	public float speed;
	public float angleAdj;

	private Quaternion localPos;
	void Start(){
		localPos = transform.localRotation;
	}
	void Update(){
		//if(!EntityCharacter.camDone){
			//transform.localRotation = Quaternion.Euler(0, localPos.eulerAngles.y + (Mathf.Sin(Time.realtimeSinceStartup*speed)*angleAdj), 0);
		//}
	}
}
