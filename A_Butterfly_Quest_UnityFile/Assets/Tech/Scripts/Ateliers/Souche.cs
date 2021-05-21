using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Souche : MonoBehaviour
{
    public enum SuccessType { UnlockWithReceptacle, UnlockWithPressurePlates }
    [Header("Parametres")]
    public SuccessType m_SuccessType;

    [Header("References")]
    public GameObject[] ItemsWichUnlock;

    private GameObject SuccessItem;
    private void Start()
    {
        SuccessItem = gameObject.transform.GetChild(0).gameObject;
        SuccessItem.SetActive(false);
    }

    private void Update()
    {
        if(isAllItemsActivated())
        {
            SuccessItem.SetActive(true);
        }
    }

    private bool isAllItemsActivated()
    {
        for (int i = 0; i < ItemsWichUnlock.Length; i++)
        {
            if (m_SuccessType == SuccessType.UnlockWithPressurePlates)
            {
                PresurePlate currPresurePlate = ItemsWichUnlock[i].GetComponent<PresurePlate>();
                if (!currPresurePlate.Activated)
                {
                    return false;
                }
            }
            else if (m_SuccessType == SuccessType.UnlockWithReceptacle)
            {
                Receptacle currReceptacle = ItemsWichUnlock[i].GetComponent<Receptacle>();
                if (!currReceptacle.Completed)
                {
                    return false;
                }
            }
        }
        return true;
    }
}
