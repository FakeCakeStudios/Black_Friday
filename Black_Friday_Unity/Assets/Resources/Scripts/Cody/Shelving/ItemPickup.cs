using UnityEngine;
using System.Collections;

public class ItemPickup : MonoBehaviour {
	public GameObject itemSpawner;
	public float pickUpDistance = 1f;
	private _ItemSpawner spawner;
	//private ScoreBoard score;
	void Awake(){
		spawner = itemSpawner.GetComponent<_ItemSpawner>();
		//score = GetComponent<ScoreBoard> ();
	}
	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "item"){
			//ScoreBoard.score += other.gameObject.GetComponent<ItemPickupItem>().thisItem.score;
			Destroy(other.gameObject);
		}
	}
}
