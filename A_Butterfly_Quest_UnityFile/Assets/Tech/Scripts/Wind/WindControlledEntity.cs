using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindControlledEntity : MonoBehaviour
{
    private void Start()
    {
        InitiateWind();
    }

    [ContextMenu("Refresh Wind")]
    void InitiateWind()
    {
        if(TryGetComponent<Renderer>(out Renderer m_renderer))
        {
            if (m_renderer.sharedMaterial != null)
            {
                m_renderer.sharedMaterial.SetVector("_WindDirection", WindManager.GlobalWindDirection);
            }
           
        }
    }
}
