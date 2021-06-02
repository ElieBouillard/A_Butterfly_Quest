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

    public Vector3 GetCoorRespawnPoint()
    {
        return m_RespawnPoint.position;
    } 
    public Quaternion GetOrientationRespawnPoint()
    {
        return m_RespawnPoint.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<RespawnSystem>())
        {
            other.gameObject.GetComponent<RespawnSystem>().AsignRespawnCoor(GetCoorRespawnPoint());
            other.gameObject.GetComponent<RespawnSystem>().AsignRepsawnRotation(GetOrientationRespawnPoint());
        }
    }

}
