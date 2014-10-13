using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player_Data
{
	//ints
	private int 				cash;

	//lists
	private List<Powerups> 		powerups;
	private List<KartUpgrades> 	kartUpgrades;

	// Use this for initialization
	public void Initialize()
	{
		//default value
		cash 			= 0;

		//create the lists
		powerups 		= new List<Powerups>();
		kartUpgrades 	= new List<KartUpgrades>();
	}

	//return the cash amount stored
	public int GetCash()
	{
		return cash;
	}

	//add cash to the stored amount
	public void AddCash(int source)
	{
		cash += source;
	}

	//subtract the cash from the stored amount
	public void SubtractCash(int source)
	{
		cash -= source;
	}

	//return the list of powerups
	public List<Powerups> GetPowerups()
	{
		return powerups;
	}

	//return the lists of kart upgrades
	public List<KartUpgrades> GetKartUpgrades()
	{
		return kartUpgrades;
	}

	//add a powerup to the list
	public void AddPowerup(Powerups source)
	{
		powerups.Add(source);
	}

	//add a kart upgrade to the list
	public void AddKartUpgrade(KartUpgrades source)
	{
		kartUpgrades.Add(source);
	}

	//remove a powerup
	public void RemovePowerup(Powerups source)
	{
		powerups.Remove(source);
	}

	//remove a kart upgrade
	public void RemoveKartUpgrade(KartUpgrades source)
	{
		kartUpgrades.Remove(source);
	}

	//clear the entire list of powerups
	public void ClearPowerups()
	{
		powerups.Clear();
	}

	//clear the entire lists of kart upgrades
	public void ClearKartUpgrades()
	{
		kartUpgrades.Clear();
	}
}
