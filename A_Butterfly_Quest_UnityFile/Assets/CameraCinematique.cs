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
    //public GameObject target;
    public GameObject dollyTrack;

    public float blackScreenDuration = 1f;

    bool isAnimationFinished = false;
    private bool canUnfreeze;
    private float blackScreenClock;
    private GameObject blackScreenObj;
    private bool blackScreenCheck;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        //target.SetActive(true);
        blackScreenObj = UIManager.instance.BlackScreenDeath.gameObject;

        Shoot.Instance.forceNoAim = true;
        Character3D.m_instance.ForceFreeze = true;
        canUnfreeze = true;

        if (m_AtelierType != AtelierType.Start)
        {
            dollyTrack.gameObject.GetComponent<CinemachineSmoothPath>().m_Waypoints[0].position = Camera.main.transform.position - dollyTrack.transform.position;
        }


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
        if (m_AtelierType == AtelierType.Start)
        {
            animator.SetTrigger("Start");
        }
    }
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0))
        {
            isAnimationFinished = true;
            //target.SetActive(false);
            if (canUnfreeze)
            {
                blackScreenObj.GetComponent<Animator>().speed = 3f;
                blackScreenObj.GetComponent<Animator>().SetBool("Opace", true);
                blackScreenCheck = true;
                blackScreenClock = blackScreenDuration;
                canUnfreeze = false;
            }
        }

        if (blackScreenCheck)
        {
            if (blackScreenClock > 0)
            {
                blackScreenClock -= Time.deltaTime;
            }
            else
            {
                blackScreenObj.GetComponent<Animator>().speed = 1f;
                blackScreenObj.GetComponent<Animator>().SetBool("Opace", false);
                Shoot.Instance.forceNoAim = false;
                Character3D.m_instance.ForceFreeze = false;
                blackScreenCheck = false;
            }
        }        
    }  
}
