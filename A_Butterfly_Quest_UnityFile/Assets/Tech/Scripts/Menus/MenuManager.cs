using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public enum MenuType {StartMenu, OptionsMenu, AudioMenu, PauseMenu, ControlsMenu, ShowInputsMenu, SensitivityMenu}
    [Header("Type")]
    public MenuType m_menuType;

    [Space]
    [Header("Links")]
    public MainSceneManager m_MainSceneManager;
    public GameObject StartMenu;
    public GameObject OptionsMenu;
    public GameObject AudioMenu;
    public GameObject ControlsMenu;
    public GameObject KeyboardMenu;
    public GameObject ControllerMenu;
    public GameObject SensitivityMenu;

    [Header("UiObjects")]
    public GameObject BlackScreenObj;
    public GameObject Title;

    [Header("Audio")]
    public Slider MasterSlider;
    public Slider MusicSlider;
    public Slider SFXSlider;

    [Header("Sensity")]
    public Slider[] SensitivitySliders;

    private MenuController m_MenuControllerScpt;
    private Animator m_Animator;
    private GameObject NextMenu;

    private void Start()
    {
        m_MenuControllerScpt = gameObject.GetComponent<MenuController>();
        m_Animator = gameObject.GetComponent<Animator>();
    }

    float turnOffClock;
    bool canOff;

    float goPlayClock;
    bool canPlay;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.Return))
        {
            if(m_menuType == MenuType.StartMenu)
            {
                if (m_MenuControllerScpt.Index == 0)
                {
                    goPlayClock = 1f;
                    canPlay = true;
                    m_Animator.SetBool("Off", true);
                    BlackScreenObj.GetComponent<Animator>().SetBool("FadeIn", true);
                    Title.GetComponent<Animator>().SetBool("Off", true);
                }
                else if (m_MenuControllerScpt.Index == 1)
                {
                    TurnOff(OptionsMenu);
                }
                else if(m_MenuControllerScpt.Index == 2)
                {
                    Application.Quit();
                }
            }
            else if(m_menuType == MenuType.OptionsMenu)
            {
                if (m_MenuControllerScpt.Index == 0)
                {
                    if(Title != null)
                    {
                        Title.GetComponent<Animator>().SetBool("Off", true);
                    }
                    TurnOff(AudioMenu, 0);
                }
                else if (m_MenuControllerScpt.Index == 1)
                {
                    TurnOff(ControlsMenu, 0);
                }
                else if (m_MenuControllerScpt.Index == 2)
                {
                    TurnOff(StartMenu, 0);
                }
            }
            else if(m_menuType == MenuType.AudioMenu)
            {
                if (m_MenuControllerScpt.Index == 0)
                {
                    m_MenuControllerScpt.CanMooveInMenu = false;
                }
                else if (m_MenuControllerScpt.Index == 1)
                {
                    Debug.Log("MusicVolume");
                }
                else if (m_MenuControllerScpt.Index == 2)
                {
                    Debug.Log("SFX/UIVolume");
                }
                else if (m_MenuControllerScpt.Index == 3)
                {
                    TurnOff(OptionsMenu, 0);
                    Title.GetComponent<Animator>().SetBool("Off", false);
                }
            }
            else if (m_menuType == MenuType.PauseMenu)
            {
                if (m_MenuControllerScpt.Index == 0)
                {
                    UIManager.instance.Resume();
                }
                else if (m_MenuControllerScpt.Index == 1)
                {
                    TurnOff(OptionsMenu, 0);
                }
                else if (m_MenuControllerScpt.Index == 2)
                {
                    UIManager.instance.Resume();
                    SceneManager.LoadScene(0);

                }
            }
            else if (m_menuType == MenuType.ControlsMenu)
            {
                if (m_MenuControllerScpt.Index == 0)
                {
                    TurnOff(KeyboardMenu, 0);
                }
                else if (m_MenuControllerScpt.Index == 1)
                {
                    TurnOff(ControllerMenu, 0);
                }
                else if (m_MenuControllerScpt.Index == 2)
                {
                    TurnOff(SensitivityMenu, 0);
                }
                else if (m_MenuControllerScpt.Index == 3)
                {
                    TurnOff(StartMenu, 0);
                }
            }
            else if(m_menuType == MenuType.ShowInputsMenu)
            {
                if (m_MenuControllerScpt.Index == 0)
                {
                    TurnOff(ControlsMenu, 0);
                }
            }
            else if (m_menuType == MenuType.SensitivityMenu)
            {
                if (m_MenuControllerScpt.Index == 4)
                {
                    TurnOff(ControlsMenu, 0);
                }
            }
        }

        if (m_menuType == MenuType.AudioMenu)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                if (m_MenuControllerScpt.Index == 0)
                {
                    MasterSlider.value += 0.2f;
                }
                else if (m_MenuControllerScpt.Index == 1)
                {
                    MusicSlider.value += 0.2f;
                }
                else if (m_MenuControllerScpt.Index == 2)
                {
                    SFXSlider.value += 0.2f;
                }
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                if (m_MenuControllerScpt.Index == 0)
                {
                    MasterSlider.value -= 0.2f;
                }
                else if (m_MenuControllerScpt.Index == 1)
                {
                    MusicSlider.value -= 0.2f;
                }
                else if (m_MenuControllerScpt.Index == 2)
                {
                    SFXSlider.value -= 0.2f;
                }
            }
        }

        if (m_menuType == MenuType.SensitivityMenu)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                if(m_MenuControllerScpt.Index != 1 && m_MenuControllerScpt.Index != 4)
                {
                    SensitivitySliders[m_MenuControllerScpt.Index].value += 1f;
                }
                else
                {
                    SensitivitySliders[1].value += 0.05f;

                }
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                if (m_MenuControllerScpt.Index != 1 && m_MenuControllerScpt.Index != 4)
                {
                    SensitivitySliders[m_MenuControllerScpt.Index].value -= 1f;
                }
                else
                {
                    SensitivitySliders[1].value -= 0.05f;

                }
            }
        }

        if (canOff)
        {
            if (turnOffClock > 0)
            {
                turnOffClock -= Time.deltaTime;
            }
            else
            {
                gameObject.SetActive(false);
                NextMenu.SetActive(true);
                canOff = false;
            }
        }

        if (canPlay)
        {
            if (goPlayClock > 0)
            {
                goPlayClock -= Time.deltaTime;
            }
            else
            {
                GoPlay();
                canPlay = false;
            }
        }
    }

    private void GoPlay()
    {
        m_MainSceneManager.LoadGameplayScenes();
    }

    public void TurnOff(GameObject nextMenu = null, int buttonIndex = -1)
    {
        m_MenuControllerScpt.canPlaySound = false;
        m_Animator.SetBool("Off", true);
        turnOffClock = 0.5f;
        canOff = true;
        NextMenu = nextMenu;
        if(buttonIndex != -1)
        {
            m_MenuControllerScpt.Index = buttonIndex;
        }
    }
}
