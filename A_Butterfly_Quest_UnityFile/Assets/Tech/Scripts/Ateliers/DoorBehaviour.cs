using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class DoorBehaviour : MonoBehaviour
{
    public enum DoorType { UnlockWithReceptacle, UnlockWithPressurePlates }
    [Header("Type")]
    public DoorType m_doorType;

    [Header("References")]
    public GameObject[] ItemsWichUnlock;

    [Header("Debug")]
    public bool isOpen;


    private void Update()
    {

        if(m_doorType == DoorType.UnlockWithReceptacle)
        {
            if (ItemsWichUnlock != null)
            {
                isOpen = isAllReceptaclesCompleted();
            }
            else
            {
                Debug.LogError("Receptacles List is Empty !");
            }
        }
    }

    private bool isAllReceptaclesCompleted()
    {
        for (int i = 0; i < ItemsWichUnlock.Length - 1; i++)
        {
            Receptacle currReceptacle = ItemsWichUnlock[i].GetComponent<Receptacle>();
            if (!currReceptacle.Completed)
            {
                return false;
            }
        }
        return true;
    }
}

