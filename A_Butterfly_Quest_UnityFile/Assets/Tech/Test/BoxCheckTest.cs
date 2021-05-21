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
    Vector3[] CubePoints(Vector3 center, Vector3 extents, Quaternion rotation)
    {
        Vector3[] points = new Vector3[8];
        points[0] = rotation * Vector3.Scale(extents, new Vector3(1, 1, 1)) + center;
        points[1] = rotation * Vector3.Scale(extents, new Vector3(1, 1, -1)) + center;
        points[2] = rotation * Vector3.Scale(extents, new Vector3(1, -1, 1)) + center;
        points[3] = rotation * Vector3.Scale(extents, new Vector3(1, -1, -1)) + center;
        points[4] = rotation * Vector3.Scale(extents, new Vector3(-1, 1, 1)) + center;
        points[5] = rotation * Vector3.Scale(extents, new Vector3(-1, 1, -1)) + center;
        points[6] = rotation * Vector3.Scale(extents, new Vector3(-1, -1, 1)) + center;
        points[7] = rotation * Vector3.Scale(extents, new Vector3(-1, -1, -1)) + center;

        return points;
    }

    void DrawCubePoints(Vector3[] points)
    {
        Debug.DrawLine(points[0], points[1]);
        Debug.DrawLine(points[0], points[2]);
        Debug.DrawLine(points[0], points[4]);

        Debug.DrawLine(points[7], points[6]);
        Debug.DrawLine(points[7], points[5]);
        Debug.DrawLine(points[7], points[3]);

        Debug.DrawLine(points[1], points[3]);
        Debug.DrawLine(points[1], points[5]);

        Debug.DrawLine(points[2], points[3]);
        Debug.DrawLine(points[2], points[6]);

        Debug.DrawLine(points[4], points[5]);
        Debug.DrawLine(points[4], points[6]);
    }
}
