using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour
{
    public GameObject Player;

    private void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            Player.transform.position = transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
    }
}
