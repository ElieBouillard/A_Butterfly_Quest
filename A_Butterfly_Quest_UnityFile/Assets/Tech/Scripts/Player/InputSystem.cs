using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public Cinemachine.CinemachineFreeLook m_freelook;

    private CameraAiming m_cameraAiming;

    public static InputSystem instance;


    [HideInInspector]
    public bool OnPauseMenu;

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

        //InputPause
        if (Input.GetKeyDown(KeyCode.Escape) || (Input.GetKeyDown(KeyCode.Joystick1Button7)))
        {
            if (!OnPauseMenu)
            {
                OnPauseMenu = true;
                Time.timeScale = 0;
            }
            else
            {
                OnPauseMenu = false;
                Time.timeScale = 1;
            }
        }
    }
}
