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

    float clockChangeTargetPos;
    private void Update()
    {
        if (followPlayer)
        {
            m_TargetPos = player.transform.position;
        }
        else
        {
            if(clockChangeTargetPos > 0)
            {
                clockChangeTargetPos -= Time.deltaTime;
            }
            else
            {
                m_Speed = Random.Range(3f, 6f);
                m_TargetPos = new Vector3(transform.parent.position.x + Random.Range(-MovementRange, MovementRange), transform.parent.position.y + Random.Range(-MovementRange, MovementRange), transform.parent.position.z + Random.Range(-MovementRange, MovementRange));
                clockChangeTargetPos = Random.Range(1f,2f);
                transform.LookAt(m_TargetPos);
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, m_TargetPos, m_Speed/100);
    }
}
