using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyTypeSelection : MonoBehaviour
{
    public static ButterflyTypeSelection Instance;

    public int SelectionTypeValue;
    public int MaxButterflyType = 2;

    private void Awake()
    {
        Instance = this;
        SelectionTypeValue = 0;
    }

    private void Update()
    {
        if (Input.GetButtonDown("ButterflySelectionRight"))
        {
            if(SelectionTypeValue < MaxButterflyType)
            {
                SelectionTypeValue += 1;
            }
            else
            {
                SelectionTypeValue = 0;
            }

            //VFX
            VFXManager.m_instance.ChangeFlowerGlowColor(SelectionTypeValue);

        }

        if (Input.GetButtonDown("ButterflySelectionLeft"))
        {
            if(SelectionTypeValue > 0)
            {
                SelectionTypeValue -= 1;
            }
            else
            {
                SelectionTypeValue = MaxButterflyType;
            }

            //VFX
            VFXManager.m_instance.ChangeFlowerGlowColor(SelectionTypeValue);
        }
    }
}
