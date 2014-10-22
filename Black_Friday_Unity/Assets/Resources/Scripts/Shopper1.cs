using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shopper1 : Behavior
{
	//use this function for additional variable intialization for the needed behavior
	override public void Initialize()
	{

	}

	public void BehaviorControl(Entity_Data source, Entity_Data playerInfo, Path path)
	{
		source.AddTime();
		Transform self 				= source.GetSelf();
		bool agrod 					= source.GetActAgro();
		List<Interaction> actions   = source.GetAction();

		source.timer2 += Time.deltaTime;
		SteeringOutput output = new SteeringOutput();
		if(!source.GetStopped() && !actions.Contains(Interaction.Runaway))
		{
			if(source.timer2 < source.trigger2)
			{
				//new destination is representing if the shopper is wanting to go down a new isle
				if(source.GetNewDestination())
				{
					source.SetLastCheckPoint(Steering.NearestPoint(self.position, path));
					source.SetNewDestination(false);
				}

				if(agrod)
				{
					output = Steering.Pursue(self.position, source.GetDestination(), source.GetVelocity(), Vector3.zero);
					if(output.linear == Vector3.zero)
					{
						source.SetActAgro(false);
					}
				}
				else
				{
					source.timer1 += Time.deltaTime;
					if(source.trigger1 < source.timer1)
					{
						source.SetDestination(IsleShopPoint(source));
						source.SetActAgro(true);
						source.timer1 		= 0.0f;
						source.RandomTrigger();
					}
					//follow the provided path
					output = Steering.FollowPath(source, path);
				}
			}
			else
			{
				source.doneShopping = true;
				source.alive 		= false;
			}
		}
		else if(!source.GetStopped() && actions.Contains(Interaction.Runaway))
		{
			output = Steering.Evade(self.position, playerInfo.GetSelf().position, source.GetVelocity(), playerInfo.GetVelocity());
		}
		//assemble new target in case obstacles are in the way
		Vector3 target 	= new Vector3();
		target 			= Detection.AvoidObstacles(source);
		//if target is not 0, then Seek the new target to avoid the obstacle
		if(target != Vector3.zero)
		{
			//source.output = steering.AddSteeringOutputs(source.output, steering.Seek(source.self.position, target));
			output = Steering.Seek(self.position, target);
		}
		//always look in the direction we are moving
		source.SetOutput(Steering.AddSteeringOutputs(output, Steering.LookInDir(self, output.linear)));
	}

	public void GetInLine(Entity_Data source, List<Transform> line)
	{
		Transform self = source.GetSelf();
		SteeringOutput output = new SteeringOutput();

		Vector3 pos = line[source.pathRoute - 1].forward * -5.0f;
		pos.y = 0.0f;

		Vector3 destination = source.GetDestination();
		if(destination != line[source.pathRoute - 1].position + pos)
		{
			source.SetDestination(line[source.pathRoute - 1].position);
			source.SetStopped(false);
		}

		output = Steering.Pursue(self.position, destination, source.GetVelocity(), Vector3.zero);
		if(output.linear != Vector3.zero)
		{
			//assemble new target in case obstacles are in the way
			Vector3 target = new Vector3();
			target = Detection.AvoidObstacles(source);
			//if target is not 0, then Seek the new target to avoid the obstacle
			if(target != Vector3.zero)
			{
				//source.output = steering.AddSteeringOutputs(source.output, steering.Seek(source.self.position, target));
				output = Steering.Seek(self.position, target);
			}
			//always look in the direction we are moving
			source.SetOutput(Steering.AddSteeringOutputs(output, Steering.LookInDir(self, output.linear)));
		}
		else
		{
			output = Steering.Align2D(self.eulerAngles, source.GetPostRotation());
			if(output.angle == 0.0f)
			{
				source.SetStopped(true);
			}
			source.SetOutput(output);
		}
	}
	
	//takes one entity at a time and determines to avoid obstacles by raycasting
	Vector3 IsleShopPoint(Entity_Data entity)
	#region
	{
		Transform self = entity.GetSelf();

		RaycastHit	hit;
		Ray			rays;
		Vector3 	toSeek;
		float 		rayDistance = 20.0f;;
		
		toSeek 		= new Vector3();
		rays 		= new Ray();

		Vector3 direction = self.eulerAngles;
		
		direction.y += Random.Range(-1, 1) * 45;

		Transform whiskers = new GameObject().transform;
		whiskers.eulerAngles = direction;
		
		rays = new Ray(self.position, whiskers.forward);
		Debug.DrawRay(self.position, whiskers.forward, Color.green);
		
		GameObject.Destroy(whiskers.gameObject);
		
		toSeek = Vector3.zero;

		if(Physics.Raycast(rays, out hit, rayDistance))
		{
			toSeek = hit.point;
			toSeek.y = 0.0f;
		}
		//the return variable is to remind to call the Seek function of Steering after this call if not zero vectors
		return toSeek;
	}
	#endregion
}
