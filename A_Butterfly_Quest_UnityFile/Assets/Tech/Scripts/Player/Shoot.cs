using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class Shoot : MonoBehaviour
{
    [Header("Shooting variables")]
    [Range(1, 10)]
    public int Damage = 1;
    [Range(1, 100)]
    public float Range = 50;
    private RaycastHit ShootInfo;
    public GameObject PrefabButterly;
    private GameObject ButterflyLauncher;

    [Header("References")]
    public Animator CamAnimator;
    public GameObject CrosshairObj;
    public CinemachineFreeLook freeLookCam;
    private CameraAiming CameraAimingScpt;
    private GameObject PlayerMesh;

    [HideInInspector]
    public bool Aiming = false;

    private void Awake()
    {
        CameraAimingScpt = GetComponent<CameraAiming>();
        PlayerMesh = gameObject.transform.GetChild(0).gameObject;
        ButterflyLauncher = PlayerMesh.transform.GetChild(0).gameObject;
    }

    private void Update()
    {        
        //Aim
        if (Input.GetAxis("Aim") > 0)
        {
            Aiming = true;
            CamAnimator.SetBool("AimCamera", true);
            ShowCrosshair(true);            
        }
        //No Aim
        else if (Input.GetAxis("Aim") <= 0)
        {
            ShowCrosshair(false);
            ResetFreeLookCamPos();
            ResetAimCamPos();
            Aiming = false;
            CamAnimator.SetBool("AimCamera", false);
        }

        //Shoot
        if (Input.GetButtonDown("Fire1") && Aiming)
        {
            ShootButterfly();
        }
    }

    private void ShootButterfly()
    {
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out ShootInfo, Range))
        {
            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * Range, Color.yellow, 10f);
            GameObject currButterfly = ButterflyPooler.SharedInstance.GetPooledObject();
            if(currButterfly != null)
            {
                currButterfly.transform.position = ButterflyLauncher.transform.position;
                currButterfly.SetActive(true);
                ButterFlyBullet currButterFlyScpt = currButterfly.GetComponent<ButterFlyBullet>();
                currButterFlyScpt.target = ShootInfo.point;
            }

        }
    }

    //Reset la camera "Aim" derriere le joueur dès que Aim = false
    public void ResetAimCamPos()
    {
        if (CameraAimingScpt.yAxis.Value != 0)
        {
            CameraAimingScpt.yAxis.Value = 0;
        }
    }

    //Reset la camera "FreeLook" sur l'orientation de la camera "Aim" dès que Aim = false
    public void ResetFreeLookCamPos()
    {
        freeLookCam.m_XAxis.Value = CameraAimingScpt.xAxis.Value;
    }

    public void ShowCrosshair(bool value)
    {
        CrosshairObj.SetActive(value);
    }
}
