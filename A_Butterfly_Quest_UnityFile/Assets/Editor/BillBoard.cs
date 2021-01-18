using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BillBoard : MonoBehaviour
{
    public Transform targetToFace;
    public enum WorldUp { X, Y, Z};
    public WorldUp rotatingAxis;
    public bool Invert;
    [HideInInspector]
    public WorldUp lastFacedAxis;
    [HideInInspector]
    public Vector3 worldupAxis;

    public bool updateInEditor = true;
    public bool followLiveCamera = true;

    public Quaternion originalRotation;

    private void Awake()
    {
        originalRotation = transform.rotation;
    }

    void Start()
    {
        UpdateAxis();        
    }


    void Update()
    {
        //Follow Current camera
        if (followLiveCamera)
        {
            if(Camera.current != null)
            {
                if (targetToFace != Camera.current.transform)
                {
                    targetToFace = Camera.current.transform;
                }
            }
                    
        }


        if(targetToFace != null)
        {
            if(lastFacedAxis != rotatingAxis)
            {
                UpdateAxis();
                
            }
            transform.LookAt(targetToFace, ((Invert ? 0 : 1) * worldupAxis) + ((Invert ? 1 : 0) * -worldupAxis));
        }
        
    }

    public void UpdateAxis()
    {
        switch (rotatingAxis)
        {
            case WorldUp.X:
                worldupAxis = new Vector3(1, 0, 0);
                break;
            case WorldUp.Y:
                worldupAxis = new Vector3(0, 1, 0);
                break;
            case WorldUp.Z:
                worldupAxis = new Vector3(0, 0, 1);
                break;

        }
        lastFacedAxis = rotatingAxis;
    }
}



[ExecuteInEditMode]
[CustomEditor(typeof(BillBoard))]
class BillBoardEditor : Editor
{
    public override void OnInspectorGUI()
    {
        BillBoard m_target = (BillBoard)target;
        EditorGUILayout.LabelField("BillBoard System", "Automatically orientates the object to face the Camera");
        DrawDefaultInspector();


        if (GUILayout.Button("Find Main Camera"))
        {
            if(Camera.main != null)
            {
                m_target.targetToFace = Camera.main.transform;
            }
           
        }


        if (GUILayout.Button("Find Active Camera"))
        {
            if (Camera.current != null)
            {
                m_target.targetToFace = Camera.current.transform;
            }
            else
            {
                EditorGUILayout.HelpBox("Could not find active camera", MessageType.Info);
            }

        }

        if (GUILayout.Button("Reset Rotation"))
        {
            m_target.transform.rotation = Quaternion.identity;
        }



        EditorUtility.SetDirty(m_target);
    }

    private void OnSceneGUI()
    {
        BillBoard m_target = (BillBoard)target;
        if (m_target.updateInEditor)
        {
            if (m_target.targetToFace != null)
            {
                m_target.transform.LookAt(m_target.targetToFace, ((m_target.Invert ? 0 : 1) * m_target.worldupAxis) + ((m_target.Invert ? 1 : 0) * -m_target.worldupAxis));
                if (m_target.lastFacedAxis != m_target.rotatingAxis)
                {
                    m_target.UpdateAxis();
                }
            }
      
            //Follow Current camera
            if (m_target.followLiveCamera)
            {
                if (Camera.current != null)
                {
                    if (m_target.targetToFace != Camera.current.transform)
                    {
                        m_target.targetToFace = Camera.current.transform;
                    }
                }
                   
            }
       
        }
    }

    private void OnEnable()
    {
        BillBoard m_target = (BillBoard)target;
        if(m_target.targetToFace == null)
        {
            m_target.targetToFace = Camera.main.transform;
        }

        m_target.UpdateAxis();
    }
}

