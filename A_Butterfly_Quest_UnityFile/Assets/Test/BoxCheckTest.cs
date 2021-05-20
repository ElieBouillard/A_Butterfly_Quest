using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BoxCheckTest : MonoBehaviour
{
    [Range(0f, 10f)]
    public float maxDist;
    [Range(0f, 10f)]
    public float offSetForward;

    public bool debugGizmos;

    private void Update()
    {
         RaycastHit hit;
        if (Physics.BoxCast(transform.position + transform.forward * offSetForward, transform.lossyScale / 2, transform.forward, out hit, transform.rotation, maxDist))
        {
            //Debug.Log(hit.transform);
            //Debug.Log("Ca touche");
        }
        
    }

    private void OnDrawGizmos()
    {
        if (debugGizmos)
        {
            Gizmos.color = Color.blue;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(Vector3.zero, transform.localScale);
        }        
    }
}
