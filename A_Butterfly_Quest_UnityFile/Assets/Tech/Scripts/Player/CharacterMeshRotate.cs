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
        //No Aim -> le mesh rotate en fonction de la direction du déplacement
        if (ShootScpt.Aiming == false)
        {
            Vector3 dir = (PlayerControllerScpt.directionForward * PlayerControllerScpt.verticalInput + PlayerControllerScpt.directionRight * PlayerControllerScpt.horizontalInput);

            //Empeche le mesh de se reset sur le vecteur (0,0,0)
            if (dir != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(dir);
            }
        }
        //Aim -> le mesh est oriente en rapport a la camera "Aim" donc il reste dos a la camera
        else if (ShootScpt.Aiming)
        {
            ResetMeshForward();
        }        
    }

    //Reset orientation du mesh sur l'axe X de la MainCamera
    public void ResetMeshForward()
    {
        transform.rotation = Quaternion.LookRotation(PlayerControllerScpt.directionForward);
    }
}
