using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresurePlate : MonoBehaviour
{

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
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Activated = false;
            targetPos = startPos;
        }
        Debug.Log("Ca sort");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Enter Tag Player");
            Activated = true;
            targetPos = startPos + new Vector3(0, -range, 0);
        }

        if(other.transform.name == "IllusionMeshPrefab")
        {
            Debug.Log("C'est l'illus");
            Activated = true;
            targetPos = startPos + new Vector3(0, -range, 0);
            clock = Character3D.m_instance.clocksCanDash[1];
            canFalse = true;
        }
    }
}
