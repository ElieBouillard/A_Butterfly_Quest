using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresurePlate : MonoBehaviour
{

    public bool Activated;

    [Header("Collision Sphere")]
    [Range(0,5)]
    public float PosY;
    [Range(0,5)]
    public float Radius;

    private Vector3 Size;
    private Vector3 Pos;

    [Header("References")]
    public Material trueColor;
    public Material falseColor;

    private MeshRenderer m_meshRenderer;

    private void Start()
    {
        gameObject.GetComponent<MeshRenderer>().material = falseColor;
        m_meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        Pos = transform.position + new Vector3(0f, PosY, 0f);

        if (Physics.CheckSphere(Pos, Radius))
        {
            Activated = true;
            m_meshRenderer.material = trueColor;

        }
        else
        {
            Activated = false;
            m_meshRenderer.material = falseColor;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y + PosY, transform.position.z), Radius);
    }
}
