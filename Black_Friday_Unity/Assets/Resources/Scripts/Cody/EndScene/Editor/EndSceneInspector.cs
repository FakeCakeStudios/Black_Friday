using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EndScene))]
public class EndSceneInspector : Editor {
	private void OnSceneGUI(){
		EndScene scene = target as EndScene;
		Transform handleTransform = scene.transform;
		Quaternion handleRotation = Tools.pivotRotation == PivotRotation.Local ?
			handleTransform.rotation : Quaternion.identity;
		Vector3 playerPosition = handleTransform.TransformPoint(scene.playerPosition);
		Vector3 cameraPosition = handleTransform.TransformPoint(scene.cameraPosition);
		Vector3 scorePosition = handleTransform.TransformPoint(scene.scorePosition);

		EditorGUI.BeginChangeCheck();
		playerPosition = Handles.DoPositionHandle(playerPosition, handleRotation);
		if(EditorGUI.EndChangeCheck()){
			Undo.RecordObject(scene, "Move Point");
			EditorUtility.SetDirty(scene);
			scene.playerPosition = handleTransform.InverseTransformPoint(playerPosition);
		}
		EditorGUI.BeginChangeCheck();
		cameraPosition = Handles.DoPositionHandle(cameraPosition, handleRotation);
		if(EditorGUI.EndChangeCheck()){
			Undo.RecordObject(scene, "Move Point");
			EditorUtility.SetDirty(scene);
			scene.cameraPosition = handleTransform.InverseTransformPoint(cameraPosition);
		}
		EditorGUI.BeginChangeCheck();
		scorePosition = Handles.DoPositionHandle(scorePosition, handleRotation);
		if(EditorGUI.EndChangeCheck()){
			Undo.RecordObject(scene, "Move Point");
			EditorUtility.SetDirty(scene);
			scene.scorePosition = handleTransform.InverseTransformPoint(scorePosition);
		}
				
		Handles.Label(playerPosition, string.Format("Player: {0}", playerPosition));
		Handles.Label(cameraPosition, string.Format("Camera: {0}", cameraPosition));
		Handles.Label(scorePosition, string.Format("Score: {0}", scorePosition));
	}
}
