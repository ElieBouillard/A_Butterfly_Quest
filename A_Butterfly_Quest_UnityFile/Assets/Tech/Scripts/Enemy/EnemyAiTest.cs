using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAITest : MonoBehaviour
{
    [Header("References")]
    public GameObject Target;
    private NavMeshAgent Agent;
    public LayerMask GroundMask, PlayerMask, EnemyMask;
    private MeshRenderer m_MeshRenderer;

    [Header("EnemyStats")]    
    public float Speed;

    [Header("Ranges")]
    [Range(0,20)]
    public float DetectionRange;
    [Range(0, 20)]
    public float AttackRange;
    [Range(0,5)]
    public float CancelAttackRange;
    [Range(0,20)]
    public float RandomPosRange;

    [Header("ChargeAttack")]


    [Header("WaitingState")]
    public float WaitingTime;


    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        m_MeshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        
    }
}
