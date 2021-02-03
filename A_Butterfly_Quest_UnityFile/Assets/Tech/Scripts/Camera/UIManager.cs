using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameObject Crosshair;

    public void Awake()
    {
        Crosshair = gameObject.transform.GetChild(0).gameObject;
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
    }

    public void ShowCrosshair(bool value)
    {
        Crosshair.SetActive(value);
    }
}
