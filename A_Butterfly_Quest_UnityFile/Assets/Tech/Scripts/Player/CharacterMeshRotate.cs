using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMeshRotate : MonoBehaviour
{
    public Character3D PlayerControllerScpt;
    public Camera m_cam;

    private void Update()
    {
        ////float RotationAngle = Mathf.Atan2(PlayerControllerScpt.horizontalInput, PlayerControllerScpt.verticalInput) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0f, RotationAngle + 90, 0f);

        Quaternion rotationTarget = new Quaternion(0f, m_cam.transform.rotation.y ,0f, 0f);
        transform.rotation = rotationTarget;
    }
}
