using UnityEngine;
using System.Collections;

[System.Serializable]
public class SpeedsStats{
	public float speed = 10f;
	public float gravity = 10f;
	public float mouseSensitivity = 10f;
}
[System.Serializable]
public class CameraStats{
	public GameObject cameraObject;
	public Vector3 offset;
	public Vector3 lookOffset;
}
[System.Serializable]
public class CameraOverView{
	public GameObject[] locations;
	public float duration;
	public bool allDone;

	private bool[] done;
	private float endTime;
	private int counter;

	public void ViewStart(){
		ViewTimeSet();
		counter = 0;
		allDone = false;
		locations = GameObject.FindGameObjectsWithTag("CamLocs");
		done = new bool[locations.Length];
	}
	public void ViewUpdate(GameObject theCamera){
		if(counter < locations.Length){
			if(Time.time > endTime){
				done[counter] = true;
				counter++;
				ViewTimeSet();
			}
			else{
				theCamera.transform.position = locations[counter].transform.position;
				theCamera.transform.eulerAngles = locations[counter].transform.rotation.eulerAngles;
			}
		}
		else{
			allDone = true;
		}
	}
	private void ViewTimeSet(){
		endTime = Time.time+duration;
	}
}