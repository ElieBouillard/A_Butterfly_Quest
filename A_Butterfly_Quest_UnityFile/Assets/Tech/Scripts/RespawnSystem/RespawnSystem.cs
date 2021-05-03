using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnSystem : MonoBehaviour
{
    public Vector3 currRespawnPoint;

    private void Start()
    {
        currRespawnPoint = gameObject.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<RespawnTrigger>())
        {
            RespawnTrigger currRespawnTrigger = other.GetComponent<RespawnTrigger>();
            currRespawnPoint = currRespawnTrigger.GetRespawnPoint();
            Debug.Log("Save at " + currRespawnPoint);
        }
    }
}
