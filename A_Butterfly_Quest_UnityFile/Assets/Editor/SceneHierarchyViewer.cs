using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;


[InitializeOnLoad]
public class SceneHierarchyViewer
{
    public static bool scenePreviewing = false;
    static SceneHierarchyViewer()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;
    }

    static void HierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {
        if (!scenePreviewing)
        {
            return;
        }

        GameObject currentGameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
        if(currentGameObject != null)
        {
            if (currentGameObject.scene == SceneManager.GetActiveScene())
            {
                return;
            }
        }
        else
        {
            return;
        }

        MakePreviewHierarchyObject(currentGameObject, selectionRect);
    }

    static void MakePreviewHierarchyObject(GameObject activeObject, Rect m_rect)
    {
        Rect textRect = new Rect(m_rect);
        textRect.x += textRect.width - 65;
        GUIStyle textStyle = new GUIStyle();
        textStyle.fontStyle = FontStyle.Italic;
        textStyle.font = EditorStyles.boldFont;
        textStyle.richText = true;
        GUI.Label(textRect, "<color=cyan>PREVIEW</color>",textStyle);

        Texture2D backgroundTex = new Texture2D(1, 1, TextureFormat.RGBA32, false);
        backgroundTex.SetPixel(0, 0, new Color(0.4f, 0.4f, 0.5f,0.5f));
        backgroundTex.Apply();
        GUI.DrawTexture(m_rect, backgroundTex);
    }

}
