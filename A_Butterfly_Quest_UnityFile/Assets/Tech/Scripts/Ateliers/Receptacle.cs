using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Receptacle : MonoBehaviour
{
    public enum ButterflyNeededType { Normal, Illusion, Tempete }

    [Header("Receptacle Stats")]
    public ButterflyNeededType m_ButterflyNeededType;
    [Range(0,20)]
    public int ValueNeeded;
    [Range(0, 10)]
    public float DetectionRange;

    private TextMesh m_text;
    private Collider m_collider;

    [Header("Debug")]
    public bool Completed;
    private LayerMask PlayerMask;
    private int ValueGived;

    private void Start()
    {
        m_text = gameObject.transform.GetChild(0).gameObject.GetComponent<TextMesh>();
        m_collider = gameObject.GetComponent<Collider>();
        m_text.anchor = TextAnchor.MiddleCenter;
        PlayerMask = LayerMask.GetMask("Player");
        m_text.color = Color.red;
    }

    void Update()
    {
        m_text.transform.rotation = Quaternion.LookRotation(m_text.transform.position - Camera.main.transform.position);

        PlayerDetection();
        CheckTextLenght();
        CheckValue();
    }

    public void AddButterfly(int value = 1)
    {
        ValueGived += value;
    }

    private void CheckValue()
    {
        if(ValueGived >= ValueNeeded)
        {
            m_collider.enabled = false;
            Completed = true;
            m_text.color = Color.green;
        }
    }

    private void PlayerDetection()
    {
        if (Physics.CheckSphere(transform.position, DetectionRange, PlayerMask))
        {
            m_text.gameObject.SetActive(true); 
        }
        else
        {
            m_text.gameObject.SetActive(false);
        }
    }

    private void CheckTextLenght()
    {
        string ValueGivedText;
        string ValueNeededText;

        if (ValueGived < 10)
        {
            ValueGivedText = "0" + ValueGived;
        }
        else
        {
            ValueGivedText = ValueGived.ToString();
        }

        if (ValueGived < 10)
        {
            ValueNeededText = "0" + ValueNeeded;
        }
        else
        {
            ValueNeededText = ValueNeeded.ToString();
        }

        m_text.text = ValueGivedText + "/" + ValueNeededText;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, DetectionRange);
    }
}
