using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public enum MenuType {StartMenu, OptionsMenu}
    [Header("Type")]
    public MenuType m_menuType;

    [Space]
    [Header("Links")]
    public MainSceneManager m_MainSceneManager;
    public GameObject StartMenu;
    public GameObject OptionsMenu;

    private MenuController m_MenuControllerScpt;
    private Animator m_Animator;
    private GameObject NextMenu;

    private void Start()
    {
        m_MenuControllerScpt = gameObject.GetComponent<MenuController>();
        m_Animator = gameObject.GetComponent<Animator>();
    }

    float clock;
    bool canOff;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            if(m_menuType == MenuType.StartMenu)
            {
                if (m_MenuControllerScpt.Index == 0)
                {
                    m_MainSceneManager.LoadGameplayScenes();
                }
                else if (m_MenuControllerScpt.Index == 1)
                {
                    m_Animator.SetBool("Off", true);
                    clock = 0.5f;
                    canOff = true;
                    NextMenu = OptionsMenu;
                }
            }
            else if(m_menuType == MenuType.OptionsMenu)
            {
                if (m_MenuControllerScpt.Index == 0)
                {
                    Debug.Log("AudioSection");
                }
                else if (m_MenuControllerScpt.Index == 1)
                {
                    Debug.Log("ControlsSection");
                }
                else if (m_MenuControllerScpt.Index == 2)
                {
                    m_Animator.SetBool("Off", true);
                    clock = 0.5f;
                    canOff = true;
                    NextMenu = StartMenu;
                    m_MenuControllerScpt.Index = 0;
                }
            }
        }

        if (canOff)
        {
            if (clock > 0)
            {
                clock -= Time.deltaTime;
            }
            else
            {
                gameObject.SetActive(false);
                NextMenu.SetActive(true);
                canOff = false;
            }
        }       
    }
}
