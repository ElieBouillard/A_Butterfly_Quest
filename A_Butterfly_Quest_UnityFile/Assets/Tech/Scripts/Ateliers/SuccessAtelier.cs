using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessAtelier : MonoBehaviour
{
    public bool AllPressurePlatesActivated = false;
    public bool FinishStage = false;

    [Header("References")]
    public GameObject[] PressurePlates;
    public GameObject FinishItem;
    private PresurePlate[] PressurePlatesScpts;

    private void Start()
    {
        PressurePlatesScpts = new PresurePlate[PressurePlates.Length];
        for (int i = 0; i < PressurePlates.Length; i++)
        {
            PressurePlatesScpts[i] = PressurePlates[i].gameObject.GetComponent<PresurePlate>();
        }
    }

    private void Update()
    {
        AllPressurePlatesActivated = AllPlatesActivated();

        if (AllPressurePlatesActivated)
        {
            FinishStage = true;
        }

        if (FinishStage)
        {
            FinishItem.SetActive(true);
            FinishItem.transform.Rotate(0, 50f * Time.deltaTime, 0);
        }
    }

    private bool AllPlatesActivated()
    {
        foreach (PresurePlate item in PressurePlatesScpts)
        {
            if (!item.Activated)
            {
                return false;
            }
        }
        return true;
    }
}
