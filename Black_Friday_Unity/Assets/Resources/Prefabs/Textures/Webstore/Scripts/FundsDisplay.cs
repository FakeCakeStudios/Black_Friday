using UnityEngine;
using System.Collections;

public class FundsDisplay : MonoBehaviour {
	public Webstore_Control control;
	public UILabel fundsTarget;

	void Update(){
		fundsTarget.text = string.Format("$ {0}", control.cash);
	}
}
