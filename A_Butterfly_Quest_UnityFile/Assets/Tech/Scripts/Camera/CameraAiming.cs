using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAiming : MonoBehaviour
{
    public static CameraAiming instance;

    public Transform cameraLookAT;
    public Cinemachine.AxisState xAxis;
    public Cinemachine.AxisState yAxis;

    private void Awake()
    {
        instance = this;
    }

    private void FixedUpdate()
    {
        //Orientation de la camera "Aim" avec la souris
        xAxis.Update(Time.fixedDeltaTime);
        yAxis.Update(Time.fixedDeltaTime);
        Vector3 cameralookat = new Vector3(yAxis.Value, xAxis.Value, 0f);
        cameraLookAT.eulerAngles = cameralookat;
    }
}
