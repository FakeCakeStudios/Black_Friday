using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//each NPC will have a set of behaviors that they react with according to their behavior type
public enum BehaviorType
{
	Player, Guard1, Guard2, Shopper1, Shopper2, Shopper3
}

public enum Interaction
{
	None, Slow, Stop, Runaway, Undetectable
}

//TODO needs to be completed with the names of the powerups in the game
public enum Powerups
{

}

//TODO needs to be completed with the names of the kart upgrades in the game
public enum KartUpgrades
{

}

//the output format used to maneuver all NPC's
public struct SteeringOutput
{
	public Vector3 	linear;
	public float 	angle;
}

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
}
