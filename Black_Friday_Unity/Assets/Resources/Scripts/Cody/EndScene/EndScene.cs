using UnityEngine;
using System.Collections;

public class EndScene : MonoBehaviour {
	public Vector3 playerPosition, cameraPosition, scorePosition;

	public void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			End(GameObject.FindGameObjectWithTag("Player").transform, Camera.main.transform);
		}
	}

	public void End(Transform player, Transform camera){
		player.position = playerPosition;
		camera.position = cameraPosition;
		camera.LookAt(scorePosition);
	}
}
