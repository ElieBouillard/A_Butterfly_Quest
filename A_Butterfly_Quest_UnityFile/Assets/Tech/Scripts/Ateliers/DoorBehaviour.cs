using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class DoorBehaviour : MonoBehaviour
{
    public enum DoorType { UnlockWithReceptacle, UnlockWithPressurePlates, UnlockWithKey }
    [Header("Parametres")]
    public DoorType m_doorType;
    public float OpenSpeed;
    public float CloseSpeed;

    [Header("KeySystem")]
    public int KeyNeeded;
    [Range(0f, 20f)]
    public float DetectionPlayerRange;

    [Header("References")]
    public GameObject[] ItemsWichUnlock;

    [Header("Debug")]
    public bool isOpen;


    private float Speed;
    private Vector3 closePos;
    private Vector3 openPos;
    private Vector3 targetPos;
    private LayerMask PlayerMask;

    private void Start()
    {
        if(m_doorType == DoorType.UnlockWithKey)
        {
            PlayerMask = LayerMask.GetMask("Player");
        }

        Speed = OpenSpeed;
        closePos = transform.position;
        targetPos = closePos;
        openPos = closePos + transform.right.normalized * 5;
    }

    private void Update()
    {
        if (isOpen)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPos, Speed / 10);

        if(m_doorType == DoorType.UnlockWithKey)
        {
            CheckPlayerUpdate();
        }
        else
        {
            if (ItemsWichUnlock.Length != 0)
            {
                isOpen = isAllItemsActivated();
            }
        }
    }

    private void OpenDoor()
    {
        targetPos = openPos;
        Speed = OpenSpeed;
    }

    private void CloseDoor()
    {
        targetPos = closePos;
        Speed = CloseSpeed;
    }

    private bool isAllItemsActivated()
    {
        
        for (int i = 0; i < ItemsWichUnlock.Length; i++)
        {
            if (m_doorType == DoorType.UnlockWithPressurePlates)
            {
                PresurePlate currPresurePlate = ItemsWichUnlock[i].GetComponent<PresurePlate>();
                if (!currPresurePlate.Activated)
                {
                    return false;
                }
            }
            else if (m_doorType == DoorType.UnlockWithReceptacle)
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

    private void CheckPlayerUpdate()
    {
        if(Physics.CheckSphere(transform.position + transform.right * transform.localScale.x, DetectionPlayerRange, PlayerMask))
        {
            if(KeyInventory.instance.GetKeyCount() >= KeyNeeded)
            {
                KeyInventory.instance.RemoveKeyFromInventory(KeyNeeded);
                isOpen = true;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + transform.right * transform.localScale.x, DetectionPlayerRange);
    }
}