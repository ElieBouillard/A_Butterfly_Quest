using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCinematique : MonoBehaviour
{
    public static CameraCinematique instance;

    public enum AtelierType
    {
       Start,
       Tempête,
       Illusion,
       Mix
    }
    public AtelierType m_AtelierType;

    public Animator animator;
    bool isAnimationFinished = false;
    public GameObject target;

    private bool canUnfreeze;
    private void Awake()
    {
        instance = this;
        target.SetActive(true);

    }
    private void Start()
    {
        Character3D.m_instance.ForceFreeze = true;
        canUnfreeze = true;

        if (m_AtelierType == AtelierType.Tempête)
        {
            animator.SetTrigger("Tempete");
        }
        if (m_AtelierType == AtelierType.Illusion)
        {
            animator.SetTrigger("Illu");
        }
        if (m_AtelierType == AtelierType.Mix)
        {
            animator.SetTrigger("Mix");
        }
    }
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0))
        {
            isAnimationFinished = true;
            target.SetActive(false);
            if (canUnfreeze)
            {
                Character3D.m_instance.ForceFreeze = false;
                canUnfreeze = false;
            }

        }
    }  
}
