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
    [Range(0, 20)]
    public float DetectionRange;

    private TextMesh m_text;
    private Collider m_collider;

    [Header("Debug")]
    public bool Completed;
    private LayerMask PlayerMask;
    private int ValueGived;

    [Header("Feedback")]
    public List<GameObject> Balls = new List<GameObject>();
    public Material Material;
    List<GameObject> BallsToColor = new List<GameObject>();
    int index = 0;


    private void Start()
    {
        m_text = gameObject.transform.GetChild(0).gameObject.GetComponent<TextMesh>();
        m_collider = gameObject.GetComponent<Collider>();
        m_text.anchor = TextAnchor.MiddleCenter;
        PlayerMask = LayerMask.GetMask("Player");
        m_text.color = Color.red;

        CheckNumberOfBalls();
    }

    void Update()
    {
        m_text.transform.rotation = Quaternion.LookRotation(m_text.transform.position - Camera.main.transform.position);

        CheckValue();
        //PlayerDetection();
        //CheckTextLenght();
    }

    public void AddButterfly(int value = 1)
    {
        ValueGived += value;
        ColorBall();
        index++;
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

    private void CheckNumberOfBalls()
    {
        for (int i = 0; i < Balls.Count; i++)
        {
            if (ValueNeeded == 5)
            {
                Balls[i].SetActive(true);
                BallsToColor.Add(Balls[i]);
            }
            if (ValueNeeded == 1)
            {
                Balls[2].SetActive(true);
                BallsToColor.Add(Balls[2]);
            }
            if (ValueNeeded == 3)
            {
                Balls[1].SetActive(true);
                Balls[2].SetActive(true);
                Balls[3].SetActive(true);
                BallsToColor.Add(Balls[1]);
                BallsToColor.Add(Balls[2]);
                BallsToColor.Add(Balls[3]);
            }
            if (ValueNeeded == 2)
            {
                Balls[1].SetActive(true);
                Balls[3].SetActive(true);
                BallsToColor.Add(Balls[1]);
                BallsToColor.Add(Balls[3]);
            }
        }                
    }

    private void ColorBall()
    {   
        BallsToColor[index].GetComponent<MeshRenderer>().material = Material;
    }
    

}
