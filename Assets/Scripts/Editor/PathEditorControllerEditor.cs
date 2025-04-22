using System.Collections.Generic;
using PathEditor;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PathEditorController))]
public class PathEditorControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GUILayout.Space(10);

        PathEditorController controller = (PathEditorController)target;

        if (GUILayout.Button("Add Waypoint"))
        {
            GameObject newWaypoint = new GameObject("Waypoint_" + controller.transform.childCount);
            Undo.RegisterCreatedObjectUndo(newWaypoint, "Create Waypoint");
            newWaypoint.transform.SetParent(controller.transform);
            newWaypoint.transform.localPosition = Vector3.zero;

            SerializedObject serializedObject = new SerializedObject(controller);
            SerializedProperty transformsProp = serializedObject.FindProperty("transforms");

            transformsProp.arraySize++;
            transformsProp.GetArrayElementAtIndex(transformsProp.arraySize - 1).objectReferenceValue = newWaypoint.transform;
            serializedObject.ApplyModifiedProperties();

            Selection.activeGameObject = newWaypoint;
            EditorUtility.SetDirty(controller);
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Update Waypoint Positions"))
        {
            Undo.RecordObject(controller, "Update Waypoint Positions");

            controller.waypointPositions = new List<Vector3>();
            foreach (var t in controller.transforms)
            {
                if (t != null)
                    controller.waypointPositions.Add(t.position);
            }

            EditorUtility.SetDirty(controller);
        }
    }
}