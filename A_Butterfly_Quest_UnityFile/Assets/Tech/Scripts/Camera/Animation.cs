using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    public GameObject spawnPoint;

    void LateUpdate()
    {
        transform.LookAt(spawnPoint.transform);
    }
}
