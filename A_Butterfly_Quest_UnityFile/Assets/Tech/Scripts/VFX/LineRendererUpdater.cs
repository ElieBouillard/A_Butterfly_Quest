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


    float offsetX = 1;
    public Vector2 soulerCoasterRange = new Vector2(-1, 1);

    LineRenderer m_rend;
    List<LineRenderer> m_Renderers = new List<LineRenderer>();
    List<Material> m_Mats = new List<Material>();
    private void Start()
    {
        m_rend = GetComponent<LineRenderer>();
        m_Renderers.Add(m_rend);
        for (int i = 0; i < transform.childCount; i++)
        {
            LineRenderer childrend = transform.GetChild(i).GetComponent<LineRenderer>();

            childrend.SetPosition(0, m_rend.GetPosition(0));
            childrend.SetPosition(1, m_rend.GetPosition(1));

            LineRendererUpdater m_script;
            if(transform.GetChild(i).TryGetComponent<LineRendererUpdater>(out m_script) == false)
            {
                m_Renderers.Add(childrend);
            }

            
        }
        for (int i = 0; i < m_Renderers.Count; i++)
        {
            m_Mats.Add(m_Renderers[i].sharedMaterial);
        }
        UpdateRenderers(); //reset values
    }

    void Update()
    {
        if(timer < TimeToDissolve)
        {
            erosionAmount = timer / TimeToDissolve;
            progressAmount = timer / TimeToProgress;
            offsetX = Mathf.Lerp(soulerCoasterRange.x, soulerCoasterRange.y, progressAmount);
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
            Material accessedMat = m_Renderers[i].material;
            accessedMat.SetFloat("_Erosion", erosionAmount * accessedMat.GetFloat("_LifetimeMultiplicator"));
            if(progressAmount <= 1)
            {
                accessedMat.SetFloat("_Progression", progressAmount * accessedMat.GetFloat("_LifetimeMultiplicator")); //LINES
             
                if(accessedMat.GetFloat("_IsSoulerCoaster") == 1) //SOULER COASTERS
                {
                    accessedMat.SetFloat("_OffsetX", offsetX * accessedMat.mainTextureScale.x);

                }
            }
        }
    }
}
