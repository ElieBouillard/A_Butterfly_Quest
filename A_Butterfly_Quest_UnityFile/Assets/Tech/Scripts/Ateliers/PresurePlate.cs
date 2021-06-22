using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresurePlate : MonoBehaviour
{

    public bool OnTopOfMovingCube;

    public bool Activated;

    private MeshRenderer m_meshRenderer;

    public float speed;
    public float range;
    private Vector3 targetPos;
    private Vector3 startPos;
    private float clock;
    private bool canFalse;
    private void Start()
    {
        startPos = transform.position;
        targetPos = startPos;
    }

    public string sfxOn;
    public string sfxOff;



    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed);

        if(clock > 0)
        {
            clock -= Time.deltaTime;
        }
        else
        {
            if (canFalse)
            {
                Activated = false;
                targetPos = startPos;
                canFalse = false;
            }
        }

        if (OnTopOfMovingCube)
        {
            startPos = transform.parent.transform.position + Vector3.up * 1.5f;
            if (!Activated)
            {
                targetPos = startPos;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Activated = false;
            targetPos = startPos;
            PlaySound(sfxOff);
        }

        if (other.GetComponent<MovingCube>())
        {
            Activated = false;
            targetPos = startPos;
            PlaySound(sfxOff);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Activated = true;
            targetPos = startPos + new Vector3(0, -range, 0);
            PlaySound(sfxOn);
        }

        if(other.transform.name == "IllusionMeshPrefab")
        {
            Activated = true;
            targetPos = startPos + new Vector3(0, -range, 0);
            clock = Character3D.m_instance.clockCanDash;
            canFalse = true;
            PlaySound(sfxOn);
        }

        if (other.GetComponent<MovingCube>())
        {
            Activated = true;
            targetPos = startPos + new Vector3(0, -range, 0);
            PlaySound(sfxOn);
        }
    }

    void PlaySound(string sfxName)
    {
        if (AudioManager.instance.sounds[0].source != null)
        {
            AudioManager.instance.Play(sfxName);
        }
    }
}
