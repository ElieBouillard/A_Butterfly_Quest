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

    private void Start()
    {
        startPos = transform.position;
        targetPos = startPos;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        Activated = true;
        targetPos = startPos + new Vector3(0, -range, 0);
    }

    private void OnTriggerExit(Collider other)
    {
        Activated = false;
        targetPos = startPos;
    }
}
