using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameObject Crosshair;
    private Text ButterflyInvetoryCount;

    public void Awake()
    {
        Crosshair = gameObject.transform.GetChild(0).gameObject;
        ButterflyInvetoryCount = gameObject.transform.GetChild(2).GetComponent<Text>();    
    }

    private void Update()
    {
        if (Shoot.Instance.Aiming)
        {
            ShowCrosshair(true);
        }
        else
        {
            ShowCrosshair(false);
        }

        ButterflyInvetoryCount.text = ButterflyInventory.Instance.ButterflyBasicInInventory.Count.ToString();
    }

    public void ShowCrosshair(bool value)
    {
        Crosshair.SetActive(value);
    }
}
