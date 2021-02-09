//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEditor;
//using UnityEngine;


//[CustomEditor(typeof(GizmoVolume))]
//public class GizmoVolumeEditor : Editor
//{

//    public override void OnInspectorGUI()
//    {
//        GizmoVolume m_target = (GizmoVolume)target;
//        //DrawDefaultInspector();

//        if (!m_target.Hidden)
//        {
//            EditorGUILayout.LabelField("Gizmo Previewer", "Easily implement gizmos in your scene");

//            m_target.volumeType = (GizmoVolume.VolumeType)EditorGUILayout.EnumPopup(m_target.volumeType);

//            GUILayout.Space(10);

//            m_target.color = EditorGUILayout.ColorField("Volume Color", m_target.color);

//            if (m_target.volumeType != GizmoVolume.VolumeType.Line)
//            {
//                m_target._3DSize = GUILayout.Toggle(m_target._3DSize, "3D Size");
//                m_target.fitCollisionSize = GUILayout.Toggle(m_target.fitCollisionSize, "Fit size to Collision Box");
//                if (m_target._3DSize)
//                {
//                    //EditorGUILayout.HelpBox("This is a help box", MessageType.Info);
//                    m_target.size = EditorGUILayout.Vector3Field("Size", m_target.size);
//                }
//                else
//                {
//                    m_target.size.x = EditorGUILayout.FloatField("Size", m_target.size.x);
//                    m_target.size.y = m_target.size.x;
//                    m_target.size.z = m_target.size.x;
//                }

//                m_target.OffsetPosition = EditorGUILayout.Vector3Field("Offset Position", m_target.OffsetPosition);

               
//            }
//            else
//            {
//                m_target.OffsetPosition = EditorGUILayout.Vector3Field("Start Point", m_target.OffsetPosition);
//                m_target.size = EditorGUILayout.Vector3Field("End Point", m_target.size);
//                m_target.targetLine = EditorGUILayout.ObjectField("Target", m_target.targetLine, typeof(GameObject), true);               
//            }

//            if (m_target.volumeType == GizmoVolume.VolumeType.Icon)
//            {
//                m_target.iconNameIndex = EditorGUILayout.IntSlider("Icon", m_target.iconNameIndex, 0, m_target.defaultIcons.Length - 1);
//                EditorGUILayout.HelpBox("To Add Icons, Edit the 'defaultIcons' array in the code", MessageType.Info);
//            }


//            //FETCHING VARIABLES

//            GUILayout.Space(10);


//            m_target._fetchVariable = GUILayout.Toggle(m_target._fetchVariable, "Fetch Value Outside");
//            if (m_target._fetchVariable)
//            {

//                m_target.fetchedComponent = (Component)EditorGUILayout.ObjectField("Component to fetch from", m_target.fetchedComponent, typeof(Component), true);

//                if (m_target.fetchedVariables_names.Count > 0)
//                {
//                    m_target.fetchedVarIndex = EditorGUILayout.Popup("Variables", m_target.fetchedVarIndex, m_target.fetchedVariables_names.ToArray());
//                }
//                else
//                {
//                    m_target.UpdateComponents();
//                }

//                m_target.fetchedVarRealtimeUpdate = GUILayout.Toggle(m_target.fetchedVarRealtimeUpdate, "Update variable in Realtime");

               

//                if (m_target.fetchedVarRealtimeUpdate)
//                {
//                    m_target._UpdateFetchedVarOnlyOnFocus = GUILayout.Toggle(m_target._UpdateFetchedVarOnlyOnFocus, "Update only when selected");
//                    if (m_target._UpdateFetchedVarOnlyOnFocus && m_target.fetchedVarIndex > 0)
//                    {
//                        m_target.UpdateVar();
//                    }
                    
//                }
//                else
//                {
//                    if (GUILayout.Button("Update Component Variables"))
//                    {
//                        m_target.UpdateComponents();
//                    }
//                }

//                if (m_target._wrongFetch)
//                {
//                    EditorGUILayout.HelpBox("WRONG FORMAT, Chosen variable is not valid.", MessageType.Warning);
//                }
                
//                //EditorGUILayout.LabelField("var index", m_target.fetchedVariables_values.Count.ToString());


//            }


//            //TEXT

//            GUILayout.Space(10);

//            m_target.addText = GUILayout.Toggle(m_target.addText, "Toggle Text");

//            if (m_target.addText)
//            {              
//                m_target.areaText = EditorGUILayout.TextField("Helper Text", m_target.areaText);
//                m_target.textSize = EditorGUILayout.IntField("Text Size", m_target.textSize);
//                m_target.textOffset = EditorGUILayout.Vector3Field("Text Offset", m_target.textOffset);
//                m_target.textColor = EditorGUILayout.ColorField("Text Color", m_target.textColor);
//                m_target.hideTextUnselected = GUILayout.Toggle(m_target.hideTextUnselected, "Hide if unselected");
//                if (m_target.hideTextUnselected)
//                {
//                    EditorGUILayout.HelpBox("Text only appears when the object is selected", MessageType.Info);
//                }
                

//            }

//            GUILayout.Space(10);

//            if (GUILayout.Button("Hide Inspector"))
//            {
//                m_target.Hidden = true;
//            }

            

//        }
//        else
//        {
//            if(GUILayout.Button("Unhide Inspector"))
//            {
//                m_target.Hidden = false;
//            }
//        }



//        EditorUtility.SetDirty(m_target);
//    }
    

//    private void OnSceneGUI()
//    {
//        GizmoVolume m_target = (GizmoVolume)target;

//        //Handles
//       /* Handles.PositionHandle(
//                m_target.transform.position,
//                m_target.transform.rotation);*/
//        Handles.BeginGUI();

//        //Text

//        if (m_target.hideTextUnselected)
//        {

//            var rectMin = Camera.current.WorldToScreenPoint(
//            m_target.transform.position + m_target.OffsetPosition +
//            m_target.textOffset);
//            var rect = new Rect();

//            int widthSize = 256;
//            int heightSize = 128;

//            rect.xMin = rectMin.x - widthSize / 2;
//            rect.yMin = Screen.height - 30 -
//                rectMin.y;
//            rect.width = widthSize;
//            rect.height = heightSize;
//            GUILayout.BeginArea(rect);
//            GUIStyle style = new GUIStyle();
//            style.fontSize = m_target.textSize;
//            style.alignment = TextAnchor.MiddleCenter;
//            style.normal.textColor = m_target.textColor;
//            if (m_target.addText)
//            {
//                GUILayout.Label(m_target.areaText, style);
//            }
//            GUILayout.EndArea();

//        }




        
//        Handles.EndGUI();
//    }
//}
