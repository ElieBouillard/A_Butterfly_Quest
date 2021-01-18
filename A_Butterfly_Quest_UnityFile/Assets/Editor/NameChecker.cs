using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



public class NameChecker : UnityEditor.AssetModificationProcessor
{
    static void OnWillCreateAsset(string assetName)
    {
        if(assetName.Substring(assetName.Length-4,4) != "meta")
        {
            NameCheckerWindow.assetPathName = assetName;
            NameCheckerWindow.ShowWindow(assetName);
        }
        else
        {
            //If we're reading a .meta file but the window hasn't popped up yet
            if(NameCheckerWindow.assetPathName != assetName.Substring(0,assetName.Length - 5))
            {
                NameCheckerWindow.assetPathName = assetName.Substring(0, assetName.Length - 5);
                NameCheckerWindow.ShowWindow(assetName.Substring(0, assetName.Length - 5));
            }
        }
        
    }
}



public class NameCheckerWindow : EditorWindow
{
    public static string assetPathName;
    string newName;
    private static Texture2D bg_tex;
    private static Texture2D tips_tex;

    public static void ShowWindow(string filename)
    {
        assetPathName = filename;
      
        // Create a new window of this type or focus an existing one
        var window = GetWindow<NameCheckerWindow>("Name Checker", true);
        // Setup and show window
        window.minSize = new Vector2(500, 180);
        window.ShowUtility();
        window.maxSize = new Vector2(500, 180);
        window.Show();

        bg_tex = new Texture2D(1, 1, TextureFormat.RGBA32, false);
        bg_tex.SetPixel(0, 0, new Color(0.40f, 0.42f, 0.45f));
        bg_tex.Apply();

        tips_tex = new Texture2D(1, 1, TextureFormat.RGBA32, false);
        tips_tex.SetPixel(0, 0, new Color(0.50f, 0.52f, 0.55f));
        tips_tex.Apply();
    }

    private void OnGUI()
    {
        //BG texture
        GUI.DrawTexture(new Rect(0, 0, maxSize.x, maxSize.y), bg_tex, ScaleMode.StretchToFill);
        GUI.DrawTexture(new Rect(0, 110, maxSize.x, maxSize.y-110), tips_tex, ScaleMode.StretchToFill);

        GUILayout.Space(10);
        GUIStyle nameStyle = new GUIStyle();
        nameStyle.fontSize = 20;

        //nameStyle.fontStyle = FontStyle.Bold;
        GUILayout.Label(" New name", nameStyle);
        //GUILayout.Label("(Ex : TYPE_CATEGORY_Name_V02)");
        //GUILayout.Space(10);

        newName = GUILayout.TextField(newName);

        if (newName == " " || newName == string.Empty || newName == null)
        {
            if(assetPathName != "")
            {
                newName = assetPathName.Split('/')[assetPathName.Split('/').Length - 1].Split('.')[0];
            }
            
        }

        GUIStyle pathStyle = new GUIStyle();
        pathStyle.fontStyle = FontStyle.Italic;
        pathStyle.fontSize = 11;
        GUILayout.Label(" " + NameCheckerWindow.assetPathName, pathStyle);
        GUILayout.Space(10);


        // Draw a button that prints "Hello World" to the console
        bool buttonWasPressed = GUILayout.Button("The name is OK !");
        if (buttonWasPressed)
        {
            if (newName != assetPathName.Split('/')[assetPathName.Split('/').Length - 1].Split('.')[0])
            {
                AssetDatabase.RenameAsset(assetPathName, newName);
                AssetDatabase.RenameAsset(assetPathName + ".meta", newName + ".meta");
                Debug.Log("Successfully changed name");
            }

            GetWindow<NameCheckerWindow>().Close();

        }

        GUILayout.Space(10);

        GUIStyle instructionsStyle = new GUIStyle();
        instructionsStyle.fontStyle = FontStyle.Italic;
        instructionsStyle.fontSize = 8;
        GUILayout.Label("-Chaque première lettre est une majuscule \n-Doit avoir au moins un préfixe \n-Pas de caractères spéciaux (#,!,ù,%,...) \n-Pas besoin de mettre l'extension à la fin", pathStyle);

    }
}
