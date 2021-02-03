using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class Shoot : MonoBehaviour
{
    public static Shoot Instance;

    [Header("Shooting variables")]
    [Range(1, 100)]
    public float Range = 50;
    private RaycastHit ShootInfo;
    public GameObject PrefabButterly;
    private GameObject ButterflyLauncher;
    private ButterflyEntity currButterflyEntity;
    private LayerMask ButterflyMask;

    [Header("References")]
    public Animator CamAnimator;
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
        ButterflyMask =~ LayerMask.GetMask("Butterfly");
        Instance = this;
    }

    private void Update()
    {        
        //Aim
        if (Input.GetAxis("Aim") > 0)
        {
            Aiming = true;
            CamAnimator.SetBool("AimCamera", true);           
        }
        //No Aim
        else if (Input.GetAxis("Aim") <= 0)
        {
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
        if (Input.GetButtonDown("Reload"))
        {
            ButterflyInventory.Instance.StartReload();
        }
    }

    private void ShootButterfly()
    {
        //Recuperation du premier papillon normal dans l'inventaire
        currButterflyEntity = ButterflyInventory.Instance.ButterflyInInventory[0];
        ButterflyInventory.Instance.ShootedButterfly(currButterflyEntity);

        //Recuperation de l'objet pool disponible
        GameObject butterflyBullet = ButterflyPooler.SharedInstance.GetPooledObject();
        if(butterflyBullet != null)
        {
            //Reset position de l'objet pool et activation
            butterflyBullet.transform.position = ButterflyLauncher.transform.position;
            butterflyBullet.SetActive(true);
            ButterflyBullet butterflyBulletScpt = butterflyBullet.GetComponent<ButterflyBullet>();

            butterflyBulletScpt.GetButterflyInfo(currButterflyEntity);

            //Si touche un mesh alors prend les coordonees en direction
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out ShootInfo, Range, ButterflyMask))
            {
                butterflyBulletScpt.onHit = true;
                butterflyBulletScpt.target = ShootInfo.point;
            }
            //Sinon le papillon part du perso pour aller tout droit
            else
            {
                butterflyBulletScpt.onHit = false;
                butterflyBulletScpt.rb.velocity = Camera.main.transform.forward * butterflyBulletScpt.ButterflySpeed;
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
}
