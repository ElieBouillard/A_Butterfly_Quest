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

    [Header("Receptacles ou Pressures Plates")]
    public GameObject[] ItemsWichUnlock;

    [Header("Sounds")]
    public AudioClip DoorSound;
    private AudioSource m_audioSource;
    private bool lastIsOpenState;
    private bool canStopSound;

    [Header("Debug")]
    public bool isOpen;

    private float Speed;
    private Vector3 closePos;
    private Vector3 openPos;
    private Vector3 targetPos;

    public Animator animator;
    public bool bigDoor;

    private GameObject player;

    private void Start()
    {
        Speed = OpenSpeed;
        closePos = transform.position;
        targetPos = closePos;
        openPos = closePos - transform.forward.normalized * 5f;
        player = Character3D.m_instance.gameObject;
        m_audioSource = GetComponent<AudioSource>();
        lastIsOpenState = false;
        if(m_audioSource != null)
        {
            m_audioSource.clip = DoorSound;
        }
        
    }

    private void Update()
    {
        if(lastIsOpenState != isOpen)
        {
            if(m_audioSource != null)
            {
                m_audioSource.Play();
            }
         
            //Debug.Log("Play");
            lastIsOpenState = isOpen;;
        }
        else if(targetPos == transform.position) 
        {
            if (m_audioSource != null)
            {
                m_audioSource.Stop();
            }
         
            //Debug.Log("Stop");
        }

        if (isOpen)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }

        if (!bigDoor)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Speed / 10);
        }       
        
        if(m_doorType == DoorType.UnlockWithKey)
        {
            CheckPlayerRangeUpdate();
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
        if (bigDoor)
        {
            animator.SetBool("Open", true);
        }
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

    private void CheckPlayerRangeUpdate()
    {
        if((player.transform.position - transform.position).magnitude < DetectionPlayerRange)
        {
            if(Input.GetKeyDown(KeyCode.Joystick1Button3) || Input.GetKeyDown("t"))
            {
                if (KeyInventory.instance.GetKeyCount() >= KeyNeeded)
                {
                    KeyInventory.instance.RemoveKeyFromInventory(KeyNeeded);
                    isOpen = true;
                }
            }            
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + transform.right * transform.localScale.x, DetectionPlayerRange);
    }
}