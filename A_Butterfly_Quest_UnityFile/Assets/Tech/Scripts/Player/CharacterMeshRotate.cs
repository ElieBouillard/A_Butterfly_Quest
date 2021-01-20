using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMeshRotate : MonoBehaviour
{
    public GameObject PlayerOb;
    private Character3D PlayerControllerScpt;
    private Shoot ShootScpt;

    private void Start()
    {
        PlayerControllerScpt = PlayerOb.GetComponent<Character3D>();
        ShootScpt = PlayerOb.GetComponent<Shoot>();
    }

    private void Update()
    {
        if (ShootScpt.Aiming == false)
        {
            Vector3 dir = (PlayerControllerScpt.directionForward * -PlayerControllerScpt.horizontalInput + PlayerControllerScpt.directionRight * PlayerControllerScpt.verticalInput);

            if (dir != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(dir);
            }
        }   
    }

    public void ResetMeshForward()
    {
        transform.rotation = Quaternion.LookRotation(PlayerControllerScpt.directionRight);
    }
}
