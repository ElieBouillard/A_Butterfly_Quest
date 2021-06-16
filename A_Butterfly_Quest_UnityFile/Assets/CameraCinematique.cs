using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

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

    public GameObject dollyTrack;
    public GameObject player;

    private void Awake()
    {
        instance = this;
        target.SetActive(true);
    }
    private void Start()
    {
        Shoot.Instance.forceNoAim = true;
        dollyTrack.gameObject.GetComponent<CinemachineSmoothPath>().m_Waypoints[0].position = Camera.main.transform.position - dollyTrack.transform.position;

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
                Character3D.m_instance.FreezePosPlayer(2f, true, true);
                canUnfreeze = false;
                Shoot.Instance.forceNoAim = false;
            }

        }
    }  
}
