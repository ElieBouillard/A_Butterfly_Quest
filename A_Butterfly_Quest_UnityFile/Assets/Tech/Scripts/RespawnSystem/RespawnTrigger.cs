using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    protected Vector3 m_RespawnPoint;
    public bool ShowGizmo;

    private void Start()
    {
        m_RespawnPoint = gameObject.transform.GetChild(0).gameObject.transform.position;
    }

    public Vector3 GetRespawnPoint()
    {
        return m_RespawnPoint;
    }

    private void OnDrawGizmosSelected()
    {
        if (ShowGizmo)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position + GetComponent<BoxCollider>().center, GetComponent<BoxCollider>().size);
        }
    }
}
