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
    public ButterflyEntity currButterflyEntity;

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

        //Shoot Papillons normaux
        if (ButterflyInventory.Instance.ButterflyInInventory.Count > 0)
        {
            if (Input.GetButtonDown("Fire1") && Aiming)
            {
                ShootButterfly();
            }
        }

        //Reload
        if (Input.GetKeyDown("r"))
        {
            ButterflyInventory.Instance.Reload();
        }
    }

    private void ShootButterfly()
    {
        currButterflyEntity = ButterflyInventory.Instance.ButterflyInInventory[0];
        ButterflyInventory.Instance.ShootedButterfly();

        GameObject currButterfly = ButterflyPooler.SharedInstance.GetPooledObject();
        if(currButterfly != null)
        {
            currButterfly.transform.position = ButterflyLauncher.transform.position;
            currButterfly.SetActive(true);
            ButterflyBullet currButterFlyScpt = currButterfly.GetComponent<ButterflyBullet>();

            currButterFlyScpt.GetButterflyInfo(currButterflyEntity);

            //Si touche un mesh alors prend les coordonees en direction
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out ShootInfo, Range))
            {
                currButterFlyScpt.onHit = true;
                currButterFlyScpt.target = ShootInfo.point;
            }
            //Sinon le papillon part du perso pour aller tout droit
            else
            {
                currButterFlyScpt.onHit = false;
                currButterFlyScpt.rb.velocity = Camera.main.transform.forward * currButterFlyScpt.ButterflySpeed;
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
