using UnityEngine;
using System.Collections;

public class Button_Notifier : MonoBehaviour
{
	private bool indicate;
	private float startingPosition;
	private float slideAmount;
	private int direction;
	private float slideSpeed;
	private UIAnchor anchor;

	// Use this for initialization
	void Start()
	{
		if(GameObject.Find("Master Control").GetComponent<Master_Control>().GetPlayerData().GetCash() > 0)
		{
			indicate = true;
		}
		else
		{
			indicate = false;
		}
		slideAmount = 0.05f;
		direction = -1;
		slideSpeed = 0.25f;
		anchor = this.GetComponent<UIAnchor>();
		startingPosition = anchor.relativeOffset.x;
		indicate = true;
	}
	
	// Update is called once per frame
	void Update()
	{
		if(indicate)
		{
			Vector3 tempPosition = this.transform.position;
			if(startingPosition - anchor.relativeOffset.x > slideAmount || startingPosition - anchor.relativeOffset.x < -slideAmount)
			{
				direction *= -1;
			}
			anchor.relativeOffset.x += direction * slideSpeed * Time.deltaTime;
			this.transform.position = tempPosition;
		}
	}
}
