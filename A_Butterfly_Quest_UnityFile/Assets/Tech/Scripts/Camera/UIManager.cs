using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Links")]
    public GameObject ConstentHUD;
    public GameObject PauseHUD;
    public GameObject Crosshair;
    public Text ButterflyCountText;
    public GameObject ButterflyTypeSelected;

    [Header("ButterlyTypeSelectionSprites")]
    public Sprite[] ButterflyType;

    [Header("Sensisivity")]
    public Slider slideXaxis;
    public Slider slideYaxis;

    private void Start()
    {
        ShowCrosshair(true);
        slideXaxis.value =  InputSystem.instance.m_freelook.m_XAxis.m_MaxSpeed;
        slideYaxis.value = InputSystem.instance.m_freelook.m_YAxis.m_MaxSpeed;
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



        ButterflyCountText.text = ButterflyInventory.Instance.ButterflyInInventory[ButterflyTypeSelection.Instance.SelectionTypeValue].Count.ToString();

        //ButterflyTypeSelection
        int selectedType = ButterflyTypeSelection.Instance.SelectionTypeValue;
        Image ButterflyTypeSrite = ButterflyTypeSelected.GetComponent<Image>();
        ButterflyTypeSrite.sprite = ButterflyType[selectedType];

        InputSystem.instance.m_freelook.m_XAxis.m_MaxSpeed = slideXaxis.value;
        InputSystem.instance.m_freelook.m_YAxis.m_MaxSpeed = slideYaxis.value;
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
