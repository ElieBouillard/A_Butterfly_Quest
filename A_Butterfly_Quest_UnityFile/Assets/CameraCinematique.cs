using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCinematique : MonoBehaviour
{
    public static CameraCinematique instance;

    public Animator animator;
    public bool isAnimationFinished = false;
    bool check;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        Character3D.m_instance.FreezeInput = true;
    }
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0))
        {
            isAnimationFinished = true;
            if (check)
            {
                Character3D.m_instance.FreezeInput = false;
                check = false;
            }

        }
    }
    
  
}
