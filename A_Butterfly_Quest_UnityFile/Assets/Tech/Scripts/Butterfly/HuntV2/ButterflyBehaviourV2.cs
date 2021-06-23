using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyBehaviourV2 : MonoBehaviour
{
    public enum ButterflyType { Normal, Illusion, Tempete }
    public ButterflyType m_ButterflyType;

    [Header("Setings")]
    private float m_Speed;
    public Vector3 m_TargetPos;
    public float MovementRange;

    private GameObject player;
    private GameObject butterflyLauncher;
    private GameObject PlayerCluster;
    private bool randomMove;
    private bool isFollowingPlayer;

    private float recoveryClock;
    private bool inRecovery;

    private float minRange;

    private void Start()
    {
        player = Character3D.m_instance.gameObject;
        m_TargetPos = transform.position;
        PlayerCluster = GameObject.Find("PlayerCluster");
        butterflyLauncher = GameObject.Find("ButterflyLauncher");
        randomMove = true;
        canScared = true;
    }

    public void SetButterFlyTypeAtSpawn(int currButterflyType)
    {
        m_ButterflyType = (ButterflyType)currButterflyType;
    }

    public void SetButterFlyToLauncherPos()
    {
        randomMove = false;
        m_Speed = 5f;
        m_TargetPos = butterflyLauncher.transform.position;
        gameObject.transform.forward = player.transform.GetChild(0).transform.forward;
    }

    public void SetCatched()
    {
        isFollowingPlayer = true;
        randomMove = true;
        transform.SetParent(PlayerCluster.transform);
        gameObject.GetComponent<Collider>().enabled = false;

        gameObject.transform.GetChild(0).transform.gameObject.GetComponent<ParticleSystem>().Stop();

        //addTrail
        TrailRenderer m_trailRend;
        if(transform.Find("FX").TryGetComponent<TrailRenderer>(out m_trailRend))
        {
            m_trailRend.enabled = true;
            m_trailRend.startWidth = Random.Range(0.01f, 0.05f);
            m_trailRend.time = Random.Range(0.1f, 1.5f);
            transform.Find("FX").Find("Glow").gameObject.SetActive(false);
        }
    }

    public int GetButterflyType()
    {
        return (int)m_ButterflyType;
    }

    public void SetToRecovery()
    {
        recoveryClock = 1f;
        inRecovery = true;
    }

    float clockChangeTargetPos;
    bool randomRotate;
    float clockScared;
    bool canScared;
    private void Update()
    {
        if (randomMove)
        {
            if (clockChangeTargetPos > 0)
            {
                clockChangeTargetPos -= Time.deltaTime;
            }
            else
            { 
                m_Speed = Random.Range(2f, 3f);

                m_TargetPos = new Vector3(transform.parent.position.x + Random.Range(-MovementRange, MovementRange), transform.parent.position.y + Random.Range(-MovementRange, MovementRange), transform.parent.position.z + Random.Range(-MovementRange, MovementRange));
                if (transform.parent.GetComponent<ButterflyClusterV2>().isFollowingPlayer)
                {
                    clockChangeTargetPos = Random.Range(0.1f, 0.3f);
                    if (transform.parent.transform.gameObject.GetComponent<ButterflyClusterV2>().distToTargetPos < 0.1)
                    {
                        clockChangeTargetPos = Random.Range(1f, 1.7f);
                    }
                }
                else
                {
                    clockChangeTargetPos = Random.Range(0.7f, 1.5f);
                }
                transform.LookAt(m_TargetPos);
            }
        }

        if (isFollowingPlayer)
        {
            MovementRange = 0.6f;            
        }
        else
        {
            if ((player.transform.position - transform.position).magnitude < 5f && player.gameObject.GetComponent<Rigidbody>().velocity.magnitude > 3.5f && canScared)
            {
                randomMove = false;
                transform.position += transform.forward * 8f * Time.deltaTime;
                if (randomRotate)
                {
                    gameObject.transform.eulerAngles = new Vector3(0, Random.Range(-180, 180), 0);
                    randomRotate = false;
                }
                clockScared = Random.Range(0.5f, 1f);
            }

            //if ((player.transform.position - transform.position).magnitude < 5f && Character3D.m_instance.inHuntNetHit == true)
            //{
                //randomMove = false;
                //m_TargetPos = transform.position + new Vector3(Random.Range(10f,15f), 0, Random.Range(10f, 15f));
                ////transform.position += transform.forward * 0.075f;
                //if (randomRotate)
                //{
                //    gameObject.transform.eulerAngles = new Vector3(0, Random.Range(-180, 180), 0);
                //    randomRotate = false;
                //}
            //}
        }
        
        if(clockScared > 0)
        {
            clockScared -= Time.deltaTime;
        }
        else
        {
            randomMove = true;
            randomRotate = true;
            canScared = true;
        }

        transform.position = Vector3.MoveTowards(transform.position, m_TargetPos, m_Speed / 100);
        

        if (inRecovery)
        {
            if(recoveryClock > 0)
            {
                recoveryClock -= Time.deltaTime;
            }
            else
            {
                m_Speed = 20f;
                m_TargetPos = PlayerCluster.transform.position;
                if((PlayerCluster.transform.position - transform.position).magnitude < 2f)
                {
                    ButterflyInventory.Instance.ButterflyInInventory[(int)this.m_ButterflyType].Add(this);
                    randomMove = true;
                    inRecovery = false;
                }                
            }
        }

        if (!NetVisualCollider.m_instance.colliderOn || NetVisualCollider.m_instance.gameObject.activeSelf == false) 
        {
            gameObject.transform.GetChild(0).transform.gameObject.GetComponent<ParticleSystem>().Stop();
        }
    }
}
