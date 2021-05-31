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

    [Header("Debug / Player Cluster")]
    [Range(0, 10)]
    public float PosBehindPlayer;
    [Range(0, 10)]
    public float PosY;
    [Range(-10, 10)]
    public float PosX;
    [Range(0, 0.1f)]
    public float Speed;

    [Header("Links")]
    public GameObject ButterflyNormal;
    public GameObject ButterflyIllusion;
    public GameObject ButterflyTempete;

    private Vector3? targetPos;
    private GameObject player;

    private void Start()
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
        player = Character3D.m_instance.gameObject.transform.GetChild(0).gameObject;
        transform.position = player.transform.position;

    }
    float clockMove;
    private void Update()
    {
        if (isFollowingPlayer)
        {
            Vector3 targetPos;
            targetPos = player.transform.position - player.transform.forward * PosBehindPlayer + Vector3.up * PosY + player.transform.right * PosX;
            transform.position =  Vector3.MoveTowards(transform.position, targetPos, Speed);

        }
        else
        {
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
                transform.position = Vector3.MoveTowards(transform.position, targetPos.Value, Random.Range(2f, 4f) / 100f);
                Vector3 myDistance = targetPos.Value - transform.position;
                if (myDistance.magnitude < 0.2f)
                {
                    targetPos = null;
                    clockMove = Random.Range(0f, 2f);
                }
            }
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        if (!isFollowingPlayer)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.parent.position, Range);
        }
    }
}
