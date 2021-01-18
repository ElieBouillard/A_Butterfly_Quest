using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEditor.SceneManagement;

public class SceneLoader : Editor
{
    static string scenesDirectoryPath = "Assets/Scenes";
    static string[] scenesPaths;

    public static void InitiateSceneFolder()
    {
        scenesDirectoryPath = SceneManager.GetActiveScene().path;
        scenesDirectoryPath = scenesDirectoryPath.Replace(SceneManager.GetActiveScene().name + ".unity", "");
    }

    public static void FindScenes()
    {
        //get scenes
        scenesPaths = Directory.GetFiles(scenesDirectoryPath, "*.unity");
    }

    [MenuItem("SceneLoader/Load All Scenes")]
    public static void LoadScenes()
    {
        SceneHierarchyViewer.scenePreviewing = false;
        InitiateSceneFolder();
        FindScenes();
        for (int i = 0; i < scenesPaths.Length; i++)
        {
            EditorSceneManager.OpenScene(scenesPaths[i], OpenSceneMode.Additive);
        }
    }

    [MenuItem("SceneLoader/Preview Scenes")]
    public static void PreviewScenes()
    {
        SceneHierarchyViewer.scenePreviewing = true;
        InitiateSceneFolder();
        FindScenes();
        for (int i = 0; i < scenesPaths.Length; i++)
        {
            EditorSceneManager.OpenScene(scenesPaths[i], OpenSceneMode.Additive);
        }

    }


    [MenuItem("SceneLoader/Close Unactive Scenes")]
    public static void CloseUnactiveScenes()
    {
        int scenesCount = EditorSceneManager.loadedSceneCount;
        for (int i = 0; i < scenesCount; i++)
        {
            if (EditorSceneManager.GetSceneAt(i) != EditorSceneManager.GetActiveScene())
            {
                EditorSceneManager.UnloadSceneAsync(EditorSceneManager.GetSceneAt(i));
            }
           
        }
        SceneHierarchyViewer.scenePreviewing = false;
    }


}

