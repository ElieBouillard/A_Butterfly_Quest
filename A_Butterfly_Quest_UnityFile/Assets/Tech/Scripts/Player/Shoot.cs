using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Shoot : MonoBehaviour
{
    public GameObject PlayerMesh;
    private CharacterMeshRotate CharacterMeshScpt;

    public Animator CamAnimator;

    public GameObject CrossAirObj;

    public CinemachineFreeLook freeLookCam;
    public CameraAiming CameraAimingScpt;    

    public bool Aiming = false;

    private void Start()
    {
        CharacterMeshScpt = PlayerMesh.GetComponent<CharacterMeshRotate>();
    }  

    private void Update()
    {        
        if (Input.GetAxis("Aim") > 0)
        {

            Aiming = true;
            CamAnimator.SetBool("AimCamera", true);
            ShowOnCrossair();            
        }
        else if (Input.GetAxis("Aim") <= 0)
        {
            ResetFreeLookCamPos();
            ResetAimCamPos();
            Aiming = false;
            CamAnimator.SetBool("AimCamera", false);
            ShowOffCrossair();
        }
    }

    public void ResetAimCamPos()
    {
        if (CameraAimingScpt.yAxis.Value != 0)
        {
            CameraAimingScpt.yAxis.Value = 0;
        }
        //CameraAimingScpt.xAxis.Value = 0;
    }

    public void ResetFreeLookCamPos()
    {
        freeLookCam.m_XAxis.Value = CameraAimingScpt.xAxis.Value;
    }

    public void ShowOnCrossair()
    {
        CrossAirObj.SetActive(true);
    }

    public void ShowOffCrossair()
    {
        CrossAirObj.SetActive(false);
    }
}
