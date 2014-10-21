using UnityEngine;
using System.Collections;

// ---------------------------- //
// -- Class for SpriteSheets -- //
// ---------------------------- //
[System.Serializable]
public class __SpriteSheets{
	public string name;
	public Texture2D spriteSheet;
	public __Icons[] icons;

	public void DrawIcon(string iconName, Vector2 uiCoordinate){
		for(int i=0; i<icons.Length; i++){
			if(icons[i].name == iconName){
				GUI.BeginGroup(new Rect(uiCoordinate.x, uiCoordinate.y, icons[i].sheetCoordinates.width, icons[i].sheetCoordinates.height));
				GUI.DrawTexture(new Rect(-icons[i].sheetCoordinates.x, -icons[i].sheetCoordinates.y, 512-icons[i].sheetCoordinates.width, 512-icons[i].sheetCoordinates.height), spriteSheet, ScaleMode.ScaleAndCrop);
				GUI.EndGroup();
				break;
			}
		}
	}
	public Rect GetSize(string icon){
		Rect temp = new Rect(0, 0, 0, 0);
		for(int i=0; i<icons.Length; i++){
			if(icons[i].name == icon){
				temp = new Rect(-icons[i].sheetCoordinates.x, -icons[i].sheetCoordinates.y, icons[i].sheetCoordinates.width, icons[i].sheetCoordinates.height);
			}
		}
		return temp;
	}
}
// ---------------------------- //
// -- Class for SpriteSheets -- //
// ---------------------------- //
[System.Serializable]
public class __Icons{
	public string name;
	public Rect sheetCoordinates;
}
// ---------------------------- //
// Class for SpriteSheets
// ---------------------------- //
[System.Serializable]
public class __IconsCoordinates{
	public string name;
	public Vector2 iconCoordinates;
}
// ---------------------------- //
// ------ Class for UIGUI ----- //
// ---------------------------- //
[System.Serializable]
public class UIGUI{
	// button Positions and Sizes
	public Rect LeftSide1;
	public Rect LeftSide2;
	public Rect RightSide1;
	public Rect RightSide2;
	public Rect MenuButton;
	public Rect ListButton;
	
	public bool showButtons = false;

	public bool useStyle = false;
	public GUIStyle ButtonGUIStyle;
}