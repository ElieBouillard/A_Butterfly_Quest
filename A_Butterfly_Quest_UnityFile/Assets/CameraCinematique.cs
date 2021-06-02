using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCinematique : MonoBehaviour
{
    public static CameraCinematique instance;

    public Animator animator;
    public bool isAnimationFinished = false;
    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0))
        {
            isAnimationFinished = true;
        }
    }
    
  
}
