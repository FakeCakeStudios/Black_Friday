using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player_Data
{
	//private
	private int 				cash;
	private List<Powerups> 		powerups;
	private List<KartUpgrades> 	kartUpgrades;
	private List<bool>			levelUnlocks;
	private List<bool>			characterUnlocks;
	private PlayerModel			playerModel;
	private CartModel			cartModel;
	
	// Use this for initialization
	public void Initialize()
	{
		//default value
		cash 			= 0;

		//create the lists
		powerups 			= new List<Powerups>();
		kartUpgrades 		= new List<KartUpgrades>();
		levelUnlocks 		= new List<bool>();
		characterUnlocks 	= new List<bool>();
		playerModel			= new PlayerModel();
		cartModel			= new CartModel();

		for(int i = 0; i < 2; i ++)
		{
			levelUnlocks.Add(false);
		}
		for(int i = 0; i < 1; i ++)
		{
			characterUnlocks.Add(false);
		}

		playerModel = PlayerModel.Nerd;
		cartModel 	= CartModel.Starter;
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

	public void SetPlayerModel(PlayerModel source)
	{
		playerModel = source;
	}

	public PlayerModel GetPlayerModel()
	{
		return playerModel;
	}

	public void SetCartModel(CartModel source)
	{
		cartModel = source;
	}
	
	public CartModel GetCartModel()
	{
		return cartModel;
	}

	public void SetLevelUnlocks(int source)
	{
		levelUnlocks[source] = true;
	}

	public List<bool> GetLevelUnlocks()
	{
		return levelUnlocks;
	}

	public void SetCharacterUnlocks(int source)
	{
		characterUnlocks[source] = true;
	}
	
	public List<bool> GetCharacterUnlocks()
	{
		return characterUnlocks;
	}
}
