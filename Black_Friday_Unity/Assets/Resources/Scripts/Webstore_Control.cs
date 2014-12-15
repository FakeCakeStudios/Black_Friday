using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Webstore_Control : Scene_Control
{
	public int cash;
	private Player_Data playerdata;

	override public void Initialize(){
		if(GameObject.FindGameObjectWithTag("Master") != null){
			playerdata = masterScript.GetPlayerData();
			cash = playerdata.GetCash();
		}
	}

	override public void SceneAction(int set){
		switch(set){
		case(10):{
			playerdata.AddPowerup(Powerups.Megacubes);
			break;
		}
		case(11):{
			playerdata.AddPowerup(Powerups.Tacks);
			break;
		}
		case(12):{
			playerdata.AddPowerup(Powerups.StickyHand);
			break;
		}
		case(13):{
			playerdata.AddPowerup(Powerups.RollerSkates);
			break;
		}
		case(14):{
			//playerdata.AddPowerup(Powerups.);// ------
			break;
		}
		case(15):{
			playerdata.AddPowerup(Powerups.Mask);
			break;
		}
		case(16):{
			playerdata.AddPowerup(Powerups.Marbles);
			break;
		}
		case(17):{
			playerdata.AddPowerup(Powerups.Jawbreakers);
			break;
		}
		case(18):{
			playerdata.AddPowerup(Powerups.Glue);
			break;
		}
		case(19):{
			playerdata.AddPowerup(Powerups.BlackCoffee);
			break;
		}
		case(20):{
			playerdata.AddPowerup(Powerups.Box);
			break;
		}
		case(21):{
			playerdata.AddPowerup(Powerups.Horn);
			break;
		}
		}
	}
	public Player_Data GetPlayerData(){return playerdata;}
}