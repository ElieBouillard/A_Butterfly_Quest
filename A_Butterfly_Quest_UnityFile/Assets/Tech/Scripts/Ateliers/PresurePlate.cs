using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresurePlate : MonoBehaviour
{

    public bool Activated;

    private MeshRenderer m_meshRenderer;

    private void OnTriggerEnter(Collider other)
    {
        Activated = true;
    }

    private void OnTriggerExit(Collider other)
    {
        Activated = false;
    }
}
