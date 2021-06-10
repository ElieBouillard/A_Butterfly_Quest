using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTips : MonoBehaviour
{
    public bool succes;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            succes = true;
        }
    }

}
