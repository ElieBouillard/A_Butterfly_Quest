using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererUpdater : MonoBehaviour
{
    float timer;
    public float TimeToDissolve = 1;
    float erosionAmount;

    public float TimeToProgress = .5f;
    float progressAmount;

    List<LineRenderer> m_Renderers = new List<LineRenderer>();
    List<Material> m_Mats = new List<Material>();
    private void Start()
    {
        m_Renderers.Add(GetComponent<LineRenderer>());
        for (int i = 0; i < transform.childCount; i++)
        {
            m_Renderers.Add(transform.GetChild(i).GetComponent<LineRenderer>());
        }
        for (int i = 0; i < m_Renderers.Count; i++)
        {
            m_Mats.Add(m_Renderers[i].sharedMaterial);
        }
    }

    void Update()
    {
        if(timer < TimeToDissolve)
        {
            erosionAmount = timer / TimeToDissolve;
            progressAmount = timer / TimeToProgress;
            UpdateRenderers();
            timer += Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    void UpdateRenderers()
    {
        for (int i = 0; i < m_Renderers.Count; i++)
        {
            //m_Renderers.
            m_Renderers[i].material.SetFloat("_Erosion", erosionAmount);
            if(progressAmount <= 1)
            {
                m_Renderers[i].material.SetFloat("_Progression", progressAmount);
            }
        }
    }
}
