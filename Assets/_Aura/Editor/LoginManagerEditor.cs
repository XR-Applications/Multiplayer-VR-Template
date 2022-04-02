using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LoginManager))]
public class LoginManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.HelpBox("This script is responsible for connecting to Photon servers.",MessageType.Info);

        LoginManager loginManager = target as LoginManager;
        if(GUILayout.Button("Connect Anonymously"))
        {
            loginManager.ConnectAnonymously();
        }
    }
}
