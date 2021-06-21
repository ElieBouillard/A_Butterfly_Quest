using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trailer : MonoBehaviour
{
    Transform target;

    private void Start()
    {
        target = GameObject.Find("Player").transform;
    }
    void Update()
    {
        //transform.LookAt(target);
    }
}
