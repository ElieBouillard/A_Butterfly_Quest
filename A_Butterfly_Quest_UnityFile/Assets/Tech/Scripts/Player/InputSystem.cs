using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public Cinemachine.CinemachineFreeLook m_freelook;

    public CameraAiming m_cameraAiming;

    public static InputSystem instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        m_cameraAiming = CameraAiming.instance;

        //SetSensiSliddersHUD
        UIManager.instance.slideFreeLookXaxis.value = m_freelook.m_XAxis.m_MaxSpeed;
        UIManager.instance.slideFreeLookYaxis.value = m_freelook.m_YAxis.m_MaxSpeed;
        UIManager.instance.slideAimXaxis.value = m_cameraAiming.xAxis.m_MaxSpeed;
        UIManager.instance.slideAimYaxis.value = m_cameraAiming.yAxis.m_MaxSpeed;
    }

    private void Update()
    {
        //Affect FreeLook Sensi
        m_freelook.m_XAxis.m_MaxSpeed = UIManager.instance.GetFreeLookSensi().x;
        m_freelook.m_YAxis.m_MaxSpeed = UIManager.instance.GetFreeLookSensi().y;
        //Affect Aim Sensi
        m_cameraAiming.xAxis.m_MaxSpeed = UIManager.instance.GetAimSensi().x;
        m_cameraAiming.yAxis.m_MaxSpeed = UIManager.instance.GetAimSensi().y;
    }
}
