using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraAiming : MonoBehaviour
{
    public static CameraAiming instance;

    public Transform cameraLookAT;
    public Cinemachine.AxisState xAxis;
    public Cinemachine.AxisState yAxis;

    public GameObject AimCamera;

    private void Awake()
    {
        instance = this;
    }

    private void FixedUpdate()
    {
        //Orientation de la camera "Aim" avec la souris
        xAxis.Update(Time.fixedDeltaTime);
        yAxis.Update(Time.fixedDeltaTime);

        if (!Shoot.Instance.isAimAssist)
        {
            Vector3 cameralookat = new Vector3(yAxis.Value, xAxis.Value, 0f);
            cameraLookAT.eulerAngles = cameralookat;
        }
        else
        {
            Vector3 dir = (Shoot.Instance.AimTargetTransform.localPosition - AimCamera.transform.position).normalized;
            cameraLookAT.transform.forward = dir;
            Debug.DrawRay(cameraLookAT.transform.position, cameraLookAT.transform.forward * 10f, Color.blue, 1f);
            xAxis.Value = cameraLookAT.eulerAngles.y;
            yAxis.Value = cameraLookAT.eulerAngles.x;
        }
    }
}
