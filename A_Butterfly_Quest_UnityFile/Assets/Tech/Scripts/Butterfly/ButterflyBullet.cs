using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyBullet : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody rb;
    private GameObject Player;
    [HideInInspector]
    public Vector3 target = Vector3.zero;
    
    [Header("BulletStats")]
    public float ButterflySpeed = 0;
    public float Damage = 0;

    private Vector3 direction;
    public float distPlayerToButterfly;
    public float distanceMax;
    private ButterflyEntity m_butterfly;

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
                ButterflyInventory.Instance.AddToReloadList(m_butterfly);
            }
        }
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

    public void GetButterflyInfo(ButterflyEntity currButterfly)
    {
        m_butterfly = currButterfly;
        ButterflySpeed = currButterfly.ButterflySpeed;
        Damage = currButterfly.Damage;
    }
    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        ButterflyInventory.Instance.AddToReloadList(m_butterfly);
    }
}
