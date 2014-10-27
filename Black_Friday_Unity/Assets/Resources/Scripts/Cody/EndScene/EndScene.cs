using UnityEngine;
using System.Collections;

public class EndScene : MonoBehaviour {
	public GameObject PlayerEndSceneObject;
	public Vector3 playerPosition, cameraPosition, scorePosition;
	public float time;

	public void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			End(other.gameObject, Camera.main.gameObject);
		}
	}

	public void End(GameObject player, GameObject camera){
		Master_Control masterControl = GameObject.FindGameObjectWithTag("Master").GetComponent<Master_Control>();
		GameObject temp = Instantiate(PlayerEndSceneObject, playerPosition, Quaternion.identity)as GameObject;
		Destroy(player);
		time = Time.time;
		masterControl.EndScene();
	}
}
