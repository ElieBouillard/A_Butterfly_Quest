using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyClusterV2 : MonoBehaviour
{
    public bool isFollowingPlayer;
    
    [Header("ButterlysCount")]
    public int NormalCount;
    public int IllusionCount;
    public int TempeteCount;

    [Header("Settings")]
    public float m_Speed;
    [Range(0f,50f)]
    public float Range;
    public bool Respawn;
    [Range(0f, 20f)]
    public float RangeScareByPlayer;

    [Header("Debug / Player Cluster")]
    [Range(0, 10)]
    public float PosBehindPlayer;
    [Range(0, 10)]
    public float PosY;
    [Range(-10, 10)]
    public float PosX;
    [Range(0, 0.1f)]
    public float Speed;

    private float Speedydididi;
    

    [Header("Links")]
    public GameObject ButterflyNormal;
    public GameObject ButterflyIllusion;
    public GameObject ButterflyTempete;

    private Vector3? targetPos;
    private GameObject playerMesh;
    private GameObject player;
    private float distToPlayer;



    private void Start()
    {
        player = Character3D.m_instance.gameObject;
        playerMesh = player.transform.GetChild(0).gameObject;
        if (isFollowingPlayer)
        {
            transform.position = playerMesh.transform.position;
        }
        SpawnButterflys();

    }
    float clockMove;
    float clockSpawn;
    bool canSpawn;
    private void Update()
    {
        if (isFollowingPlayer)
        {
            Vector3 targetPos;
            targetPos = playerMesh.transform.position - playerMesh.transform.forward * PosBehindPlayer + Vector3.up * PosY + playerMesh.transform.right * PosX;
            distToPlayer = (targetPos - transform.position).magnitude;
            if (distToPlayer < 0f)
            {
                Speedydididi = 0.02f;
            }
            else if (distToPlayer > 2.5f)
            {
                Speedydididi = 0.1f;
            }
            else
            {
                float distRatio = (distToPlayer - 0f) / (2.5f - 0);
                float diffSpeed = 0.1f - 0.02f;
                Speedydididi = (distRatio * diffSpeed) + 0.02f;
            }

            transform.position = Vector3.MoveTowards(transform.position, targetPos, Speedydididi);

            if (Shoot.Instance.Aiming)
            {
                PosX = -1f;
            }
            else
            {
                PosX = 0f;
            }
            

        }
        else
        {
            if((player.transform.position - transform.position).magnitude < RangeScareByPlayer && player.GetComponent<Rigidbody>().velocity.magnitude > 3.5f)
            {
                m_Speed = 10f;
                clockMove = 0f;
            }
            else
            {
                m_Speed = Random.Range(1.5f, 3f);
            }

            if (targetPos == null)
            {
                if (clockMove > 0)
                {
                    clockMove -= Time.deltaTime;
                }
                else
                {
                    targetPos = new Vector3(transform.parent.transform.position.x + Random.Range(-Range, Range), transform.parent.transform.position.y, transform.parent.transform.position.z + Random.Range(-Range, Range));
                    if (Vector3.Distance(transform.parent.transform.position, targetPos.Value) > Range)
                    {
                        targetPos = null;
                    }
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos.Value, m_Speed / 100f);
                Vector3 myDistance = targetPos.Value - transform.position;
                if (myDistance.magnitude < 0.2f)
                {
                    targetPos = null;
                    clockMove = Random.Range(0.5f, 1.5f);
                }
            }
        }

        if (!isFollowingPlayer && Respawn)
        {
            if (gameObject.transform.childCount == 0 && canSpawn == false)
            {
                clockSpawn = Random.Range(5f, 10f);
                canSpawn = true;
            }

            if (clockSpawn > 0)
            {
                clockSpawn -= Time.deltaTime;
            }
            else
            {
                if (canSpawn)
                {
                    SpawnButterflys();
                    canSpawn = false;
                }
            }
        }
    }

    private void SpawnButterflys()
    {
        GameObject currInstantiateButterfly;
        for (int i = 0; i < NormalCount; i++)
        {
            currInstantiateButterfly = Instantiate(ButterflyNormal, this.transform);
            currInstantiateButterfly.GetComponent<ButterflyBehaviourV2>().SetButterFlyTypeAtSpawn(0);
        }
        for (int i = 0; i < IllusionCount; i++)
        {
            currInstantiateButterfly = Instantiate(ButterflyIllusion, this.transform);
            currInstantiateButterfly.GetComponent<ButterflyBehaviourV2>().SetButterFlyTypeAtSpawn(1);
        }
        for (int i = 0; i < TempeteCount; i++)
        {
            currInstantiateButterfly = Instantiate(ButterflyTempete, this.transform);
            currInstantiateButterfly.GetComponent<ButterflyBehaviourV2>().SetButterFlyTypeAtSpawn(2);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!isFollowingPlayer)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.parent.position, Range);
        }

        Gizmos.DrawWireSphere(transform.position, RangeScareByPlayer);
    }
}
