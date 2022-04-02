using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RoomManager))]
public class RoomManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.HelpBox("This script joins a random room (creating one if none)",MessageType.Info);

        RoomManager roomManager = (RoomManager)target;
        if(GUILayout.Button("Join Random Room"))
        {
            roomManager.JoinRandomRoom();
        }
    }
}
