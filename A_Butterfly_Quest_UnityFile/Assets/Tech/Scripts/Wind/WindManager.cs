using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindManager : MonoBehaviour
{
    public static WindManager m_instance;
    private static Vector4 localWindDir = new Vector4(.5f, 0f,.5f,1f);
    public static Vector4 GlobalWindDirection 
    { 
        get 
        { 
            return localWindDir.normalized; 
        }
        set
        {
            localWindDir = value;
        }
        
    }

    private void Awake()
    {
        m_instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
