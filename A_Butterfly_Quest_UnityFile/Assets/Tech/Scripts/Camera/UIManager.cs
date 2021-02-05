using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Links")]
    public GameObject Crosshair;
    public Text ButterflyCountText;
    public GameObject ButterflyTypeSelected;

    [Header("ButterlyTypeSelectionSprites")]
    public Sprite[] ButterflyType;

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

        ButterflyCountText.text = ButterflyInventory.Instance.ButterflyInInventory[ButterflyTypeSelection.Instance.SelectionTypeValue].Count.ToString();

        //ButterflyTypeSelection
        int selectedType = ButterflyTypeSelection.Instance.SelectionTypeValue;
        Image ButterflyTypeSrite = ButterflyTypeSelected.GetComponent<Image>();
        ButterflyTypeSrite.sprite = ButterflyType[selectedType];
    }

    public void ShowCrosshair(bool value)
    {
        Crosshair.SetActive(value);
    }
}
