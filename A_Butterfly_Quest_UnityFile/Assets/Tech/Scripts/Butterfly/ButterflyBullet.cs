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
    public int Type = 0;

    private Vector3 direction;
    private Vector3 direction2;
    public float distPlayerToButterfly;
    public float distanceMax;
    private ButterflyEntity m_butterfly;
    public bool onHit;

    public Material[] TypesMat;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        Player = GameObject.Find("Player");
        Physics.IgnoreCollision(Player.GetComponent<Collider>(), GetComponent<Collider>(), true);
    }

    private void Update()
    {
        //Distance du papillon
        if (enabled)
        {
            distPlayerToButterfly = Vector3.Distance(transform.position, Player.transform.position);
            if(distPlayerToButterfly > distanceMax)
            {
                DisableButteryfly();
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
                rb.velocity = direction * ButterflySpeed * Time.deltaTime;
            }
            //Si le tir est dans le vide
            else
            {
                rb.velocity = direction2 * ButterflySpeed * Time.fixedDeltaTime;
            }
        }            
    }

    public void GetButterflyInfo(ButterflyEntity currButterfly)
    {
        m_butterfly = currButterfly;
        ButterflySpeed = currButterfly.ButterflySpeed;
        Damage = currButterfly.Damage;
        Type = (int) currButterfly.ButterflyType;
        SetColor(Type);
    }

    public void SetColor(int type)
    {
        MeshRenderer m_meshRenderer = GetComponent<MeshRenderer>();
        m_meshRenderer.material = TypesMat[type];
    }

    private void OnTriggerEnter(Collider other)
    {
        //HitReceptacle
        if(other.gameObject.GetComponent<Receptacle>())
        {
            Receptacle m_receptacle = other.gameObject.GetComponent<Receptacle>();
            if((int)m_receptacle.m_ButterflyNeededType == Type)
            {
                HitReceptacle(m_receptacle);
            }          
        }
        else
        {
            DisableButteryfly();
        }
    }
    
    public void DisableButteryfly()
    {
        gameObject.SetActive(false);
        ButterflyInventory.Instance.AddToReloadList(m_butterfly);
    }

    public void HitReceptacle(Receptacle currReceptacle)
    {
        gameObject.SetActive(false);
        ButterflyInventory.Instance.RemoveTravelList(m_butterfly);
        currReceptacle.AddButterfly();
    }

    public void GetDirection1()
    {
        direction = target - transform.position;
        direction.Normalize();
    }

    public void GetDirection2()
    {
        direction2 = Camera.main.transform.forward;
    }
}
