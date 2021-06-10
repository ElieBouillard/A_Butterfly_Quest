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
    

    [Header("VFX")]
    //[ColorUsage(true, true)]
    //public Color NormalGlowColor;
    //[ColorUsage(true, true)]
    //public Color IllusionGlowColor;
    //[ColorUsage(true, true)]
    //public Color TempeteGlowColor;
    //public Renderer Hole_Glow;
    //public Renderer LogoButterfly;

    public GameObject Butterfly_Normal;
    public GameObject Butterfly_Illusion;
    public GameObject Butterfly_Tempete;



    [Header("Camera")]
    public bool camAffilied;
    public GameObject cam;
    public bool haveConnectedReceptacle;
    public GameObject connectedReceptacle;



    private void Start()
    {
        m_collider = gameObject.GetComponent<Collider>();
        PlayerMask = LayerMask.GetMask("Player");

        CheckNumberOfBalls();
        UpdateVFXColors();
    }

    void Update()
    {
        CheckValue();
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
            camLaunch();          
        }
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


    void UpdateVFXColors()
    {
        switch (m_ButterflyNeededType)
        {
            //case ButterflyNeededType.Normal:
            //    Hole_Glow.material.SetColor("_MainColor", NormalGlowColor);
            //    LogoButterfly.material.SetTexture("_MainTexture",Butterfly_Normal);
            //    break;
            //case ButterflyNeededType.Illusion:
            //    Hole_Glow.material.SetColor("_MainColor", IllusionGlowColor);
            //    LogoButterfly.material.SetTexture("_MainTexture", Butterfly_Illusion);
            //    break;
            //case ButterflyNeededType.Tempete:
            //    Hole_Glow.material.SetColor("_MainColor", TempeteGlowColor);
            //    LogoButterfly.material.SetTexture("_MainTexture", Butterfly_Tempete);
            //    break;
            case ButterflyNeededType.Normal:
                Butterfly_Normal.SetActive(true);
                Butterfly_Illusion.SetActive(false);
                Butterfly_Tempete.SetActive(false);
                break;
            case ButterflyNeededType.Illusion:
                Butterfly_Normal.SetActive(false);
                Butterfly_Illusion.SetActive(true);
                Butterfly_Tempete.SetActive(false);
                break;
            case ButterflyNeededType.Tempete:
                Butterfly_Normal.SetActive(false);
                Butterfly_Illusion.SetActive(false);
                Butterfly_Tempete.SetActive(true);
                break;
        }
    }

    void camAnimation()
    {
        cam.SetActive(true);
    }
    void camLaunch()
    {
        if (camAffilied)
        {
            if (haveConnectedReceptacle && connectedReceptacle.GetComponent<Receptacle>().Completed == true)
            {
                camAnimation();

            }
            if (!haveConnectedReceptacle)
            {
                camAnimation();
            }
        }
    }
}
