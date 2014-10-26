using UnityEngine;
using System.Collections;

public class ItemPickupItem : MonoBehaviour {
	public float rotationSpeed = 5f;
	public itemStuff[] items;
	public itemStuff thisItem;

	void Start(){
		int temp = Mathf.RoundToInt (Random.Range (0, items.Length));
		GameObject clone = Instantiate(items[temp].mesh, transform.position, Quaternion.identity) as GameObject;
		thisItem = items[temp];
		clone.transform.parent = transform;
	}
	void Update(){
		transform.Rotate(0, rotationSpeed*Time.deltaTime, 0, Space.World);

	}
}
[System.Serializable]
public class itemStuff{
	public GameObject mesh;
	public int score;
}