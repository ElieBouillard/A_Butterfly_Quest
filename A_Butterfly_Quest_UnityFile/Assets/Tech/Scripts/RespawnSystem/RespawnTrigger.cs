using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    protected Vector3 m_RespawnPoint;
    protected Quaternion m_RotationSpawn;
    public bool ShowGizmo;

    private void Start()
    {
        m_RespawnPoint = gameObject.transform.GetChild(0).gameObject.transform.position;
        m_RotationSpawn = gameObject.transform.GetChild(0).gameObject.transform.rotation;
    }

    public Vector3 GetRespawnPoint()
    {
        return m_RespawnPoint;
    }

    public Quaternion GetRotationSpawn()
    {
        return m_RotationSpawn;
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
