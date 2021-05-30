using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyClusterV2 : MonoBehaviour
{
    public bool isFollowingPlayer;
    public enum ClusterType { Normal, Illusion, Tempete }
    [Space]
    [Header("Settings")]
    public ClusterType m_ClusterType;
    public int ButterflyAmountInCluster;
    public float m_Speed;
    public float m_MaxSpeed;
    [Range(0f,10f)]
    public float Range;
    [Range(0f, 5f)]
    public float MinMovementRange;

    [Space]
    [Header("Links")]
    public GameObject ButterflyObj;

    private Vector3? targetPos;

    private void Start()
    {
        GameObject currInstantiateButterfly;
        for (int i = 0; i < ButterflyAmountInCluster; i++)
        {
            currInstantiateButterfly = Instantiate(ButterflyObj, this.transform);
            currInstantiateButterfly.GetComponent<ButterflyBehaviourV2>().SetButterFlyTypeAtSpawn((int)m_ClusterType);
        }
    }

    private void Update()
    {
        if(targetPos == null)
        {
            targetPos = new Vector3(transform.parent.transform.position.x + Random.Range(-Range, Range), transform.parent.transform.position.y, transform.parent.transform.position.z + Random.Range(-Range, Range));
            if (Vector3.Distance(transform.parent.transform.position, targetPos.Value) > Range)
            {
                targetPos = null;
            } 
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos.Value, Random.Range(2f, 7f)/100f);
            Vector3 myVector = targetPos.Value - transform.position;
            if(myVector.magnitude < 0.2f)
            {
                targetPos = null;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.parent.position, Range);
        Gizmos.DrawWireSphere(transform.parent.position, MinMovementRange);
    }
}
