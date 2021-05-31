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
    private GameObject butterflyLauncher;
    private GameObject PlayerCluster;
    private bool randomMove;

    private void Start()
    {
        player = Character3D.m_instance.gameObject;
        m_TargetPos = transform.position;
        PlayerCluster = GameObject.Find("PlayerCluster");
        butterflyLauncher = GameObject.Find("ButterflyLauncher");
        randomMove = true;
    }

    public void SetButterFlyTypeAtSpawn(int currButterflyType)
    {
        m_ButterflyType = (ButterflyType)currButterflyType;
    }

    public void SetButterFlyToLauncherPos()
    {
        randomMove = false;
        m_Speed = 10f;
        m_TargetPos = butterflyLauncher.transform.position;
        gameObject.transform.forward = player.transform.GetChild(0).transform.forward;
    }

    public void SetCatched()
    {
        randomMove = true;
        transform.SetParent(PlayerCluster.transform);
        gameObject.GetComponent<Collider>().enabled = false;
    }

    public int GetButterflyType()
    {
        return (int)m_ButterflyType;
    }

    float clockChangeTargetPos;
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
                clockChangeTargetPos = Random.Range(0.7f, 1.5f);
                transform.LookAt(m_TargetPos);
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, m_TargetPos, m_Speed / 100);
    }
}
