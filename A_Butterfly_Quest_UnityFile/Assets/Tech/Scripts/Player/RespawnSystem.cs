using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnSystem : MonoBehaviour
{
    private Transform currRespawnPoint;

    private void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<RespawnTrigger>())
        {
            RespawnTrigger currRespawnTrigger = other.GetComponent<RespawnTrigger>();
            currRespawnPoint = currRespawnTrigger.m_RespawnPoint;
            Debug.Log(currRespawnPoint);
        }
    }
}
