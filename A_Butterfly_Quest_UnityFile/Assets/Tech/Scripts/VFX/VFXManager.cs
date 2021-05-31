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
    public ParticleSystem ImpactVFX;
    bool isAiming;


    //Character
    public Renderer mesh_rend;
    float target_armGlow;
    float armGlow_TurnOffDelay = 1.5f;
    float armGlow_DelayTimer;
    float armGlow_timeToChange;

    private void Awake()
    {
        m_instance = this;
    }

    void Start()
    {
        
    }

  
    void Update()
    {
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
}
