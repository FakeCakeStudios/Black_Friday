using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Detection
{
	//recursive function to cycle through all NPC's and add to the list of possible collisions to avoid if within view and range
	public static List<Entity_Data> AvoidNPCs(List<Entity_Data> entities, int index)
	#region
	{
		List<Entity_Data> 	targets = new List<Entity_Data>();
		Vector3 			direction;

		for(int i = index; i + 1 < entities.Count; i++)
		{
			direction = entities[index + i + 1].GetSelf().position - entities[index].GetSelf().position;

			if(Vector3.Magnitude(direction) < entities[index].collRad)
			{
				float angle = Vector3.Angle(entities[index].GetSelf().forward, direction);

				if(angle < entities[index].viewAngle)
				{
					targets.Add(entities[index + i + 1]);
				}
			}
		}
		index += 1;

		if(index + 1 < entities.Count)
		{
			AvoidNPCs(entities, index);
		}
		return targets;
	}
	#endregion

	//takes an entity and checks if the player is within range and view to agro
	public static bool PlayerAgro(Entity_Data entity, Vector3 playerPos)
	#region
	{
		Ray		ray;
		RaycastHit	hit;
		Vector3 direction;
		direction = playerPos - entity.GetSelf().position;
		ray = new Ray(entity.GetSelf().position, direction);

		float angle = Vector3.Angle(entity.GetSelf().forward, direction);
		if(angle < entity.viewAngle)
		{
			if(Physics.Raycast(ray, out hit, entity.agroRad))
			{
				if(Vector3.Distance(hit.point, playerPos) < 0.5f)
				{
					return true;
				}
			}
		}
		return false;
	}
	#endregion

	//takes one entity at a time and determines to avoid obstacles by raycasting
	public static Vector3 AvoidObstacles(Entity_Data entity)
	#region
	{
		RaycastHit	hit;
		Ray[]		rays;
		float 		avoidDist = 5.0f;
		Vector3 	toSeek;

		toSeek 	= new Vector3();
		rays 	= new Ray[3];

		//center ray
		rays[0] = new Ray(entity.GetSelf().position, entity.GetSelf().forward);
		Debug.DrawRay(entity.GetSelf().position, entity.GetSelf().forward, Color.white);
		
		Vector3 direction = entity.GetSelf().eulerAngles;
		
		direction.y += 20.0f;
		
		Transform whiskers = new GameObject().transform;
		whiskers.eulerAngles = direction;
		
		rays[1] = new Ray(entity.GetSelf().position, whiskers.forward);
		Debug.DrawRay(entity.GetSelf().position, whiskers.forward, Color.green);
		
		direction.y -= 40.0f;
		whiskers.eulerAngles = direction;
		
		rays[2] = new Ray(entity.GetSelf().position, whiskers.forward);
		Debug.DrawRay(entity.GetSelf().position, whiskers.forward, Color.yellow);
		
		GameObject.Destroy(whiskers.gameObject);

		toSeek = Vector3.zero;

		for(int i = 0; i < 3; i++)
		{
			if(Physics.Raycast(rays[i], out hit, entity.rayDist[i]))
			{
				if(hit.transform.gameObject.layer != 9)
				{
					toSeek = (hit.point + hit.normal * avoidDist);
					toSeek.y = 0.0f;
				}
			}
		}
		//the return variable is to remind to call the Seek function of Steering after this call if not zero vectors
		return toSeek;
	}
	#endregion
}
