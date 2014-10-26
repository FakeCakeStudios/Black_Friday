using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EntityManager{
	//------------------------------------------------------------------------------------//
	//------------------- Holds and controls all the Entities created --------------------//
	//------------------------------------------------------------------------------------//

	public List<EntityCharacter> Entities;

	public void MyStart(){
		Entities = new List<EntityCharacter>();
		/*
		for(int i=0; i < Entities.Count; i++){
			Entities[i].SetMesh();
			Debug.Log("Entity["+i+"]");
		}
		*/
	}

	public void MyUpdate() {
		for(int i=0; i < Entities.Count; i++){
			Entities[i].EntityControl();
		}
	}
	public void UIUpdate(UIGUI ui){
		for(int i=0; i < Entities.Count; i++){
			Entities[i].UIControl(ui);
		}
	}
}
/*
public enum Buttons{
	Left,
	Right,
	None
}
public enum Motor{
	SpeedUp,
	SlowDown
}
public enum EntityType{
	Player,
	PCplayer,
	GuardOne,
	GuardTwo,
	NPC
}*/
