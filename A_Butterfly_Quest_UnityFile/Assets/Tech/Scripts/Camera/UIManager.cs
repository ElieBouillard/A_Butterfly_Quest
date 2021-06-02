using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

 

    [Header("Menus")]
    public GameObject InGameHUD;
    public GameObject PauseMenuHUD;
    public GameObject Crosshair;

    [Header("ButterlyTypeSelectionSprites")]
    public Text[] ButterflyCountText;
    public Image[] ButterflyImage;
    public ButterflyImages[] Butterflys;
    public Image DashCd;
    public Image DashSprite;
    public Color[] DashColors;
    [System.Serializable]
    public class ButterflyImages
    {
        public Sprite Selected;
        public Sprite Unselected;
    }

    [Header("Sensisivity")]
    public Slider slideFreeLookXaxis;
    public Slider slideFreeLookYaxis;
    public Slider slideAimXaxis;
    public Slider slideAimYaxis;

    [Header("HealthHud")]
    public Image[] hearts;
    public Sprite fullHearth;
    public Sprite emptyHearth;

    [Header("KeyHud")]
    public Text KeyCountTxt;

    [Header("DeathHud")]
    public Image BlackScreenDeath;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ShowCrosshair(true);
        PauseMenuHUD.SetActive(false);       
    }

    bool freezePlayer;
    private void Update()
    {
        //CrossHairHUD
        if (Shoot.Instance.Aiming)
        {
            ShowCrosshair(true);
        }
        else
        {
            ShowCrosshair(false);
        }

        //AffectGoodSpriteButterflyTypeSelection
        int selectedType = ButterflyTypeSelection.Instance.SelectionTypeValue;
        for (int i = 0; i < ButterflyImage.Length; i++)
        {
            if(selectedType == i)
            {
                ButterflyImage[i].sprite = Butterflys[i].Selected;
            }
            else
            {
                ButterflyImage[i].sprite = Butterflys[i].Unselected;
            }
        }

        //FreezePlayerPosOnPauseMenu
        if (freezePlayer)
        {
            Character3D.m_instance.forceNoJump = true;
            Character3D.m_instance.freezeDirection = true;
            Character3D.m_instance.FreezeInput = true;
        }

        for (int i = 0; i < ButterflyCountText.Length; i++)
        {
            ButterflyCountText[i].text = ButterflyInventory.Instance.ButterflyInInventory[i].Count.ToString();
        }

        if (Input.GetKeyDown(KeyCode.Escape) || (Input.GetKeyDown(KeyCode.Joystick1Button7)))
        {
            StartPauseMenu();
        }

    }

    public Vector2 GetFreeLookSensi()
    {
        return new Vector2(slideFreeLookXaxis.value, slideFreeLookYaxis.value);
    }
    public Vector2 GetAimSensi()
    {
        return new Vector2(slideAimXaxis.value, slideAimYaxis.value);
    }
    public void ShowCrosshair(bool value)
    {
        Crosshair.SetActive(value);
    }

    public void StartPauseMenu()
    {
        PauseMenuHUD.SetActive(true);
        AnimationManager.m_instance.canPlayStepSound = false;
        freezePlayer = true;
    }

    public void Resume()
    {
        PauseMenuHUD.SetActive(false);
        AnimationManager.m_instance.canPlayStepSound = true;
        freezePlayer = false;
        Character3D.m_instance.FreezePosPlayer(0.3f, true, true);
    }

}
