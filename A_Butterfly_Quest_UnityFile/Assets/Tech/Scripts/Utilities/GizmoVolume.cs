//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor;
//using UnityEngine;
//using System;
//using System.Linq;
//using System.Reflection;
//using UnityEngine.PlayerLoop;
//using System.CodeDom;

//[ExecuteInEditMode]

//public class GizmoVolume : MonoBehaviour
//{
//    public bool Hidden;
//    public bool activated = true; //only used for disabling gizmo inside code.
//    public enum VolumeType { Cube, WireCube, Sphere, WireSphere, Line, Icon };
//    public VolumeType volumeType = new VolumeType();
//    public Color color = Color.white;
//    public Vector3 OffsetPosition;
//    public bool _3DSize;
//    public Vector3 size = Vector3.one;
//    public bool fitCollisionSize;
//    Collider m_collider;
//    Vector3 sizeOffset;
//    public string Name;

//    public bool addText;
//    public string areaText;
//    public bool hideTextUnselected;
//    public int textSize = 20;
//    public Vector3 textOffset;
//    public Color textColor = Color.white;

//    public UnityEngine.Object targetLine;
//    public int iconNameIndex;

//    public Component fetchedComponent;
//    Component lastFetchedComponent;
//    object lastFetchedValue;
//    public bool _fetchVariable;
//    public bool fetchedVarRealtimeUpdate;
//    public bool _UpdateFetchedVarOnlyOnFocus;
//    public List<string> fetchedVariables_names = new List<string>();
//    public List<object> fetchedVariables_values = new List<object>();
//    public int fetchedVarIndex = 0;
//    public bool _wrongFetch;

//     >> Find Default Icon Names https://unitylist.com/p/5c3/Unity-editor-icons <<
//    public string[] defaultIcons = new string[17] { "d_tree_icon_frond", "d_tree_icon_leaf", "GameManager Icon", "LightProbeGroup Gizmo", "CharacterController Icon", "CompositeCollider2D Icon", "PackageBadgeNew", "Assembly Icon", "CollabEdit Icon", "d_CollabConflict Icon", "Favorite Icon", "EdgeCollider2D Icon", "NetworkStartPosition Icon", "VideoEffect Icon", "VisualEffectAsset Icon", "VisualEffect Gizmo", "CloudRecoBehaviour Icon" };



//    private void Reset()
//    {
//        set default size to the object's size
//        size = transform.localScale;

//        defaultIcons = new string[17] { "d_tree_icon_frond", "d_tree_icon_leaf", "GameManager Icon", "LightProbeGroup Gizmo", "CharacterController Icon", "CompositeCollider2D Icon", "PackageBadgeNew", "Assembly Icon", "CollabEdit Icon", "d_CollabConflict Icon", "Favorite Icon", "EdgeCollider2D Icon", "NetworkStartPosition Icon", "VideoEffect Icon", "VisualEffectAsset Icon", "VisualEffect Gizmo", "CloudRecoBehaviour Icon" };


//        Rename automatically newly created object
//        if (name.Length >= 10)
//        {
//            if (name.Substring(0, 10) == "GameObject")
//            {
//                gameObject.name = "_GIZMO";
//            }
//        }

//        Hidden = false;
//        activated = true;
//        fetchedVarIndex = 0;
//        _fetchVariable = false;
//        fetchedVarRealtimeUpdate = false;
//        _UpdateFetchedVarOnlyOnFocus = false;
//        _wrongFetch = false;

//    }


//    public void UpdateComponents()
//    {
//        accessibleVariables = new Dictionary<string, object>();
//        fetchedVariables_names = new List<string>();
//        fetchedVariables_values = new List<object>();
//        fetchedVariables_names.Clear();
//        fetchedVariables_values.Clear();

//        fetchedVariables_names.Add("None");
//        fetchedVariables_values.Add(null);

//        if (fetchedComponent == null)
//        {
//            return;
//        }

//        Type myObjectType = fetchedComponent.GetType();

//        if (myObjectType.IsSubclassOf(typeof(MonoBehaviour)))
//        {
//            if (myObjectType != typeof(GizmoVolume))
//            {
//                var fields = myObjectType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);

//                for (int i = 0; i < fields.Length; i++)
//                {
//                    if (!fetchedVariables_names.Contains(fetchedComponent.GetType().ToString() + "/" + fields[i].ToString()))
//                    {
//                        fetchedVariables_names.Add(fetchedComponent.GetType().ToString() + "/" + fields[i].ToString());
//                        fetchedVariables_values.Add(fields[i].GetValue(fetchedComponent));
//                    }

//                }

//            }


//        }
//        else
//        {
//            var properties = myObjectType.GetProperties();
//            for (int i = 0; i < properties.Length; i++)
//            {
//                if (!fetchedVariables_names.Contains(fetchedComponent.GetType().ToString() + "/" + properties[i].Name + " (" + properties[i].PropertyType + ")"))
//                {
//                    if (!properties[i].IsDefined(typeof(ObsoleteAttribute), true))  //filter out deprecated properties
//                    {
//                        fetchedVariables_names.Add(fetchedComponent.GetType().ToString() + "/" + properties[i].Name + " (" + properties[i].PropertyType + ")");
//                        fetchedVariables_values.Add(properties[i].GetValue(fetchedComponent, null));
//                    }

//                }

//            }

//        }

//    }



//    public void UpdateVar()
//    {
//        Component[] myComponents = GetComponents(typeof(Component)); //get all components on that gameObject       
//        accessibleVariables_refs[sizeVarIndex] = myComponents[sizeVarCompIndex].GetType().GetField(accessibleVariables_names[sizeVarIndex]);
//        if (fetchedComponent.GetType().IsSubclassOf(typeof(MonoBehaviour)))
//        {

//            if (fetchedVariables_values.Count > fetchedVarIndex)
//            {
//                fetchedVariables_values[fetchedVarIndex] = fetchedComponent.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)[fetchedVarIndex - 1].GetValue(fetchedComponent);
//            }
//            else
//            {
//                UpdateComponents();
//            }

//        }
//        else
//        {
//            if (fetchedVariables_values.Count > fetchedVarIndex)
//            {
//                accessibleVariables_refs[sizeVarIndex] = sizeVarRef.GetType().GetProperties()[sizeVarIndex].GetValue(sizeVarRef, null);
//                if (!fetchedComponent.GetType().GetProperties()[fetchedVarIndex - 1].IsDefined(typeof(ObsoleteAttribute), true))  //filter out deprecated properties
//                {
//                    fetchedVariables_values[fetchedVarIndex] = fetchedComponent.GetType().GetProperties()[fetchedVarIndex - 1].GetValue(fetchedComponent, null);
//                }

//            }
//            else
//            {
//                UpdateComponents();
//            }

//        }

//    }

//    void OnDrawGizmos()
//    {
//        object fetchedValue = null;

//        if (fetchedComponent != lastFetchedComponent) //if we just changed the component, automatically update
//        {
//            UpdateComponents();
//            _wrongFetch = false;
//            lastFetchedComponent = fetchedComponent;
//        }

//        if (fetchedVarIndex != 0 && _fetchVariable)
//        {
//            if updaterealtime
//            if (!_UpdateFetchedVarOnlyOnFocus && fetchedVarRealtimeUpdate)
//                {
//                    UpdateVar();
//                }


//            if (fetchedVariables_values.Count > 0)
//            {
//                size.x = (float)accessibleVariables.TryGetValue(accessibleVariables.Keys.ToArray()[sizeVarIndex], out getSize);
//                fetchedValue = fetchedVariables_values[fetchedVarIndex];
//                if (fetchedValue != lastFetchedValue) //if we just changed the value
//                {
//                    _wrongFetch = false;
//                    lastFetchedValue = fetchedValue;
//                }

//            }

//        }





//        if (fitCollisionSize)
//        {
//            if (!_3DSize)
//            {
//                _3DSize = true;
//            }

//            if (m_collider == null)
//            {
//                m_collider = GetComponent<Collider>();
//            }

//        }

//        if (volumeType == VolumeType.Line)
//        {
//            sizeOffset = transform.position;
//            if (targetLine != null)
//            {
//                size = ((GameObject)targetLine).transform.position;
//                sizeOffset = Vector3.zero;
//            }
//            if (_fetchVariable)
//            {
//                if (fetchedVarIndex != 0 && fetchedVariables_values.Count > 0)
//                {

//                    if (fetchedValue.GetType() == typeof(Vector3))
//                    {
//                        size = (Vector3)fetchedValue;
//                    }
//                    else if (fetchedValue.GetType() == typeof(GameObject))
//                    {
//                        GameObject fetchedGameObject = (GameObject)fetchedValue;
//                        size = fetchedGameObject.transform.position;
//                    }
//                    else if (fetchedValue.GetType() == typeof(Transform))
//                    {
//                        Transform fetchedTransform = (Transform)fetchedValue;
//                        size = fetchedTransform.position;
//                        _wrongFetch = false;
//                    }
//                    else if (fetchedValue.GetType() == typeof(Single))
//                    {
//                        size = Vector3.one * (float)fetchedValue;
//                        _wrongFetch = false;
//                    }
//                    else if (fetchedValue.GetType() == typeof(System.Boolean))
//                    {
//                        size = Vector3.one * ((bool)fetchedValue ? 1 : 0);
//                    }
//                    else if (fetchedValue.GetType() == typeof(System.Double))
//                    {
//                        size = Vector3.one * (float)fetchedValue;
//                    }
//                    else if (fetchedValue.GetType() == typeof(float))
//                    {
//                        size = Vector3.one * (float)fetchedValue;
//                    }
//                    else if (fetchedValue.GetType() == typeof(int))
//                    {
//                        size = Vector3.one * (int)fetchedValue;
//                    }
//                    else if (fetchedValue.GetType() == typeof(Quaternion))
//                    {
//                        size = (Vector4)fetchedValue;
//                    }
//                    else if (fetchedValue.GetType() == typeof(Vector2))
//                    {
//                        size = (Vector2)fetchedValue;
//                        _wrongFetch = false;
//                    }
//                    else
//                    {
//                        _wrongFetch = true;
//                    }

//                }
//            }


//        }
//        else
//        {
//            sizeOffset = Vector3.zero;

//            if (m_collider != null && fitCollisionSize)
//            {
//                size = m_collider.bounds.size;
//                if (m_collider.GetType() == typeof(SphereCollider) && (volumeType == VolumeType.Sphere || volumeType == VolumeType.WireSphere))
//                {
//                    size = m_collider.bounds.extents;
//                }

//            }

//            if (fetchedValue != null)
//            {
//                if (fetchedValue.GetType() == typeof(Vector3))
//                {
//                    size = (Vector3)fetchedValue;
//                }
//                else if (fetchedValue.GetType() == typeof(System.Single))
//                {
//                    size = Vector3.one * (float)fetchedValue;
//                }
//                else if (fetchedValue.GetType() == typeof(System.Boolean))
//                {
//                    size = Vector3.one * ((bool)fetchedValue ? 1 : 0);
//                }
//                else if (fetchedValue.GetType() == typeof(System.Double))
//                {
//                    size = Vector3.one * (float)fetchedValue;
//                }
//                else if (fetchedValue.GetType() == typeof(float))
//                {
//                    size = Vector3.one * (float)fetchedValue;
//                }
//                else if (fetchedValue.GetType() == typeof(int))
//                {
//                    size = Vector3.one * (int)fetchedValue;
//                }
//                else if (fetchedValue.GetType() == typeof(Quaternion))
//                {
//                    size = (Vector4)fetchedValue;
//                }
//                else if (fetchedValue.GetType() == typeof(GameObject))
//                {
//                    GameObject fetchedGameObject = (GameObject)fetchedValue;
//                    size = fetchedGameObject.transform.localScale;
//                }
//                else if (fetchedValue.GetType() == typeof(Transform))
//                {
//                    Transform fetchedTransform = (Transform)fetchedValue;
//                    size = fetchedTransform.localScale;
//                }
//                else
//                {
//                    _wrongFetch = true;
//                }
//            }

//        }

//        if (activated)
//        {
//            DrawVolume(volumeType, color, transform.position + OffsetPosition, size + sizeOffset, defaultIcons[iconNameIndex]);


//            ADD TEXT
//        if (addText && !hideTextUnselected)
//            {
//                Handles.BeginGUI();


//                GUIStyle style = new GUIStyle();
//                style.fontSize = textSize;
//                style.alignment = TextAnchor.MiddleCenter;
//                style.normal.textColor = textColor;

//                var view = SceneView.currentDrawingSceneView;
//                Vector3 screenPos = Camera.current.WorldToScreenPoint(transform.position + OffsetPosition + textOffset);

//                Vector2 sizeG = GUI.skin.label.CalcSize(new GUIContent(areaText));
//                GUI.Label(new Rect(screenPos.x - (sizeG.x / 2), -screenPos.y + Screen.height - 30/*view.position.height*/, sizeG.x, sizeG.y), areaText, style);
//                GUI.Label(new Rect(screenPos.x - (sizeG.x / 2), -screenPos.y + Camera.current.pixelHeight/*view.position.height*/, sizeG.x, sizeG.y), areaText, style);
//                Handles.EndGUI();
//            }

//        }




//    }


//    public static void DrawVolume(VolumeType volumeType, Color color, Vector3 position, Vector3 size, string iconName = null)
//    {
//        switch (volumeType)
//        {
//            case VolumeType.Cube:
//                Gizmos.color = color;
//                Gizmos.DrawCube(position, size);

//                break;

//            case VolumeType.WireCube:
//                Gizmos.color = color;
//                Gizmos.DrawWireCube(position, size);
//                break;

//            case VolumeType.Sphere:
//                Gizmos.color = color;
//                Gizmos.DrawSphere(position, size.x);
//                break;

//            case VolumeType.WireSphere:
//                Gizmos.color = color;
//                Gizmos.DrawWireSphere(position, size.x);
//                break;

//            case VolumeType.Line:
//                Gizmos.color = color;
//                Gizmos.DrawLine(position, size);
//                break;

//            case VolumeType.Icon:
//                Gizmos.color = color;
//                Gizmos.DrawIcon(position, iconName, true, color);
//                break;

//        }

//    }



//    [MenuItem("GameObject/Create Other/Gizmo Volume")]
//    public static void NewGizmoVolume()
//    {
//        GameObject newObject = new GameObject();
//        newObject.name = "_GIZMO";
//        newObject.AddComponent<GizmoVolume>();
//    }

//}
