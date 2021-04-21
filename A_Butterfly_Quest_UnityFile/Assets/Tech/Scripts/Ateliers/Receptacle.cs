using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Receptacle : MonoBehaviour
{

    [Header("Receptacle Stats")]
    [Range(0,20)]
    public int ValueNeeded;
    [Range(0, 10)]
    public float DetectionRange;
    public enum ButterflyNeededType { Normal, Illusion, Tempete }
    public ButterflyNeededType m_ButterflyNeededType;
    [HideInInspector]
    public int ValueGived;

    [Header("References")]
    public GameObject DoorToOpen;

    private GameObject player;
    private TextMesh m_text;
    private Collider m_collider;

    private bool Completed;
    private bool UnlockDoor;
    private LayerMask PlayerMask;

    private void Start()
    {
        m_text = gameObject.transform.GetChild(0).gameObject.GetComponent<TextMesh>();
        player = Character3D.m_instance.gameObject;
        m_collider = gameObject.GetComponent<Collider>();
        m_text.anchor = TextAnchor.MiddleCenter;
        PlayerMask = LayerMask.GetMask("Player");
        m_text.color = Color.red;
    }

    void Update()
    {
        m_text.transform.rotation = Quaternion.LookRotation(m_text.transform.position - Camera.main.transform.position);

        if (Completed)
        {
            m_text.color = Color.green;
            m_collider.enabled = false;

            if (!UnlockDoor)
            {
                ReceptacleValidated();
                UnlockDoor = true;
            }
        }

        PlayerDetection();
        CheckTextLenght();
        CheckValue();
    }

    private void ReceptacleValidated()
    {
        if(DoorToOpen != null)
        {
            DoorToOpen.SetActive(false);
        }
    }

    private void CheckValue()
    {
        if(ValueGived >= ValueNeeded)
        {
            Completed = true;
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
