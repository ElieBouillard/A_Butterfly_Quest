using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;
using System;
using System.Reflection;

public class VisualDebug : MonoBehaviour
{
    public bool showLogs;

    float heightRect = 20;

    public static List<string> logsNames = new List<string>();
    public static List<string> logsValues = new List<string>();
    public static List<Color> logsColor = new List<Color>();

    private void Reset()
    {
        name = "__VISUAL DEBUG__";
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            showLogs = !showLogs;
        }
    }


    /// <summary>
    /// Display passed string on screen as GUI
    /// </summary>
    /// <param name="str">string to show</param>
    public static void Log(object log, string title = "", Color? optionalColor = null)
    {
        if(!logsNames.Contains(title))
        {
            if (optionalColor != null)
            {
                logsColor.Add(optionalColor.GetValueOrDefault());
            }
            else
            {
                logsColor.Add(Color.white);
            }

            logsNames.Add(title);
            logsValues.Add(log.ToString());
            //Debug.Log("LOGGING : " + log.ToString());
        }
        else
        {
            logsValues[logsNames.IndexOf(title)] = log.ToString();
        }
        

       
    }



    private void OnGUI()
    {
        if (showLogs)
        {
            for (int i = 0; i < logsNames.Count; i++)
            {
                GUI.color = logsColor[i];
                GUI.Label(new Rect(30, 10 + ((i) * heightRect), 200, heightRect), logsNames[i] + " : " + logsValues[i]);
            }
        }
        else
        {
            GUI.Label(new Rect(30, 10, 200, heightRect), "Press F12 to show logs");
        }
    
        
    }



    //[MenuItem("GameObject/Visual Debug/Initiate Visual Debug &v",false,10)]
    public static void NewVisualDebug()
    {
        GameObject newObject = new GameObject();
        newObject.name = "_GIZMO";
        newObject.AddComponent<VisualDebug>();
    }
}
