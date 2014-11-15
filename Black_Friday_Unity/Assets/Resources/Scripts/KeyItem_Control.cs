using UnityEngine;
using System.Collections;

public class KeyItem_Control : MonoBehaviour
{
	public float 			rotateAngle;
	public int 				savings;
	private bool 			active;
	private Material 		glowMat;
	private Material 		original;
	private MeshRenderer 	selfMats;
	private Master_Control 	masterScript;
	public string 			listName;
	private Level_Control	levelControl;

	void Awake()
	{
		selfMats 			= this.gameObject.GetComponent<MeshRenderer>();
		glowMat 			= Resources.Load("Prefabs/Placeholders/Materials/yellowMat") as Material;
		original 			= selfMats.materials[0];
		selfMats.material 	= glowMat;
		SetActive(false);
		masterScript = GameObject.FindGameObjectWithTag("Master").GetComponent<Master_Control>();
		levelControl = GameObject.Find("Scene Control").GetComponent<Level_Control>();
	}

	void Update()
	{
		if(active)
		{
			Vector3 axis = new Vector3(0.0f, 1.0f, 0.0f);
			this.transform.Rotate(axis, rotateAngle * Time.deltaTime);
		}
	}

	public int GetSavings()
	{
		return savings;
	}

	public void SetActive(bool source)
	{
		active = source;
		
		if(active)
		{
			selfMats.material 				= glowMat;
			this.gameObject.SetActive(true);
		}
		else
		{
			this.gameObject.SetActive(false);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			masterScript.AddCash(savings);
			levelControl.CollectedItem();
		}
	}
}
