using UnityEngine;
using System.Collections;

public class WebstoreToggle : MonoBehaviour {
	public Webstore_Control control;
	public Index_Button indexButton;
	public GameObject[] windows;
	public PupInfo pupInfo;
	public UILabel SideText;
	public UILabel pricetext;
	public int[] prices;
	public PupInfo purchasedItem;
	public GameObject[] purchasedDiplay;

	public bool[] lastOffense;
	public bool[] lastSpeed;
	public bool[] lastDefense;
	//Main Panel Functions
	public void Awake(){
		lastOffense = new bool[3];
		lastSpeed = new bool[3];
		lastDefense = new bool[3];

		windows[0].SetActive(true);
		windows[1].SetActive(false);
		windows[2].SetActive(false);
		windows[3].SetActive(false);
		for(int i=0; i<purchasedDiplay.Length; i++){purchasedDiplay[i].SetActive(false);}
	}
	public void GotoFaceDate(){
		windows[0].SetActive(false);
		windows[1].SetActive(true);
		windows[2].SetActive(false);
		windows[3].SetActive(false);
	}
	public void GotoPowerUps(){
		windows[0].SetActive(false);
		windows[1].SetActive(false);
		windows[2].SetActive(true);
		windows[3].SetActive(false);
	}
	public void GotoKartUpgrades(){
		windows[0].SetActive(false);
		windows[1].SetActive(false);
		windows[2].SetActive(false);
		windows[3].SetActive(true);
	}
	public void GotoWebstoreMain(){
		windows[0].SetActive(true);
		windows[1].SetActive(false);
		windows[2].SetActive(false);
		windows[3].SetActive(false);
	}

	//FaceDate Panel Functions
	public void FDToggleStats(){
		if(windows[4].activeSelf){
			windows[4].SetActive(false);
			windows[5].SetActive(true);
			windows[6].SetActive(true);
		}else{
			windows[4].SetActive(true);
			windows[5].SetActive(false);
			windows[6].SetActive(false);
		}
	}

	//Powerup Panel Functions
	public void ShowPUP(){
		if(pupInfo == PupInfo.None){
			pricetext.text = "$ ???";
			SideText.text = "";
		}
		else if(pupInfo == PupInfo.Megacubes){
			pricetext.text = string .Format("$ {0}", prices[0]);
			SideText.text = "Megacubes\n(Description)";
		}
		else if(pupInfo == PupInfo.Tacks){
			pricetext.text = string .Format("$ {0}", prices[1]);
			SideText.text = "Tacks\n(Description)";
		}
		else if(pupInfo == PupInfo.Stickyhand){
			pricetext.text = string .Format("$ {0}", prices[2]);
			SideText.text = "Stickyhand\n(Description)";
		}
		else if(pupInfo == PupInfo.Rollerskates){
			pricetext.text = string .Format("$ {0}", prices[3]);
			SideText.text = "Rollerskates\n(Description)";
		}
		else if(pupInfo == PupInfo.Mask){
			pricetext.text = string .Format("$ {0}", prices[4]);
			SideText.text = "Mask\n(Description)";
		}
		else if(pupInfo == PupInfo.Marbles){
			pricetext.text = string .Format("$ {0}", prices[5]);
			SideText.text = "Marbles\n(Description)";
		}
		else if(pupInfo == PupInfo.Jawbreaker){
			pricetext.text = string .Format("$ {0}", prices[6]);
			SideText.text = "Jawbreaker\n(Description)";
		}
		else if(pupInfo == PupInfo.Glue){
			pricetext.text = string .Format("$ {0}", prices[7]);
			SideText.text = "Glue\n(Description)";
		}
		else if(pupInfo == PupInfo.BlackCoffee){
			pricetext.text = string .Format("$ {0}", prices[8]);
			SideText.text = "BlackCoffee\n(Description)";
		}
		else if(pupInfo == PupInfo.Box){
			pricetext.text = string .Format("$ {0}", prices[9]);
			SideText.text = "Box\n(Description)";
		}
		else if(pupInfo == PupInfo.Horn){
			pricetext.text = string .Format("$ {0}", prices[10]);
			SideText.text = "Horn\n(Description)";
		}
		else{
			pricetext.text = "$ ???";
			SideText.text = "Not availible yet.";
		}
	}
	public bool priceCheck(){
		bool BOOL = false; int set = 0;
		if(pupInfo == PupInfo.Megacubes){set = 0;}
		else if(pupInfo == PupInfo.Megacubes){set = 1;}
		else if(pupInfo == PupInfo.Megacubes){set = 2;}
		else if(pupInfo == PupInfo.Megacubes){set = 3;}
		else if(pupInfo == PupInfo.Megacubes){set = 4;}
		else if(pupInfo == PupInfo.Megacubes){set = 5;}
		else if(pupInfo == PupInfo.Megacubes){set = 6;}
		else if(pupInfo == PupInfo.Megacubes){set = 7;}
		else if(pupInfo == PupInfo.Megacubes){set = 8;}
		else if(pupInfo == PupInfo.Megacubes){set = 9;}
		else if(pupInfo == PupInfo.Megacubes){set = 10;}
		int remain = control.cash - prices[set];
		if(remain >= 0){
			BOOL =  true;
		}else{
			BOOL = false;
		}
		return BOOL;
	}
	public void ConfirmationToggle(){
		if(priceCheck()){
			if(windows[25].activeSelf && pupInfo != PupInfo.None && pupInfo != PupInfo.NA){
				windows[25].SetActive(false);
				windows[26].SetActive(true);
			}else{
				windows[25].SetActive(true);
				windows[26].SetActive(false);
			}
		}
	}
	public void ConfirmAccept(){
		windows[25].SetActive(true);
		windows[26].SetActive(false);
		purchasedItem = pupInfo;
		if(purchasedItem == PupInfo.Megacubes){ShowDisplayItem(0);control.cash =- prices[0];indexButton.index = 10;}
		if(purchasedItem == PupInfo.Tacks){ShowDisplayItem(1);control.cash =- prices[1];indexButton.index = 11;}
		if(purchasedItem == PupInfo.Stickyhand){ShowDisplayItem(2);control.cash =- prices[2];indexButton.index = 12;}
		if(purchasedItem == PupInfo.Rollerskates){ShowDisplayItem(3);control.cash =- prices[3];indexButton.index = 13;}
		if(purchasedItem == PupInfo.Mask){ShowDisplayItem(4);control.cash =- prices[4];indexButton.index = 15;}
		if(purchasedItem == PupInfo.Marbles){ShowDisplayItem(5);control.cash =- prices[5];indexButton.index = 16;}
		if(purchasedItem == PupInfo.Jawbreaker){ShowDisplayItem(6);control.cash =- prices[6];indexButton.index = 17;}
		if(purchasedItem == PupInfo.Glue){ShowDisplayItem(7);control.cash =- prices[7];indexButton.index = 18;}
		if(purchasedItem == PupInfo.BlackCoffee){ShowDisplayItem(8);control.cash =- prices[8];indexButton.index = 19;}
		if(purchasedItem == PupInfo.Box){ShowDisplayItem(9);control.cash =- prices[9];indexButton.index = 20;}
		if(purchasedItem == PupInfo.Horn){ShowDisplayItem(10);control.cash =- prices[10];indexButton.index = 21;}
		control.masterScript.SetPlayerData(control.GetPlayerData());
	}
	public void ConfirmCancel(){
		windows[25].SetActive(true);
		windows[26].SetActive(false);
	}
	public void ShowDisplayItem(int value){
		purchasedDiplay[value].SetActive(true);
	}
	public void PUP01(){pupInfo = PupInfo.Megacubes;ShowPUP();}
	public void PUP02(){pupInfo = PupInfo.Tacks;ShowPUP();}
	public void PUP03(){pupInfo = PupInfo.Stickyhand;ShowPUP();}
	public void PUP04(){pupInfo = PupInfo.Rollerskates;ShowPUP();}
	public void PUP05(){pupInfo = PupInfo.NA;ShowPUP();}
	public void PUP06(){pupInfo = PupInfo.Mask;ShowPUP();}
	public void PUP07(){pupInfo = PupInfo.Marbles;ShowPUP();}
	public void PUP08(){pupInfo = PupInfo.Jawbreaker;ShowPUP();}
	public void PUP09(){pupInfo = PupInfo.Glue;ShowPUP();}
	public void PUP10(){pupInfo = PupInfo.BlackCoffee;ShowPUP();}
	public void PUP11(){pupInfo = PupInfo.Box;ShowPUP();}
	public void PUP12(){pupInfo = PupInfo.Horn;ShowPUP();}

	//KartUpgrade Panel Functions
	public void OffenseToggle(){
		if(windows[7].activeSelf){
			windows[7].SetActive(false);
			windows[8].SetActive(true);
			windows[9].SetActive(true);
			windows[10].SetActive(false);
			windows[11].SetActive(true);
			windows[12].SetActive(false);
			windows[16].SetActive(true);
			windows[20].SetActive(false);
			windows[24].SetActive(false);
		}else{
			windows[7].SetActive(true);
			windows[8].SetActive(false);
			windows[9].SetActive(true);
			windows[10].SetActive(false);
			windows[11].SetActive(true);
			windows[12].SetActive(false);
			windows[16].SetActive(false);
			windows[20].SetActive(false);
			windows[24].SetActive(false);
		}
	}
	public void SpeedToggle(){
		if(windows[9].activeSelf){
			windows[7].SetActive(true);
			windows[8].SetActive(false);
			windows[9].SetActive(false);
			windows[10].SetActive(true);
			windows[11].SetActive(true);
			windows[12].SetActive(false);
			windows[16].SetActive(false);
			windows[20].SetActive(true);
			windows[24].SetActive(false);
		}else{
			windows[7].SetActive(true);
			windows[8].SetActive(false);
			windows[9].SetActive(true);
			windows[10].SetActive(false);
			windows[11].SetActive(true);
			windows[12].SetActive(false);
			windows[16].SetActive(false);
			windows[20].SetActive(false);
			windows[24].SetActive(false);
		}
	}
	public void DefenseToggle(){
		if(windows[11].activeSelf){
			windows[7].SetActive(true);
			windows[8].SetActive(false);
			windows[9].SetActive(true);
			windows[10].SetActive(false);
			windows[11].SetActive(false);
			windows[12].SetActive(true);
			windows[16].SetActive(false);
			windows[20].SetActive(false);
			windows[24].SetActive(true);
		}else{
			windows[7].SetActive(true);
			windows[8].SetActive(false);
			windows[9].SetActive(true);
			windows[10].SetActive(false);
			windows[11].SetActive(true);
			windows[12].SetActive(false);
			windows[16].SetActive(false);
			windows[20].SetActive(false);
			windows[24].SetActive(false);
		}
	}

	public void OffenseSlotOne(){
		if(!windows[13].activeSelf){
			windows[13].SetActive(true);
		}else{		
			windows[13].SetActive(false);
		}
	}
	public void OffenseSlotTwo(){
		if(!windows[14].activeSelf){		
			windows[14].SetActive(true);
		}else{		
			windows[14].SetActive(false);
		}
	}
	public void OffenseSlotThree(){
		if(!windows[15].activeSelf){		
			windows[15].SetActive(true);
		}else{		
			windows[15].SetActive(false);
		}
	}
	
	public void SpeedSlotOne(){
		if(!windows[17].activeSelf){		
			windows[17].SetActive(true);
		}else{		
			windows[17].SetActive(false);
		}
	}
	public void SpeedSlotTwo(){
		if(!windows[18].activeSelf){		
			windows[18].SetActive(true);
		}else{		
			windows[18].SetActive(false);
		}
	}
	public void SpeedSlotThree(){
		if(!windows[19].activeSelf){		
			windows[19].SetActive(true);
		}else{		
			windows[19].SetActive(false);
		}
	}

	public void DefenseSlotOne(){
		if(!windows[21].activeSelf){		
			windows[21].SetActive(true);
		}else{		
			windows[21].SetActive(false);
		}
	}
	public void DefenseSlotTwo(){
		if(!windows[22].activeSelf){		
			windows[22].SetActive(true);
		}else{		
			windows[22].SetActive(false);
		}
	}
	public void DefenseSlotThree(){
		if(!windows[23].activeSelf){		
			windows[23].SetActive(true);
		}else{		
			windows[23].SetActive(false);
		}
	}
}

public enum PupInfo{
	None,
	Megacubes,
	Tacks,
	Stickyhand,
	Rollerskates,
	Mask,
	Marbles,
	Jawbreaker,
	Glue,
	BlackCoffee,
	Box,
	Horn,
	NA
}
