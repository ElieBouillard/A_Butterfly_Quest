using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Links")]
    public GameObject ConstentHUD;
    public GameObject PauseHUD;
    public GameObject Crosshair;
    public Text ButterflyCountText;
    public GameObject ButterflyTypeSelected;

    [Header("ButterlyTypeSelectionSprites")]
    public Sprite[] ButterflyType;

    [Header("Sensisivity")]
    public Slider slideFreeLookXaxis;
    public Slider slideFreeLookYaxis;
    public Slider slideAimXaxis;
    public Slider slideAimYaxis;

    [HideInInspector]
    public bool OnPauseMenu;

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
        if (OnPauseMenu)
        {
            ShowPauseMenu(true);
        }
        else
        {
            ShowPauseMenu(false);
        }

        //InputPause
        if (Input.GetKeyDown(KeyCode.Escape) || (Input.GetKeyDown(KeyCode.Joystick1Button7)))
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

        //AffectGoodSpriteButterflyTypeSelection
        int selectedType = ButterflyTypeSelection.Instance.SelectionTypeValue;
        Image ButterflyTypeSrite = ButterflyTypeSelected.GetComponent<Image>();
        ButterflyTypeSrite.sprite = ButterflyType[selectedType];
        ButterflyCountText.text = ButterflyInventory.Instance.ButterflyInInventory[ButterflyTypeSelection.Instance.SelectionTypeValue].Count.ToString();
    }

    public void TurnOffPauseMenu()
    {
        OnPauseMenu = false;
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
        PauseHUD.SetActive(value);
    }
}
