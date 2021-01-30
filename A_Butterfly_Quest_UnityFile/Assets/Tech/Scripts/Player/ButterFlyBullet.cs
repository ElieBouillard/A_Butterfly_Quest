using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyBullet : MonoBehaviour
{
    public GameObject Player;
    [HideInInspector]
    public Rigidbody rb;
    [HideInInspector]
    public Vector3 target = Vector3.zero;
    
    [Header("BulletStats")]
    public float ButterflySpeed = 0;
    public float Damage = 0;
    public float TicFirePerSec = 0;
    public bool Illusion = false;


    private Vector3 direction;
    public float distPlayerToButterfly;
    public float distanceMax;


    public bool onHit;


    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        Player = GameObject.Find("Player");
    }

    private void Update()
    {
        //Distance du papillon
        if (enabled)
        {
            distPlayerToButterfly = Vector3.Distance(transform.position, Player.transform.position);
            if(distPlayerToButterfly > distanceMax)
            {
                gameObject.SetActive(false);
                ButterflyInventory.Instance.AddToReloadList(ButterflySpeed, Damage, TicFirePerSec, Illusion);
            }
        }
    }

    public void GetButterflyInfo(ButterflyEntity currButterfly)
    {
        ButterflySpeed = currButterfly.ButterflySpeed;
        Damage = currButterfly.Damage;
        TicFirePerSec = currButterfly.TicFirePerSec;
        Illusion = currButterfly.Illusion;
    }

    private void FixedUpdate()
    {
        if (enabled)
        {
            //Si il y une cible
            if (onHit)
            {
                direction = target - transform.position;
                direction.Normalize();
                rb.velocity = direction * ButterflySpeed;
            }
            //Si le tir est dans le vide
            else
            {
                rb.velocity = rb.velocity;
            }
        }
            
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        ButterflyInventory.Instance.AddToReloadList(ButterflySpeed, Damage, TicFirePerSec, Illusion);
    }
}
