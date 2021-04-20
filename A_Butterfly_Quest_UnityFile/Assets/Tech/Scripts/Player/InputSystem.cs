using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public Cinemachine.CinemachineFreeLook m_freelook;

    [HideInInspector]
    public bool OnPauseMenu;

    public static InputSystem instance;

    private void Awake()
    {
        instance = this;
    }


    private void Update()
    {      
        if(Input.GetKeyDown(KeyCode.Escape) || (Input.GetKeyDown(KeyCode.Joystick1Button7)))
        {
            if (!OnPauseMenu)
            {
                OnPauseMenu = true;
            }
            else
            {
                OnPauseMenu = false;
            }
        }
    }
}
