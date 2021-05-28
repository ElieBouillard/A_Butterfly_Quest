using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Butterfly")
        {
            Debug.Log("ça touche");
        }
    }
}
