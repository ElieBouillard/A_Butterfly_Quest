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
    public GameObject ButterflyLauncher;
    private ButterflyEntity currButterflyEntity;
    private LayerMask ButterflyMask;

    [Header("References")]
    public Animator CamAnimator;
    public CinemachineFreeLook freeLookCam;
    private CameraAiming CameraAimingScpt;
    private GameObject PlayerMesh;

    [HideInInspector]
    public bool Aiming = false;

    public bool canShoot = true;

    private void Awake()
    {
        CameraAimingScpt = GetComponent<CameraAiming>();
        PlayerMesh = gameObject.transform.GetChild(0).gameObject;
        ButterflyMask =~ LayerMask.GetMask("Butterfly") + LayerMask.GetMask("Player");
        Instance = this;
    }

    private void Update()
    {        
        //Aim
        if (Input.GetAxis("Aim") > 0)
        {
            Aiming = true;
            CamAnimator.SetBool("AimCamera", true);

            AnimationManager.m_instance.playerFocused = true; //Anim
        }
        //No Aim
        else if (Input.GetAxis("Aim") <= 0)
        {
            ResetFreeLookCamPos();
            ResetAimCamPos();
            Aiming = false;
            CamAnimator.SetBool("AimCamera", false);

            AnimationManager.m_instance.playerFocused = false; //Anim
        }

        //Shoot Papillons normaux
        if(Aiming)
            ShootInputSystem();

        //Reload
        if (Input.GetButtonDown("Reload"))
        {
            ButterflyInventory.Instance.StartReload();
        }      
    }

    private void ShootButterfly()
    {
        //Recuperation du premier papillon normal dans l'inventaire
        currButterflyEntity = ButterflyInventory.Instance.ButterflyInInventory[ButterflyTypeSelection.Instance.SelectionTypeValue][0];
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
                butterflyBulletScpt.GetDirection1();
            }
            //Sinon le papillon part du perso pour aller tout droit
            else
            {
                butterflyBulletScpt.GetDirection2();
                butterflyBulletScpt.onHit = false;
            }
        }        
    }

    private void ShootInputSystem()
    {
        if (ButterflyInventory.Instance.ButterflyInInventory[ButterflyTypeSelection.Instance.SelectionTypeValue].Count > 0)
        {
            if (Input.GetAxisRaw("Fire1") == 1)
            {
                if (canShoot == true)
                {
                    ShootButterfly();

                    AnimationManager.m_instance.shootTrigger = true; //Anim   

                    canShoot = false;
                }
            }
        }
        if (Input.GetAxisRaw("Fire1") == 0)
        {
            canShoot = true;
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
