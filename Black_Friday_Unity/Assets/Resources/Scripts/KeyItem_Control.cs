using UnityEngine;
using System.Collections;

public class KeyItem_Control : MonoBehaviour
{
	public float 			rotateAngle;
	public int 				cost;
	public int 				savings;
	private bool 			active;
	private Material 		glowMat;
	private Material 		original;
	private MeshRenderer 	selfMats;

	void Start()
	{
		selfMats 			= this.gameObject.GetComponent<MeshRenderer>();
		glowMat 			= Resources.Load("Prefabs/Materials/yellowMat") as Material;
		original 			= selfMats.materials[0];
		selfMats.material 	= glowMat;
		SetActive(false);
	}

	void Update()
	{
		if(active)
		{
			Vector3 axis = new Vector3(0.0f, 1.0f, 0.0f);
			this.transform.Rotate(axis, rotateAngle * Time.deltaTime);
		}
	}

	public int GetCost()
	{
		return cost;
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
			selfMats.enabled 				= true;
			selfMats.material 				= glowMat;
			this.particleEmitter.enabled 	= true;
		}
		else
		{
			selfMats.enabled				= false;
			selfMats.material 				= original;
			this.particleEmitter.enabled 	= false;
		}
	}
}
