using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour
{
    public AudioManager m_AudioManager;
    public MenuController m_MenuController;
    public int thisIndex; 
    public Animator BackTextAnimator;

    private void Update()
    {
        if (m_MenuController.Index == thisIndex)
        {
            Selected(true);
            if (Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                Pressed();
            }
        }
        else
        {
            Selected(false);
        }
    }

    private void Selected(bool value)
    {
        BackTextAnimator.SetBool("Selected", value);
        if (m_MenuController.canPlaySound)
        {
            m_AudioManager.Play("Selected");
            m_MenuController.canPlaySound = false;
        }
    }

    private void Pressed()
    {
        m_AudioManager.Play("Pressed");
    }
}
