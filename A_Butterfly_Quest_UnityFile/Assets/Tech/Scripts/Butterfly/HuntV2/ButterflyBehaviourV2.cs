using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyBehaviourV2 : MonoBehaviour
{
    public enum ButterflyType { Normal, Illusion, Tempete }
    public ButterflyType m_ButterflyType;

    [Header("Setings")]
    public float m_Speed;
    public Vector3 m_TargetPos;

    private GameObject player;
    private bool followPlayer;

    private void Start()
    {
        player = Character3D.m_instance.gameObject;
        m_TargetPos = transform.position;
    }

    public void SetButterFlyTypeAtSpawn(int currButterflyType)
    {
        m_ButterflyType = (ButterflyType)currButterflyType;
    }

    public void SetCatched()
    {
        followPlayer = true;
    }

    private void Update()
    {
        if (followPlayer)
        {
            m_TargetPos = player.transform.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, m_TargetPos, m_Speed);
    }
}
