using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour
{
    public GameObject Player;

    private void Start()
    {
        Player = Character3D.m_instance.gameObject;
    }

    private void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            Player.transform.position = transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
    }
}
