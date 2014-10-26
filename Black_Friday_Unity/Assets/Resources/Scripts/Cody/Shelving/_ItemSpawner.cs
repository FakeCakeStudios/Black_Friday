using UnityEngine;
using System.Collections;

public class _ItemSpawner : MonoBehaviour {
	public GameObject itemPrefab;
	public Vector3[] itemLocations;
	public GameObject[] itemsInScene;
	
	void Awake(){
		itemsInScene = GameObject.FindGameObjectsWithTag("itemLocations");
		itemLocations = new Vector3[itemsInScene.Length];

		for(int loc=0; loc<itemsInScene.Length; loc++){
			itemLocations[loc] = itemsInScene[loc].transform.position;
		}
		for(int item=0; item<itemLocations.Length; item++){
			GameObject pClone = Instantiate(itemPrefab, itemLocations[item], Quaternion.identity) as GameObject;
			pClone.transform.parent = transform;
			pClone.name =  string.Format("ItemPrefab (Clone #{0})", item);
			Destroy(itemsInScene[item]);
		}
	}
}