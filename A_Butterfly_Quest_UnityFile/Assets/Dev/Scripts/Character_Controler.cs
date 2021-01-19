using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controler : MonoBehaviour
{
    private Rigidbody Rb;
    public float Speed = 5;

    private float AxisX;
    private float AxisZ;

    private void Start()
    {
        Rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        AxisX = Input.GetAxis("Horizontal");
        AxisZ = Input.GetAxis("Vertical");      
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 dir = new Vector3(AxisX* Speed, Rb.velocity.y, AxisZ * Speed);
        Rb.velocity = dir;
    }
}
