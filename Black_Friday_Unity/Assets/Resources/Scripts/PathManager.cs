using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Path
{
	public List<CheckPoint> checkPoints;

	//call at the beginning of the application
	public void Setup()
	{
		checkPoints = new List<CheckPoint>();
	}
	
	//call between levels when paths change
	public void Reset()
	{
		checkPoints.Clear();
	}
}

public class PathManager
{
	public List<Path> paths;

	public void SetupPaths()
	{
		paths = new List<Path>();
	}

	public void ResetPaths()
	{
		for(int i = 0; i < paths.Count; i++)
		{
			paths[i].Reset();
		}
		paths.Clear();
	}

	public void SetPaths()
	{
		GameObject[] points = GameObject.FindGameObjectsWithTag("Check Point");
		for(int i = 0; i < points.Length; i++)
		{
			int pathIndex = -8 + points[i].layer;
			if(pathIndex + 1 > paths.Count)
			{
				paths.Add(new Path());
				paths[pathIndex].Setup();
			}
			paths[pathIndex].checkPoints.Add(points[i].GetComponent<CheckPoint>());
		}
	}
}
