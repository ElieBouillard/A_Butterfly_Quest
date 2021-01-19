using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controler : MonoBehaviour
{
    private Rigidbody Rb;
    public float Speed = 5;

    private void Start()
    {
        Rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();       
    }

    private void Move()
    {
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal")* Speed, Rb.velocity.y, Input.GetAxis("Vertical") * Speed);
        Rb.velocity = dir;
    }
}
