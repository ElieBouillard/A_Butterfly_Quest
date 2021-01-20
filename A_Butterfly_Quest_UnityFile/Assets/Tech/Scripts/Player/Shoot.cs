using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Shoot : MonoBehaviour
{
    public Animator CameraAnimator;
    public bool Aiming = false;
    public CinemachineFreeLook freeLookCam;
    public GameObject CamTarget;
    public GameObject PlayerMesh;
    private CharacterMeshRotate CharacterMeshScpt;

    private void Start()
    {
        CharacterMeshScpt = PlayerMesh.GetComponent<CharacterMeshRotate>();
    }  

    private void Update()
    {        
        if (Input.GetAxis("Aim") > 0)
        {
            CharacterMeshScpt.ResetMeshForward();
            Aiming = true;
            CameraAnimator.SetBool("AimCamera", true);
        }
        else if (Input.GetAxis("Aim") <= 0)
        {
            StartCoroutine(resetMesh());

        }
    }

    IEnumerator resetMesh()
    {
        CameraAnimator.SetBool("AimCamera", false);
        yield return new WaitForSeconds(0.8f);
        Aiming = false;
    }
}
