using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterFlyBullet : MonoBehaviour
{
    public GameObject Player;
    private Rigidbody rb;
    [HideInInspector]
    public Vector3 target = Vector3.zero;
    public float ButterFlySpeed = 20f;

    private Vector3 direction;
    private float distPlayerToButterfly;
    private float distLauncherToTarget;


    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        Player = GameObject.Find("Player");
    }

    private void Update()
    {
        distPlayerToButterfly = Vector3.Distance(transform.position, Player.transform.position);

    }

    private void FixedUpdate()
    {
        if (enabled)
        {
            direction = target - transform.position;
            direction.Normalize();
            rb.velocity = direction * ButterFlySpeed;
        }   
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
    }
}
