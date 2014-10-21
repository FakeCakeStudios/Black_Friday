using UnityEngine;
using System.Collections;

public class MyMasterControl : MonoBehaviour {
	public GameObject UI;
	public EntityManager Manager;
	public GameObject playerObject;

	private GameObject[] npcObject;
	private GameObject[] enemyObject;

	private GameObject thePlayer;
	private GameObject playerMesh;

	void Awake(){
		// Creates the entity manager
		Manager = new EntityManager();
		// instantiates the player
		thePlayer = Instantiate(playerObject, transform.position, transform.rotation) as GameObject;
		// names the player's game object
		thePlayer.name = "Player(Prefab)";
	}

	void Start(){
		// Tells the Entity manager to initiate
		Manager.MyStart();
		// tells the Enity manager to add the player entity to it's list
		Manager.Entities.Add (thePlayer.GetComponent<EntityCharacter>());
	}

	void Update(){
		// Tells Entity manager to update it's list of entities
		Manager.MyUpdate();
		Manager.UIUpdate(UI.GetComponent<_SpriteSheetCollector>().UI);
	}
}
