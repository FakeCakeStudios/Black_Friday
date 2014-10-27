// Object Replacement Tool, v1.0.4 - Last Modified at 11:47 PM on Thursday, 7th day of August, 2014.
// Developed by Tylah Heil at Silver Fox Games.
// View the 'readme.txt' file for detailed instructions on how to use this Editor Extension.
// Visit http://www.silver-fox-media.com/ for more information on our products. 

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ObjectReplacementTool : EditorWindow {
	
	private List<GameObject> currentSelection = new List<GameObject>();			// The selected GameObjects.
	
	public GameObject replacementObject;										// GameObject to replace selection with.
	
	public int numberToAdd = 1;													// The number of objects to be instantiated at the selection (Add Only).
	
	public string objectName = "";												// The string used when searching for an object by name.
	
	public bool maintainPosition = true;										// If true, replacement object assumes the scene object's position.
	public bool maintainRotation = true;										// If true, replacement object assumes the scene object's rotation.
	public bool maintainScale = true;											// If true, replacement object assumes the scene object's scale.
	
	public bool offset = false;
	
	public Vector3 offsetPosition = Vector3.zero;								// Offset the position of the replacement relative to the selection.
	public Vector3 offsetRotation = Vector3.zero;								// Offset the rotation of the replacement relative to the selection.
	public Vector3 offsetScale = Vector3.zero;									// Offset the scale of the replacement relative to the selection.
	
	public ManageObject manageObject;											// Alternately the replacement object can be added without destroying the selection. 
	public enum ManageObject { Replace, Add }
	
	public HierarchyParameters hierarchyParameters;
	public enum HierarchyParameters { None, MaintainHierarchy, MaintainChildren, MaintainParent }
	
	private bool isSelectionPersistent = false;
	private bool isReplacementPersistent = true;
	
	private Vector2 scrollView = Vector2.zero;
	
	[MenuItem("GameObject/Replace Object...")]
	
	static void Init() {
		// Get the Object Replacement Tool Window.
		ObjectReplacementTool window = (ObjectReplacementTool)EditorWindow.GetWindow(typeof(ObjectReplacementTool), false, "Replace Object"); // Set boolean to true if you don't want to dock the window.
		window.minSize = new Vector2(420, 320);
		window.maxSize = new Vector2(480, 384);
	}
	
	private void OnGUI() {
		if (Selection.objects.Length > 0) {
			currentSelection = Selection.objects.OfType<GameObject>().ToList();
		} else {
			currentSelection.Clear();
		}
		
		// Check that a selection has been made.
		if (Selection.gameObjects.Length > 0) {
			isSelectionPersistent = EditorUtility.IsPersistent(Selection.activeGameObject);
		} else {
			isSelectionPersistent = false;
		}
		
		// A check to ensure that the replacement comes from the Project Window.
		if (replacementObject != null) {
			isReplacementPersistent = EditorUtility.IsPersistent(replacementObject);
		} else {
			isReplacementPersistent = true;
		}
		
		// Start Scroll View
		scrollView = EditorGUILayout.BeginScrollView(scrollView);
		
		GUILayout.Label("Select a GameObject from the Scene or Hierarchy... ");
		EditorGUILayout.Separator();
		GUILayout.BeginHorizontal();
		objectName = EditorGUILayout.TextField(new GUIContent("Search", "Search for, and select all objects by name."), objectName);
		if (objectName == string.Empty) {
			GUI.enabled = false;
		}
		if (GUILayout.Button("Find and Select")) {
			List<GameObject> objectsToSelect = new List<GameObject>();
			foreach (GameObject go in GameObject.FindObjectsOfType<GameObject>()) {
				if (go.name == objectName) {
					objectsToSelect.Add(go);
				}
			}
			if (objectsToSelect.Count > 0) {
				Selection.objects = objectsToSelect.ToArray();
			}
		}
		GUI.enabled = true;
		GUILayout.EndHorizontal();
		EditorGUILayout.Separator();
		manageObject = (ManageObject)EditorGUILayout.EnumPopup(new GUIContent("Function", "The selection can either be replaced by the Replacement Object, or the Replacement Object be added without removing the selection from the scene."), manageObject);
		hierarchyParameters = (HierarchyParameters)EditorGUILayout.EnumPopup(new GUIContent("Hierarchy Parameters", "Optional Parameters for retention of Hierarchy status."), hierarchyParameters);
		replacementObject = EditorGUILayout.ObjectField(new GUIContent("Replacement Object", "The object that will replace the current selection."), replacementObject, typeof(GameObject), true) as GameObject;
		
		if (manageObject == ManageObject.Add) {
			GUI.enabled = true;
		} else {
			GUI.enabled = false;
			// numberToAdd is reset to one.
			numberToAdd = 1;
		}
		
		numberToAdd = EditorGUILayout.IntField(new GUIContent("Number to Add", "The number of times the object will be instantiated at each selection."), (int)Mathf.Clamp(numberToAdd, 1, Mathf.Infinity));
		GUI.enabled = true;
		GUILayout.Label("Maintain Transform");
		EditorGUILayout.BeginHorizontal();
		maintainPosition = GUILayout.Toggle(maintainPosition, new GUIContent("Position", "Will the Replacement Object inherit the currently selected object's position?"));
		maintainRotation = GUILayout.Toggle(maintainRotation, new GUIContent("Rotation", "Will the Replacement Object inherit the currently selected object's rotation?"));
		maintainScale = GUILayout.Toggle(maintainScale, new GUIContent("Scale", "Will the Replacement Object inherit the currently selected object's scale?"));
		EditorGUILayout.EndHorizontal();
		offset = EditorGUILayout.Foldout(offset, new GUIContent("Offset Transform", "Manually define the offset values of the replacement transform."));
		
		if (offset) {
			offsetPosition = EditorGUILayout.Vector3Field("Position", offsetPosition);
			offsetRotation = EditorGUILayout.Vector3Field("Rotation", offsetRotation);
			offsetScale = EditorGUILayout.Vector3Field("Scale", offsetScale);
		}
		
		// Disable the button if we don't have a selected object or replacement assigned.
		if (Selection.gameObjects.Length > 0 && !isSelectionPersistent && replacementObject != null && isReplacementPersistent) {
			GUI.enabled = true;
		} else {
			GUI.enabled = false;
		}
		
		if (manageObject == ManageObject.Add && hierarchyParameters != HierarchyParameters.None && numberToAdd > 1) {
			GUI.enabled = false;
		}
		
		EditorGUILayout.Separator();
		if (GUILayout.Button(manageObject + " Selection")) {
			Replace();
		}
		
		GUI.enabled = true;
		
		// Display errors at the bottom of the window as they are needed.
		if (Selection.gameObjects.Length == 0) {
			EditorGUILayout.HelpBox("No Object Selected.", MessageType.Error, true);
		} else if (replacementObject == null) {
			EditorGUILayout.HelpBox("No Replacement Object was Assigned.", MessageType.Error, true);
		} else if (!isReplacementPersistent) {
			EditorGUILayout.HelpBox("Replacement Object must be added from the Project Window.", MessageType.Error, true);
		} else if (isSelectionPersistent) {
			EditorGUILayout.HelpBox("Selection must be made from the Scene or Hierarchy Window.", MessageType.Error, true);
		} else if (manageObject == ManageObject.Add && hierarchyParameters != HierarchyParameters.None && numberToAdd > 1) {
			EditorGUILayout.HelpBox("Hierarchy Parameters are only available if 'Number to Add' is equal to one.", MessageType.Error, true);
		}
		
		EditorGUILayout.EndScrollView();
	}
	
	private void Replace() {
		// This operation is performed multiple times if 'numberToAdd' is greater than one.
		for (int i = 1; i <= numberToAdd; i++) {
			// Itterate the selection and make modifications as necessary.
			foreach (GameObject selection in currentSelection) {
				// Using 'PrefabUtility.InstantiatePrefab' we can instantiate the actual prefab as though it were dragged straight from the project window rather than creating a Clone.
				GameObject replacement = PrefabUtility.InstantiatePrefab(replacementObject) as GameObject;
				
				// Store children.
				Transform[] children = selection.GetComponentsInChildren<Transform>();
				
				// If the selection is not a root object and we need to retain hierarchy parameters do so now.
				if (selection.transform.parent != null && hierarchyParameters == HierarchyParameters.MaintainHierarchy || selection.transform.parent != null && hierarchyParameters == HierarchyParameters.MaintainParent) {
					replacement.transform.parent = selection.transform.parent;
				}
				
				// Set up the position of the new object
				if (maintainPosition) {
					if (hierarchyParameters == HierarchyParameters.None) {
						replacement.transform.position = selection.transform.position + (offsetPosition * i);
					} else {
						replacement.transform.localPosition = selection.transform.localPosition + (offsetPosition * i);
					}
				} else {
					replacement.transform.localPosition = Vector3.zero;
				}
				
				// Set up the rotation of the new object
				if (maintainRotation) {
					replacement.transform.eulerAngles = selection.transform.eulerAngles + (offsetRotation * i);
				} else {
					replacement.transform.eulerAngles = Vector3.zero;
				}
				
				// Set up the scale of the new object
				if (maintainScale) {
					replacement.transform.localScale = selection.transform.localScale + (offsetScale * i);
				} else {
					replacement.transform.localScale = Vector3.one;
				}
				
				if (manageObject != ManageObject.Add) {
					// Register a Hierarchy Undo on the Selection.
					Undo.RegisterFullObjectHierarchyUndo(selection, manageObject.ToString());
				}
				
				// If the selection has children in the hierarchy and we need to retain them do so now.
				if (selection.transform.childCount > 0 && hierarchyParameters == HierarchyParameters.MaintainHierarchy || selection.transform.childCount > 0 && hierarchyParameters == HierarchyParameters.MaintainChildren) {
					foreach (Transform child in children) {
						if (child.parent == selection.transform) {
							if (manageObject == ManageObject.Add) {
								// Register a Hierarchy Undo on the child.
								Undo.RegisterFullObjectHierarchyUndo(child.gameObject, manageObject.ToString());
							}
							
							// Temporarily store each child's transform values so we can restore them afterward.
							Vector3 storedPosition = child.localPosition;
							Vector3 storedRotation = child.localEulerAngles;
							Vector3 storedScale = child.localScale;
							child.parent = replacement.transform;
							child.localPosition = storedPosition;
							child.localEulerAngles = storedRotation;
							child.localScale = storedScale;
						}
					}
				}
				
				// If we are replacing the asset remove the selection. 
				if (manageObject == ManageObject.Replace) {
					DestroyImmediate(selection);
				}
				
				// Register the created objec so that it will be destroyed if we undo the opperation.
				Undo.RegisterCreatedObjectUndo(replacement, manageObject.ToString() + " Selection");
			}
		}
	}
	
	private void OnInspectorUpdate() {
		Repaint();
	}
}