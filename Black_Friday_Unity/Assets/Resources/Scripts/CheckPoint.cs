using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheckPoint : MonoBehaviour
{
	//public
	public Vector3 		self;
	public int			isleNum;
	public int			otherEnd;
	public int 			pointNum;
	public List<int>	connectedTo;

	void Start()
	{
		self 		= this.gameObject.transform.position;
		connectedTo = new List<int>();
	}
}
