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

    [Space]
    [Header("Links")]
    public GameObject ButterflyObj;


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
       
    }
}
