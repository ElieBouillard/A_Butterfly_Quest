using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    protected Transform m_RespawnPoint;

    private void Start()
    {
        m_RespawnPoint = gameObject.transform.GetChild(0).gameObject.transform;
    }

    public Transform GetRespawnPoint()
    {
        return m_RespawnPoint;
    }
}
