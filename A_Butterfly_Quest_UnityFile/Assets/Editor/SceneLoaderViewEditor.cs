using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[InitializeOnLoad]
public class SceneLoaderViewEditor : Editor
{
    static Rect buttonRect = new Rect(Vector2.zero, new Vector2(150,40));
    static SceneLoaderViewEditor()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    static void OnSceneGUI(SceneView sceneView)
    {
        Handles.BeginGUI();
        
        if (!SceneHierarchyViewer.scenePreviewing)
        {

            if(GUI.Button(buttonRect,"Preview All Scenes"))
            {
                SceneLoader.PreviewScenes();
            }
           
        }
        else
        {
            if (GUI.Button(buttonRect,"Stop Preview"))
            {
                SceneLoader.CloseUnactiveScenes();
            }
            
        }
       
        Handles.EndGUI();

    }
}
