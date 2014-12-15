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

public enum Powerups
{
	BlackCoffee, Box, Glue, Horn, Jawbreakers, Marbles, Mask, Megacubes, RollerSkates, StickyHand, Tacks
}

public enum KartUpgrades
{
	Fans, Glove, Oil, Repeller, Scroll, WD4000
}

//the output format used to maneuver all NPC's
public struct SteeringOutput
{
	public Vector3 	linear;
	public float 	angle;
}

public enum PlayerModel
{
	Nerd, Psycho
}

public enum CartModel
{
	Starter, Drifter, Offroad
}
