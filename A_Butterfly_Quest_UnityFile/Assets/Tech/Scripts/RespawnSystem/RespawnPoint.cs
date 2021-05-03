using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    public bool ShowGizmo;
    [Range(0,5)]
    public float RadiusGizmo;

    private void OnDrawGizmosSelected()
    {
        if (ShowGizmo)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, RadiusGizmo);
        }
    }
}
