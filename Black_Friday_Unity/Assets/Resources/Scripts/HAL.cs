﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Lines
{
	public List<Transform> lines;


	public void Initialize()
	{
		lines = new List<Transform>();
	}

	public void Reset()
	{
		lines.Clear();
	}
}

public class HAL
{
	private List<GameObject> 		entities;
	private List<Entity_Data> 		scriptEnts;
	private List<Spawn_Triggers> 	triggers;
	private List<Vector3>			keyPoints;
	private List<Lines>				checkout;

	private PathManager 			pathsMng;
	private Guard1					guardType1;
	private Guard2					guardType2;
	private Shopper1				shopperType1;
	public Player_Control			playerScript;
	
	private Object 					Guard1;
//	private Object 					Guard2;
	private Object 					Shopper1;
	//private Object 					Shopper2;
	//private Object 					Shopper3;
	
	public Transform				playerPos;
	private Vector3					spawnPoint;
	private Quaternion				spawnRot;
	private Vector3					exitPoint;

	private float testTimer;
	private float testTrigger;

	public void Initialize()
	{
		testTimer = 0.0f;
		testTrigger = 5.0f;

		entities 	= new List<GameObject>();
		scriptEnts 	= new List<Entity_Data>();
		triggers 	= new List<Spawn_Triggers>();
		keyPoints 	= new List<Vector3>();
		checkout 	= new List<Lines>();

		pathsMng 	= new PathManager();
		pathsMng.SetupPaths();

		shopperType1	= new Shopper1();
		shopperType1.Start();
		guardType1		= new Guard1();
		guardType1.Start();
		guardType2		= new Guard2();
		guardType2.Start();

		Guard1 			= Resources.Load("Prefabs/Characters/BigCop");
		//Guard2 			= Resources.Load("Prefabs/Guard2");
		Shopper1 		= Resources.Load ("Prefabs/Characters/Shopper 1");
		//Shopper2 		= Resources.Load ("Prefabs/Shopper2");
		//Shopper3 		= Resources.Load ("Prefabs/Shopper3");
	}

	public void SetPlayer()
	{
		playerPos = GameObject.FindGameObjectWithTag("Player").transform;
		playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Control>();
	}

	public void Reset()
	{
		entities.Clear();
		scriptEnts.Clear();
		triggers.Clear();
		keyPoints.Clear();
		pathsMng.ResetPaths();
		for(int i = 0; i < checkout.Count; i++)
		{
			checkout[i].Reset();
		}
	}
	
	public void SetupLevel()
	{
		SetEntities();
		SetTriggers();
		pathsMng.SetPaths();
		SetSpawnPoint();
		SetCheckoutPoint();
	}

	public void MyUpdate()
	{
		testTimer += Time.deltaTime;
		if(testTimer >= testTrigger)
		{
			SpawnShopper();
			testTimer = 0.0f;
		}

		for(int i = 0; i < entities.Count; i++)
		{
			scriptEnts[i].AddTime();
			switch(scriptEnts[i].behavior)
			{
			case(BehaviorType.Guard1):
			{
				guardType1.BehaviorControl(scriptEnts[i], playerScript, pathsMng.paths[scriptEnts[i].pathRoute]);
				break;
			}
			case(BehaviorType.Guard2):
			{
				guardType2.BehaviorControl(scriptEnts[i], playerScript);
				break;
			}
			case(BehaviorType.Shopper1):
			{
				if(!scriptEnts[i].doneShopping)
				{
					shopperType1.BehaviorControl(scriptEnts[i], playerScript, pathsMng.paths[scriptEnts[i].pathRoute]);
				}
				else
				{
					if(!scriptEnts[i].alive)
					{
						int currLine = 0;
						int shortestLine = checkout[0].lines.Count;

						for(int k = 0; k < checkout.Count; k++)
						{
							if(checkout[k].lines.Count <= shortestLine)
							{
								shortestLine = checkout[k].lines.Count;
								currLine = k;
							}
						}
						checkout[currLine].lines.Add(scriptEnts[i].GetSelf());
						scriptEnts[i].pathRoute = checkout[currLine].lines.Count - 1;
						scriptEnts[i].SetLastCheckPoint(currLine);
						scriptEnts[i].alive = true;
						scriptEnts[i].trigger1 = Random.Range(3.0f, 8.0f);
						scriptEnts[i].timer1 = 0.0f;
					}
					shopperType1.GetInLine(scriptEnts[i], checkout[scriptEnts[i].GetLastCheckPoint()].lines);
				}
				for(int k = 0; k < checkout.Count; k++)
				{
					if(checkout[k].lines.Count > 0)
					{

					}
				}






				break;
			}
			case(BehaviorType.Player):
			{
				break;
			}
			}
		}
		SpawnNPC();
	}

	void SpawnNPC()
	{
		for(int i = 0; i < triggers.Count; i++)
		{
			if(triggers[i].GetSpawnNPC())
			{
				Vector3 pos = playerPos.forward * -5.0f;
				pos.y = 0.1f;
				GameObject spawn = GameObject.Instantiate(Guard1, playerPos.position + pos, playerPos.rotation)as GameObject;
				entities.Add(spawn);
				scriptEnts.Add(spawn.GetComponent<Entity_Data>());
				triggers[i].SetSpawnNPC(false);
				//currently destroy the trigger game object after the guard has spawned to reduce number of game objects, because we only spawn one guard from each trigger for now
				if(triggers[i].GetNumOfSpawns() <= triggers[i].GetSpawnCount())
				{
					GameObject.Destroy(triggers[i].gameObject);
					triggers.RemoveAt(i);
				}
				else
				{
					triggers[i].collider.enabled = true;
				}
			}
		}
	}

	void SpawnShopper()
	{
		Object type = Shopper1;

		int shopperModel = Random.Range(0, 2);
		switch(shopperModel)
		{
		case(0):
		{
			type = Shopper1;
			break;
		}
		case(1):
		{
			//spawn = GameObject.Instantiate(Shopper2, spawnPoint, spawnRot)as GameObject;
			break;
		}
		case(2):
		{
			//spawn = GameObject.Instantiate(Shopper3, spawnPoint, spawnRot)as GameObject;
			break;
		}
		default:
		{
			type = Shopper1;
			break;
		}
		}

		GameObject spawn;
		spawn = GameObject.Instantiate(type, spawnPoint, spawnRot)as GameObject;
		entities.Add(spawn);
		scriptEnts.Add(spawn.GetComponent<Entity_Data>());
		//make new shopper act in one of the behaviors randomly
		//int behave = Random.Range(0, 3);
		//switch(behave)
		//{
		//case(0):
		//{
			scriptEnts[scriptEnts.Count - 1].behavior = BehaviorType.Shopper1;
			//break;
		//}
		//case(1):
		//{
			//scriptEnts[scriptEnts.Count - 1].behavior = BehaviorType.Shopper2;
			//break;
		//}
		//case(2):
		//{
			//scriptEnts[scriptEnts.Count - 1].behavior = BehaviorType.Shopper3;
			//break;
		//}
		//}
	}

	void SetEntities()
	{
		GameObject[] shoppers = GameObject.FindGameObjectsWithTag("Shoppers");
		GameObject[] guards = GameObject.FindGameObjectsWithTag("Guards");
		
		for(int i = 0; i < shoppers.Length; i++)
		{
			entities.Add(shoppers[i]);
			scriptEnts.Add(shoppers[i].GetComponent<Entity_Data>());
		}
		
		for(int i = 0; i < guards.Length; i++)
		{
			entities.Add(guards[i]);
			scriptEnts.Add(guards[i].GetComponent<Entity_Data>());
		}
	}
	
	void SetTriggers()
	{
		GameObject[] spawnTriggers = GameObject.FindGameObjectsWithTag("Spawn Trigger");
		for(int i = 0; i < spawnTriggers.Length; i++)
		{
			triggers.Add(spawnTriggers[i].GetComponent<Spawn_Triggers>());
		}
	}

	void SetKeyPoints()
	{
		GameObject[] items = GameObject.FindGameObjectsWithTag("Item Location");
		for(int i = 0; i < items.Length; i++)
		{
			keyPoints.Add(items[i].transform.position);
		}
	}


	void SetCheckoutPoint()
	{
		GameObject[] registers = GameObject.FindGameObjectsWithTag("Checkout");
		for(int i = 0; i < registers.Length; i++)
		{
			Lines temp = new Lines();
			temp.Initialize();
			temp.lines.Add(registers[i].transform);
			checkout.Add(temp);
		}
		exitPoint = GameObject.FindGameObjectWithTag("Exit").transform.position;
	}
	
	void SetSpawnPoint()
	{
		GameObject shopperSpawn = GameObject.FindGameObjectWithTag("Spawn Point");
		if(shopperSpawn != null)
		{
			spawnPoint 	= shopperSpawn.transform.position;
			spawnRot	= shopperSpawn.transform.rotation;
			GameObject.Destroy(shopperSpawn);
		}
	}

	public void SetEntitiesInteractions(Interaction source, float effectTime)
	{
		switch(source)
		{
		case(Interaction.Undetectable):
		{
			SetPlayerDetectable(effectTime);
			break;
		}
		case(Interaction.Runaway):
		{
			SetFleePlayer(effectTime);
			break;
		}
		}
	}

	public void SetPlayerDetectable(float effectedTime)
	{
		for(int i = 0; i < entities.Count; i++)
		{
			switch(scriptEnts[i].behavior)
			{
			case(BehaviorType.Player):
			{
				break;
			}
			case(BehaviorType.Shopper1):
			{
				break;
			}
			case(BehaviorType.Guard1):
			{
				scriptEnts[i].SetAction(Interaction.Undetectable, effectedTime);
				break;
			}
			case(BehaviorType.Guard2):
			{
				scriptEnts[i].SetAction(Interaction.Undetectable, effectedTime);
				break;
			}
			}
		}
	}

	public void SetFleePlayer(float effectedTime)
	{
		float distance;
		for(int i = 0; i < entities.Count; i++)
		{
			distance = Vector3.Distance(playerPos.position, scriptEnts[i].GetSelf().position);
			if(distance <= 3.0f)
			{
				scriptEnts[i].SetAction(Interaction.Runaway, effectedTime);
			}
		}
	}
}