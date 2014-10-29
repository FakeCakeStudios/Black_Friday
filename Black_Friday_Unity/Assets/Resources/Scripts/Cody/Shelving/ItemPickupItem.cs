using UnityEngine;
using System.Collections;

public class ItemPickupItem : MonoBehaviour
{
	public itemStuff[] items;
	public itemStuff thisItem;

	void Start(){
		int temp = Mathf.RoundToInt (Random.Range (0, items.Length));
		GameObject clone = Instantiate(items[temp].mesh, transform.position, Quaternion.identity) as GameObject;
		thisItem = items[temp];
		clone.transform.parent = transform;
	}
}

[System.Serializable]
public class itemStuff{
	public Object mesh;
}