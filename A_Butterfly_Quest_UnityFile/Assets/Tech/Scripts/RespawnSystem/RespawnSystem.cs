using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnSystem : MonoBehaviour
{
    public Vector3 currRespawnPoint;
    public Quaternion currRotationSpawn;

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
            currRotationSpawn = currRespawnTrigger.GetRotationSpawn();
            Debug.Log("-RESPAWNSYSTEM- Save at " + currRespawnPoint);
        }
    }
}
