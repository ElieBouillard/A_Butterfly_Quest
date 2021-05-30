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
    private Vector3 HitPos;
    private RaycastHit ShootInfo;
    public LayerMask IgnoreMask;
    public LayerMask ReceptaclesMask;
    public float lookSpeed;

    [Header("References")]
    public Animator CamAnimator;
    public CinemachineFreeLook freeLookCam;
    public GameObject CamTarget;
    private CameraAiming CameraAimingScpt;
    private GameObject PlayerMesh;

    [HideInInspector]
    public bool Aiming = false;

    [Header("Debug")]
    public bool canShoot = true;

    private bool canResetFreeLookCam;
    private bool canResetAimCam;
    private float AimXStart;
    private float AimYStart;

    [HideInInspector]
    public bool isAimAssist;
    [HideInInspector]
    public RaycastHit AimAssitHit;
    [HideInInspector]
    public Transform AimTargetTransform;


    private void Awake()
    {
        CameraAimingScpt = GetComponent<CameraAiming>();
        PlayerMesh = gameObject.transform.GetChild(0).gameObject;
        Instance = this;
    }

    private void Start()
    {
        AimXStart = CameraAimingScpt.xAxis.m_MaxSpeed;
        AimYStart = CameraAimingScpt.yAxis.m_MaxSpeed;
    }

    private void Update()
    {
        //Aim
        if (Input.GetAxis("Aim") > 0)
        {
            Aiming = true;
            CamAnimator.SetBool("AimCamera", true);

            AnimationManager.m_instance.playerFocused = true; //Anim
            canResetFreeLookCam = true;
        }
        //No Aim
        else if (Input.GetAxis("Aim") <= 0)
        {
            if (canResetFreeLookCam)
            {
                ResetFreeLookCamPos();
                canResetFreeLookCam = false;
            }
            ResetAimCamPos();
            Aiming = false;
            isAimAssist = false;
            CamAnimator.SetBool("AimCamera", false);
            AnimationManager.m_instance.playerFocused = false; //Anim
        }

        //Shoot Papillons normaux
        if (Aiming)
        {
            ShootInputSystem();
            AimAssitReceptacle();
        }

        //Reload
        if (Input.GetButtonDown("Reload"))
        {
            ButterflyInventory.Instance.StartReload();
        }
    }

    private void ShootButterfly(int ButterflyType)
    {
        ButterflyBehaviourV2 currButterlfy;
        currButterlfy = ButterflyInventory.Instance.GetFirstButterfly(ButterflyType);
        ButterflyInventory.Instance.ShootedButterfly(currButterlfy);

        //Si touche un mesh alors prend les coordonees en direction
        if (Physics.Raycast(Camera.main.transform.position + Camera.main.transform.forward * 5f, Camera.main.transform.forward, out ShootInfo, Range, IgnoreMask))
        {
            Debug.DrawRay(Camera.main.transform.position + Camera.main.transform.forward * 5f, Camera.main.transform.forward * Range, Color.red, 5f);
            HitPos = ShootInfo.point;
            Debug.DrawRay(HitPos, Vector3.up * 5f, Color.red, 5f);

            if (ShootInfo.transform.gameObject.GetComponent<HealthSystem>())
            {
                HealthSystem currHealthSystem = ShootInfo.transform.gameObject.GetComponent<HealthSystem>();
                currHealthSystem.TakeDamage(1);
                currHealthSystem = null;
            }

            if (ShootInfo.transform.gameObject.GetComponent<Receptacle>())
            {
                Receptacle currReceptacle = ShootInfo.transform.gameObject.GetComponent<Receptacle>();
                if(ButterflyTypeSelection.Instance.SelectionTypeValue == (int)currReceptacle.m_ButterflyNeededType)
                {
                    currReceptacle.AddButterfly();
                }
            }
            else
            {
                ButterflyInventory.Instance.AddToReloadList(currButterlfy);
            }
        }
        else
        {
            Ray shootRay = new Ray(Camera.main.transform.position + Camera.main.transform.forward * 5f, Camera.main.transform.forward * Range);
            Debug.DrawRay(Camera.main.transform.position + Camera.main.transform.forward * 5f, Camera.main.transform.forward * Range, Color.blue, 5f);
            HitPos = shootRay.GetPoint(Range);
            Debug.DrawRay(HitPos, Vector3.up * 5f, Color.blue, 5f);
            ButterflyInventory.Instance.AddToReloadList(currButterlfy);
        }


    }

    private void ShootInputSystem()
    {
        if (ButterflyInventory.Instance.ButterflyInInventory[ButterflyTypeSelection.Instance.SelectionTypeValue].Count > 0)
        {
            if (Input.GetAxisRaw("Fire1") == 1 && canShoot)
            {
                ShootButterfly(ButterflyTypeSelection.Instance.SelectionTypeValue);
                //AnimationManager.m_instance.shootTrigger = true; //Anim   
                canShoot = false;
            }
        }
        if (Input.GetAxisRaw("Fire1") == 0)
        {
            canShoot = true;
        }
    }

    private void AimAssitReceptacle()
    {
        if (Physics.Raycast(Camera.main.transform.position + Camera.main.transform.forward * 5f, Camera.main.transform.forward, out AimAssitHit, Range, ReceptaclesMask))
        {
            CameraAimingScpt.xAxis.m_MaxSpeed = AimXStart *0.25f;
            CameraAimingScpt.yAxis.m_MaxSpeed = AimYStart * 0.25f;
            isAimAssist = true;
        }
        else
        {
            CameraAimingScpt.xAxis.m_MaxSpeed = AimXStart;
            CameraAimingScpt.yAxis.m_MaxSpeed = AimYStart;
            isAimAssist = false;
        }
    }

    //Reset la camera "Aim" au centre de l'écran dès que Aim = false
    public void ResetAimCamPos()
    {
        if (CameraAimingScpt.yAxis.Value != 0)
        {
            CameraAimingScpt.yAxis.Value = 0;
        }
        CameraAimingScpt.xAxis.Value = freeLookCam.m_XAxis.Value;
    }

    //Reset la camera "FreeLook" sur l'orientation de la camera "Aim" dès que Aim = false
    public void ResetFreeLookCamPos()
    {
        freeLookCam.m_XAxis.Value = CameraAimingScpt.xAxis.Value;
    }

    public void ResetFreeLookBehindPlayer()
    {
        freeLookCam.m_XAxis.Value = PlayerMesh.transform.rotation.eulerAngles.y;
        freeLookCam.m_YAxis.Value = 0.4f;
    }
}
