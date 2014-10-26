//written by Joshua Murrill
//8/12/2014
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/* All steering functions return a struct of SteeringOutput
 * All functions return a coordinates of a desired position for that entity
 * It is assumed physics of acceleration will be applied later to move each entity
 * srcPos 	= source position
 * tarPos 	= target position
 * srcVel	= source velocity
 * tarVel 	= target velocity
 * srcRot	= source rotation
 * tarRot	= target rotation
 * srcEul	= source euler angles
 * tarEul	= target euler angles
 * tarRad	= target radius
 * time2Tar = time to target
 * dest 	= destination
 * dir		= direction
 * dist		= distance
 * rot		= rotation
 * maxPred 	= maximum prediction
 * pred		= predicition
 * len		= length
*/



public static class Steering
{
	//base steering function used in later combintory steering functions

	//returns a direction from currernt position to a target position
	public static SteeringOutput Seek(Vector3 srcPos, Vector3 tarPos)
	#region
	{
		SteeringOutput output 	= new SteeringOutput();
		output.linear 			= tarPos - srcPos;
		output.linear 			= Vector3.Normalize(output.linear);
		output.angle 			= 0.0f;
		return output;
	}
	#endregion
		
	//returns a direction away from a target position
	public static SteeringOutput Flee(Vector3 srcPos, Vector3 tarPos)
	#region
	{
		SteeringOutput output 	= new SteeringOutput();
		output.linear 			= srcPos - tarPos;
		output.linear 			= Vector3.Normalize(output.linear);
		output.angle 			= 0.0f;
		return output;
	}
	#endregion

	//zeros output if position is close enough to target position, prevents jittering
	private static SteeringOutput Arrive(SteeringOutput output, float distance)
	#region
	{
		//distance to be within to stop going any further
		float tarRad 			= 2.0f;
	
		//zero linear output if within target radius of target position
		if(distance < tarRad)
		{
			output.linear 	= Vector3.zero;
			output.angle 	= 0.0f;
			return output;
		}

		//make sure to normalize linear
		//output.linear 	= Vector3.Normalize(output.linear);
		output.angle 	= 0.0f;
		return output;
	}
	#endregion
	
	//combonation functions that make use of other base functions for more complex actions

	//returns output to reach the desired position using Seek and Arrive
	public static SteeringOutput Pursue(Vector3 srcPos, Vector3 tarPos, Vector3 srcVel, Vector3 tarVel)
	#region
	{
		//Pursue is also a step above what seek does as it will try to predict where the current object will be next and Seeks that position
		float maxPred 	= 2.0f;
		float pred 		= 0.0f;
		float dist 		= Vector3.Distance(tarPos, srcPos);
		float speed 	= Vector3.Magnitude(srcVel);

		//if target is far enough away, maximize the prediction time of where it will be
		if(speed <= dist / maxPred)
		{
			pred = maxPred;
		}
		//otherwise adjust prediction by speed and distance to target
		else
		{
			pred = dist / speed;
		}
		return Arrive(Seek(srcPos, tarPos + tarVel * pred), dist);
	}
	#endregion

	//returns output in opposite direction of target position using Flee with prediction as Pursue
	public static SteeringOutput Evade(Vector3 srcPos, Vector3 tarPos, Vector3 srcVel, Vector3 tarVel)
	#region
	{
		float maxPred 	= 2.0f;
		float pred 		= 0.0f;
		float dist 		= Vector3.Distance(tarPos, srcPos);
		float speed 	= Vector3.Magnitude(srcVel);

		//if target is far enough away, maximize the prediction time of where it will be
		if(speed <= dist / maxPred)
		{
			pred = maxPred;
		}

		//otherwise adjust prediction by speed and distance to target
		else
		{
			pred = dist / speed;
		}
		return Flee(srcPos, tarPos+ tarVel * pred);
	}
	#endregion

	//when called wander will provide a new orientation direction for the entity to go, do not call every update
	public static SteeringOutput Wander(Vector3 srcPos)
	#region
	{
		float maxDistance = Random.Range(0, 10.0f);
		Vector3 wanderTo = new Vector3(Random.Range(-1, 1), 0.0f, Random.Range (-1, 1));
		wanderTo = Vector3.Normalize(wanderTo);
		wanderTo *= maxDistance;
		wanderTo += srcPos;
		wanderTo.y = 0.1f;
		float dist = Vector3.Distance(srcPos, wanderTo);
		return Arrive(Seek(srcPos, wanderTo), dist);
	}
	#endregion

	//align the orientation of the current entity with that of the target, current will look in the same direction as target, not at target
	public static SteeringOutput Align2D(Vector3 srcEul, Vector3 tarEul)
	#region
	{
		SteeringOutput output 	= new SteeringOutput();
		float tarRad 			= 0.02f;
		output.linear	 		= Vector3.zero;

		//in 2.5 dimensions only need to deal with the rotation among the Y axis
		output.angle 			= tarEul.y - srcEul.y;
		if(output.angle > 180.0f)
		{
			//rotate in negative direction when angle is greater than 180.0f
			output.angle *= -1.0f;
		}

		float rotSize = Mathf.Abs(output.angle);

		//if within target radius, stop rotating
		if(rotSize <= tarRad)
		{
			output.angle = 0.0f;
			return output;
		}
		return output;
	}
	#endregion

	//velocity matching will have the current entity match the velocity of the target, meaning that it will travel in the same direction as the target, not towards it
	public static SteeringOutput VelocityMatch(Vector3 srcVel, Vector3 tarVel)
	#region
	{
		SteeringOutput output 	= new SteeringOutput();
		output.linear 			= tarVel - srcVel;
		output.angle 			= 0.0f;
		output.linear 			= Vector3.Normalize(output.linear);
		return output;
	}
	#endregion

	//returns an angle to look towards the target position
	public static SteeringOutput Face2D(Transform source, Vector3 tarPos)
	#region
	{
		SteeringOutput output = new SteeringOutput();
		Vector3 direction 	= tarPos - source.position;

		//obtain the angle from the entity's facing to where we want to face
		output.angle = Vector3.Angle(source.forward, direction);

		//create a point on the left and right of current entity to determine which direction to rotate
		Vector3 rightPoint = source.right * 2.0f + source.position;
		Vector3 leftPoint = -source.right * 2.0f + source.position;

		//obtain distance to each point in vector form
		rightPoint = tarPos - rightPoint;
		leftPoint = tarPos - leftPoint;

		//figure the distance in float form
		float rightDist = Vector3.Magnitude(rightPoint);
		float leftDist = Vector3.Magnitude(leftPoint);

		//if within an amount, then we are already facing the correct direction
		if(output.angle <= 0.02f)
		{
			output.angle = 0.0f;
			return output;
		}

		//if distance is closer to the left point than the right, reverse the angle direction to get there quicker
		if(leftDist < rightDist)
		{
			output.angle *= -1.0f;
		}
		return output;
	}
	#endregion
		
	public static SteeringOutput LookInDir(Transform source, Vector3 linear)
	#region
	{
		SteeringOutput output = new SteeringOutput();
		float len = Vector3.Magnitude(linear);
		if(len == 0.0f)
		{
			output.linear = Vector3.zero;
			output.angle = 0.0f;
			return output;
		}
		Vector3 pos = source.position + linear;
		return Face2D(source, pos);
	}
	#endregion

	public static SteeringOutput FollowPath(Entity_Data source, Path path)
	#region
	{
		SteeringOutput output = new SteeringOutput();

		int polarity;

		if(source.pathForward)
		{
			polarity = 1;
		}
		else
		{
			polarity = -1;
		}

		int nextPoint = source.GetLastCheckPoint() + polarity;

		if(nextPoint > (path.checkPoints.Count - 1))
		{
			nextPoint = 0;
		}
		else if(nextPoint < 0)
		{
			nextPoint = path.checkPoints.Count - 1;
		}
		output = Pursue(source.GetSelf().position, path.checkPoints[nextPoint].self, source.GetVelocity(), Vector3.zero);

		if(output.linear == Vector3.zero)
		{
			source.SetLastCheckPoint(nextPoint);
		}
		return output;
	}
	#endregion

	//use this function for seperation of herding or flocking behaviors
	public static SteeringOutput Seperation(Entity_Data srcPos, List<Entity_Data> tarPos)
	#region
	{
		float decayCoeff = 2.0f;
		SteeringOutput output = new SteeringOutput();

		for(int i = 0; i < tarPos.Count; i++)
		{
			Vector3 direction = tarPos[i].GetSelf().position - srcPos.GetSelf().position;
			float distance = Vector3.Magnitude(direction);

			float strength = decayCoeff / (distance * distance);
			direction = Vector3.Normalize(direction);
			output.linear = strength * direction;
		}
		return output;
	}
	#endregion

	public static SteeringOutput CollisionAvoidance(Entity_Data srcPos, List<Entity_Data> tarPos)
	#region
	{
		SteeringOutput output 	= new SteeringOutput();

		float collRad 			= 1.0f;

		float firstMinSep 		= 0.0f;
		float firstDist = 0.0f;
		Vector3 firstPos 		= Vector3.zero;
		Vector3 firstVel		= Vector3.zero;
		float shortTime 		= Mathf.Infinity;

		float minSep;
		float dist = 0.0f;
		Vector3 pos = Vector3.zero;
		Vector3 vel = Vector3.zero;
		float speed;
		float collTime;

		for(int i = 0; i < tarPos.Count; i++)
		{
			pos = tarPos[i].GetSelf().position - srcPos.GetSelf().position;
			vel = tarPos[i].GetVelocity() - srcPos.GetVelocity();
			speed = Vector3.Magnitude(vel);
			collTime = Vector3.Dot(pos, vel) / (speed * speed);
			dist = Vector3.Magnitude(pos);
			minSep = dist - speed * shortTime;

			if(minSep <= 2 * collRad)
			{
				return output;
			}

			if(collTime > 0.0f && collTime < shortTime)
			{
				shortTime 	= collTime;
				firstMinSep = minSep;
				firstDist 	= dist;
				firstPos 	= pos;
				firstVel 	= vel;
			}
		}

		if(firstMinSep > 0.0f || firstDist >= 2 * collRad)
		{
			pos = firstPos + firstVel * shortTime;
		}

		pos = Vector3.Normalize(pos);
		output.linear = pos;
		return output;
	}
	#endregion

	//returns a SteeringOutput that is the sum of the two parameters
	public static SteeringOutput AddSteeringOutputs(SteeringOutput source, SteeringOutput target)
	#region
	{
		SteeringOutput output;
		output 			= new SteeringOutput();
		output.linear 	= source.linear + target.linear;
		output.angle 	= source.angle + target.angle;
		return output;
	}
	#endregion

	public static int NearestPoint(Vector3 srcPos, Path route)
	{
		float distance, shortDistance = 0.0f;
		int point = -1;
		for(int i = 0; i < route.checkPoints.Count; i++)
		{
			distance = Vector3.Distance(srcPos, route.checkPoints[i].self);
			if(distance > shortDistance)
			{
				shortDistance = distance;
				point = i;
			}
		}
		return point;
	}
}
