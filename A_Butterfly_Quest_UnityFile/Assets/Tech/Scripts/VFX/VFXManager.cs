using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{

    public static VFXManager m_instance;

    //VFX

    //SHOOT
    public ParticleSystem idleAimEnergyVFX;
    public ParticleSystem AimVFX;
    public ParticleSystem ShootVFX;
    public GameObject ShootLine;
    public ParticleSystem ImpactVFX_missed;
    public ParticleSystem ImpactVFX_hit;

    bool isAiming;


    //Character
    public Renderer mesh_rend;
    public Transform shoot_startPoint;

    //Butterflies
    public GameObject SuccessfulCatch_FX;
    public GameObject SuccessfulCatch_Illusion_FX;
    public GameObject SuccessfulCatch_Tempete_FX;

    //ArmGlow
    float target_armGlow;
    float armGlow_TurnOffDelay = 1.5f;
    float armGlow_DelayTimer;
    float armGlow_timeToChange;

    //Net
    [SerializeField]
    Animator net_Animator;
    bool waitingToShowNet;
    float target_netVisibility;
    float net_TurnOffDelay = 8f;
    float net_DelayTimer;
    float net_timeToChange;

    private void Awake()
    {
        m_instance = this;
    }

    void Start()
    {
        
    }

  
    void Update()
    {

        //Arm Glow
        if(armGlow_timeToChange >= -0.1f)
        {
            if(target_armGlow == 0 && armGlow_DelayTimer < armGlow_TurnOffDelay)
            {
                armGlow_DelayTimer += Time.deltaTime;
            }
            else
            {
                if (target_armGlow > .9f)
                {
                    mesh_rend.sharedMaterial.SetFloat("_ArmGlow", Mathf.Lerp(0, 1, Mathf.Clamp01(1 - armGlow_timeToChange)));
                }
                else
                {
                    mesh_rend.sharedMaterial.SetFloat("_ArmGlow", Mathf.Lerp(1, 0, Mathf.Clamp01(1 - armGlow_timeToChange)));
                }

                armGlow_timeToChange -= Time.deltaTime;
            }
           
        }

        //Net visibility
        if(net_DelayTimer > -.1f)
        {
            if (net_DelayTimer >= net_TurnOffDelay)
            {
                //net_Animator.SetTrigger("HideNet");
                net_Animator.SetBool("NetVisibility", false);
                net_DelayTimer = -1f;
            }
            else
            {
                net_DelayTimer += Time.deltaTime;
            }
        }

        if (waitingToShowNet)
        {
            //Debug.Log(AnimationManager.m_instance.m_anim.GetCurrentAnimatorStateInfo(1).fullPathHash/*.IsName("NetAttack")*/);

            if (!AnimationManager.m_instance.m_anim.GetCurrentAnimatorStateInfo(1).IsName("NetAttack"))
            {
                //net_Animator.SetTrigger("ShowNet");
                net_Animator.SetBool("NetVisibility", true);
                net_DelayTimer = 0;
                waitingToShowNet = false;
            }
        }

        if (AnimationManager.m_instance.m_anim.GetCurrentAnimatorStateInfo(1).IsName("NetAttack"))
        {
            waitingToShowNet = true;
        }


    }

    public void StartAiming()
    {
        if (!isAiming)
        {
            AimVFX.Play();
            idleAimEnergyVFX.Play();
            target_armGlow = 1;
            armGlow_timeToChange = .5f;
            isAiming = true;
        }
        
    }

    public void StopAiming()
    {
        if (isAiming)
        {
            idleAimEnergyVFX.Stop();
            target_armGlow = 0;
            armGlow_timeToChange = 1;
            armGlow_DelayTimer = 0;
            isAiming = false;
        }

    }

    public void ShowNet(bool visibility)
    {
        if (visibility)
        {
            //net_Animator.SetTrigger("ShowNet");
            net_Animator.SetBool("NetVisibility",false);
            //waitingToShowNet = true;
            //armGlow_DelayTimer = 0;
        }
        else
        {
            //net_Animator.SetTrigger("HideNet");
            net_Animator.SetBool("NetVisibility", false);


        }
    }

    public void CatchButterfly(GameObject hitButterfly)
    {
        switch (hitButterfly.GetComponent<ButterflyBehaviourV2>().m_ButterflyType)
        {
            case ButterflyBehaviourV2.ButterflyType.Normal:
                Instantiate(SuccessfulCatch_FX, hitButterfly.transform.position, Quaternion.identity);
                break;
            case ButterflyBehaviourV2.ButterflyType.Tempete:
                Instantiate(SuccessfulCatch_Tempete_FX, hitButterfly.transform.position, Quaternion.identity);
                break;
            case ButterflyBehaviourV2.ButterflyType.Illusion:
                Instantiate(SuccessfulCatch_Illusion_FX, hitButterfly.transform.position, Quaternion.identity);
                break;
        }
        
    }

    public void SpawnShootVFX(Vector3 endPos)
    {
        Vector3 startPos = shoot_startPoint.transform.position;
        
        
        Instantiate(ShootVFX, shoot_startPoint.transform.position, Quaternion.identity); //shoot

        Instantiate(ImpactVFX_missed, endPos, Quaternion.identity); //impact

        //line renderer

        GameObject shoot_Line = Instantiate(ShootLine, new Vector3(0,0,0), Quaternion.identity);
        LineRenderer LineRend = shoot_Line.GetComponent<LineRenderer>();
        LineRend.SetPosition(0, startPos);
        LineRend.SetPosition(1, endPos);
    }
}
