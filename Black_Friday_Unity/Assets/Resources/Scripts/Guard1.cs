using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* Guard Behavior Description
 * Guard1 is a patrolling guard that will follow a set path of points
 * whilst checking for if the player is within an agro radius. If the player is within
 * the agro radius, the guard will pursue the player as long as within this range. The 
 * chase will terminate when the player is no longer within the agro radius or the 
 * player is caught. If the chase ends because the player is no longer within the agro
 * radius, the guard will continue their path from the closest point to their current
 * position.
 */
public class Guard1 : Behavior
{

	//use this function for additional variable intialization for the needed behavior
	override public void Initialize()
	{

	}

	public void BehaviorControl(Entity_Data source, Player_Control playerInfo, Path path)
	{
		source.AddTime();
		Transform self 				= source.GetSelf();
		Vector3 playerPos 			= playerInfo.GetSelf().position;
		List<Interaction> actions 	= source.GetAction();
		bool agrod 					= source.GetActAgro();
		bool agroDetect 			= false;

		//check to see if player is within agro range and sight
		if(!actions.Contains(Interaction.Undetectable))
		{
			agroDetect = Detection.PlayerAgro(source, playerPos);
		}
		else
		{
			source.SetActAgro(agroDetect);
		}

		if(agrod == true && agrod != agroDetect)
		{
			source.timer1 += Time.deltaTime;
			if(source.timer1 >= source.trigger1)
			{
				source.timer1 = 0.0f;
				source.SetActAgro(agroDetect);
				source.SetNewDestination(true);
			}
		}
		else
		{
			source.SetActAgro(agroDetect);
		}

		if(agrod && !actions.Contains(Interaction.Stop))
		{
			source.SetStopped(false);
		}
		else if(actions.Contains(Interaction.Stop))
		{
			source.SetStopped(true);
		}

		SteeringOutput output = new SteeringOutput();
		if(!source.GetStopped() && !actions.Contains(Interaction.Runaway))
		{
			if(agrod)
			{
				source.SetNewDestination(true);

				//puruse after the player
				output = Steering.Pursue(self.position, playerPos, source.GetVelocity(), playerInfo.GetVelocity());
				//assemble new target in case obstacles are in the way
				Vector3 target = new Vector3();
				target = Detection.AvoidObstacles(source);
				//if target is not 0, then Seek the new target to avoid the obstacle
				if(target != Vector3.zero)
				{
					output = Steering.Seek(self.position, target);
				}
				//always look in the direction we are moving
				source.SetOutput(Steering.AddSteeringOutputs(output, Steering.LookInDir(self, output.linear)));
			}
			else
			{
				if(source.GetNewDestination())
				{
					//source.SetLastCheckPoint(pathsMng.NearestPoint(self.position, source.pathRoute));
					source.SetLastCheckPoint(Steering.NearestPoint(self.position, path));
					source.SetNewDestination(false);
				}
			
				//follow the provided path
				output = Steering.FollowPath(source, path);
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
		}
		else if(!source.GetStopped() && actions.Contains(Interaction.Runaway))
		{
			output = Steering.Evade(self.position, playerInfo.GetSelf().position, source.GetVelocity(), playerInfo.GetVelocity());
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
	}
}
