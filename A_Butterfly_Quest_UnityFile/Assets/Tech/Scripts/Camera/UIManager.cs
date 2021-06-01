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
    public GameObject OptionsMenuHUD;
    public GameObject BindingMenuHUD;
    public GameObject ReadMeMenuHUD;
    public GameObject Crosshair;

    [Header("ButterlyTypeSelectionSprites")]
    public Text[] ButterflyCountText;
    public Image[] ButterflyImage;
    public ButterflyImages[] Butterflys;
    public Image DashCd;
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

    private bool canShowPauseMenu = true;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ShowCrosshair(true);
    }

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

        //PauseMenu
        if (InputSystem.instance.OnPauseMenu)
        {
            ShowPauseMenu(true);
        }
        else
        {
            ShowPauseMenu(false);
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

        for (int i = 0; i < ButterflyCountText.Length; i++)
        {
            ButterflyCountText[i].text = ButterflyInventory.Instance.ButterflyInInventory[i].Count.ToString();
        }

    }

    public void TurnOnOptions()
    {
        canShowPauseMenu = false;
        OptionsMenuHUD.SetActive(true);
        PauseMenuHUD.SetActive(false);
    }
    public void TurnOnControls()
    {
        canShowPauseMenu = false;
        BindingMenuHUD.SetActive(true);
        PauseMenuHUD.SetActive(false);
    }
    public void TurnOnReadMe()
    {
        canShowPauseMenu = false;
        ReadMeMenuHUD.SetActive(true);
        PauseMenuHUD.SetActive(false);
    }
    
    public void BackToPauseMenu()
    {
        canShowPauseMenu = true;
        PauseMenuHUD.SetActive(true);

        ReadMeMenuHUD.SetActive(false);
        BindingMenuHUD.SetActive(false);
        OptionsMenuHUD.SetActive(false);
    }

    public void TurnOffPauseMenu()
    {
        canShowPauseMenu = true;
        InputSystem.instance.OnPauseMenu = false;
        BindingMenuHUD.SetActive(false);
        ReadMeMenuHUD.SetActive(false);
        OptionsMenuHUD.SetActive(false);
        Time.timeScale = 1;
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

    public void ShowPauseMenu(bool value)
    {
        if (canShowPauseMenu)
        {
            PauseMenuHUD.SetActive(value);
        }

        if (!value)
        {
            BindingMenuHUD.SetActive(false);
            ReadMeMenuHUD.SetActive(false);
            OptionsMenuHUD.SetActive(false);
            canShowPauseMenu = true;
        }
    }
}
