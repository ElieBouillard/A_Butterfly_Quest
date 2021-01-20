using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMeshRotate : MonoBehaviour
{
    public GameObject PlayerOb;
    private Character3D PlayerControllerScpt;

    private void Start()
    {
        PlayerControllerScpt = PlayerOb.GetComponent<Character3D>();
    }

    private void Update()
    {
        Vector3 dir = (PlayerControllerScpt.directionForward * -PlayerControllerScpt.horizontalInput + PlayerControllerScpt.directionRight * PlayerControllerScpt.verticalInput);

        if(dir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }
}
