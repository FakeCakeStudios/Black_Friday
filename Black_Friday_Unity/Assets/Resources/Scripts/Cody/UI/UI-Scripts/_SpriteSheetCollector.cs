using UnityEngine;
using System.Collections;

public class _SpriteSheetCollector : MonoBehaviour {

	public __SpriteSheets[] SpriteInfo;
	public __IconsCoordinates[] coordinates;
	public UIGUI UI;

	void Awake(){
		// ----------------------------------------------------------------------- //
		// -------- Defines the touch areas that are going to be used ------------ //
		// ----------------------------------------------------------------------- //
		for(int i=0; i<SpriteInfo[0].icons.Length; i++){
			if(SpriteInfo[0].icons[i].name == "MenuButton"){
				UI.MenuButton = SpriteInfo[0].GetSize("MenuButton");
				//UI.MenuButton = new Rect (0, 0, 200, 100);
			}

			if(SpriteInfo[0].icons[i].name == "ListButton"){
				UI.ListButton = SpriteInfo[0].GetSize("ListButton");
				UI.ListButton = new Rect (Screen.width-UI.ListButton.width, 0, UI.ListButton.width, UI.ListButton.height);
				//UI.ListButton = new Rect (Screen.width-200, 0, 200, 100);
			}

			UI.LeftSide1 = new Rect (0, UI.MenuButton.height, UI.MenuButton.width, Screen.height-UI.MenuButton.height);
			UI.LeftSide2 = new Rect (UI.MenuButton.width, 0, Screen.width*0.5f-UI.MenuButton.width, Screen.height);
			UI.RightSide1 = new Rect (Screen.width*0.5f, 0, Screen.width*0.5f-UI.ListButton.width, Screen.height);
			UI.RightSide2 = new Rect (Screen.width*0.5f+UI.RightSide1.width, UI.ListButton.height, UI.ListButton.width, Screen.height-UI.MenuButton.height);
		}

	}

	void OnGUI(){
		// ----------------------------------------------------------------------- //
		// ----------- Displays Icons Based on the Needed Icons ------------------ //
		// ----------------------------------------------------------------------- //
		//SpriteInfo[0].DrawIcon("MenuButton", new Vector2(UI.MenuButton.x, UI.MenuButton.y));
		//SpriteInfo[0].DrawIcon("ListButton", new Vector2(UI.ListButton.x, UI.ListButton.y));
		/*
	for(int i=0; i<coordinates.Length; i++){
		SpriteInfo[0].DrawIcon(coordinates[i].name, coordinates[i].iconCoordinates);
	}
	*/
		
		// Shows Button locations via OnGUI function
		if(UI.showButtons && !UI.useStyle){
			GUI.Button(UI.MenuButton, "Menu");
			GUI.Button(UI.ListButton, "List");
			GUI.Button(UI.LeftSide1, "Left Button");
			GUI.Button(UI.RightSide1, "Right Button");
			GUI.Button(UI.LeftSide2, "Left Button");
			GUI.Button(UI.RightSide2, "Right Button");
		}
		if(UI.showButtons && UI.useStyle){
			GUI.Button(UI.MenuButton, "Menu", UI.ButtonGUIStyle);
			GUI.Button(UI.ListButton, "List", UI.ButtonGUIStyle);
			GUI.Button(UI.LeftSide1, "Left Button", UI.ButtonGUIStyle);
			GUI.Button(UI.RightSide1, "Right Button", UI.ButtonGUIStyle);
			GUI.Button(UI.LeftSide2, "Left Button", UI.ButtonGUIStyle);
			GUI.Button(UI.RightSide2, "Right Button", UI.ButtonGUIStyle);
		}
	}
}
