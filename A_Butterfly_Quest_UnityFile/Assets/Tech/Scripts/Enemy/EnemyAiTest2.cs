using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAiTest2 : MonoBehaviour
{
    [Header("References")]
    private NavMeshAgent Agent;
    public GameObject Target;
    public LayerMask GroundMask, PlayerMask;
    private MeshRenderer m_MeshRenderer;
    private Rigidbody rb;

    [Header("EnemyStats")]    
    public float Speed;
    public float DetectionRange;
    public float AttackRange;
    public bool PlayerInDetectionRange;
    public bool PlayerInAttackRange;
    [Range(0,5)]
    public float CancelAttackRange;
    public float TargetPosRange;
    public float WaitingTime;
    private float WaitingClock;

    public float ChargeAttacksSpeed;
    public float CanalisationAttackTime;
    public bool InChargeAttack;
    public bool InCanalisationAttack;
    public float ChargeClock;
    public Vector3? posAfterAttack;

    [Header("State Info")]
    public Vector3? TargetPos;
    private Vector3 distanceToTargetPos;
    public Material[] StatesMat;
    public Vector3 InitialPos;
    private bool Started = false;
    

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        m_MeshRenderer = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        InitialPos = transform.position;
        Target = Character3D.m_instance.gameObject;
        Started = true;
    }

    private void Update()
    {
        PlayerInDetectionRange = Physics.CheckSphere(transform.position, DetectionRange, PlayerMask);
        PlayerInAttackRange = Physics.CheckSphere(transform.position, AttackRange, PlayerMask);

        if (!PlayerInDetectionRange && !PlayerInAttackRange) WaitingState();
        if (PlayerInDetectionRange && !PlayerInAttackRange) ChaseState();
        if (PlayerInDetectionRange && PlayerInAttackRange) AttackState();
    }

    private void WaitingState()
    {
        m_MeshRenderer.material = StatesMat[0];
        Agent.speed = Speed;

        if (TargetPos == null)
        {
            if(WaitingClock <= 0)
            {
                Agent.isStopped = false;
                SearchTargetPoint();
            }
            else if(WaitingClock > 0)
            {
                WaitingClock -= Time.deltaTime;
            }
        }
        else
        {
            Agent.SetDestination(TargetPos.Value);

            distanceToTargetPos = TargetPos.Value - transform.position;

            if (distanceToTargetPos.magnitude < 2f)
            {
                ResestTargetPos();
            }
        }
    }

    private void SearchTargetPoint()
    {
        float randomX = Random.Range(-TargetPosRange, TargetPosRange);
        float randomZ = Random.Range(-TargetPosRange, TargetPosRange);

        TargetPos = new Vector3(InitialPos.x + randomX, transform.position.y, InitialPos.z + randomZ); 

        if(!Physics.Raycast(TargetPos.Value, -transform.up, 2f, GroundMask))
        {
            ResestTargetPos();
        }

    }

    private void ChaseState()
    {
        Agent.isStopped = false;
        Agent.speed = Speed;
        m_MeshRenderer.material = StatesMat[1];
        Agent.SetDestination(Target.transform.position);
    }
    
    private void AttackState()
    {
        m_MeshRenderer.material = StatesMat[2];
        if (!InChargeAttack)
        {
            Agent.isStopped = true;
        }
        transform.LookAt(Target.transform);
        ChargeAttack();
        if (InCanalisationAttack == false)
        {
            InitChargeAttack();
        }

        if(ChargeClock <= 0)
        {
            if (InChargeAttack == false)
            {
                Vector3 dir = Target.transform.position - transform.position;
                float distToTarget = dir.magnitude;
                dir.Normalize();
                posAfterAttack = transform.position + distToTarget * 2 * dir;
                Debug.Log(posAfterAttack);
                InChargeAttack = true;
                Agent.speed = ChargeAttacksSpeed;
                Agent.isStopped = false;
            }
        }
        else
        {
            ChargeClock -= Time.deltaTime;
        }


    }

    private void ChargeAttack()
    {
        if(posAfterAttack != null)
        {
            Agent.SetDestination(posAfterAttack.Value);
            distanceToTargetPos = posAfterAttack.Value - transform.position;

            if(distanceToTargetPos.magnitude < 1f)
            {
                Agent.isStopped = true;
                Agent.speed = Speed;
                InChargeAttack = false;
                InCanalisationAttack = false;
                posAfterAttack = null;
            }

        }
        
    }

    private void InitChargeAttack()
    {
        ChargeClock = CanalisationAttackTime;
        InCanalisationAttack = true;
    }

    public void ResestTargetPos()
    {
        TargetPos = null;
        WaitingClock = WaitingTime;
        Agent.isStopped = true;
    }

    //Debug
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, DetectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
        Gizmos.DrawWireSphere(transform.position, AttackRange + CancelAttackRange);
        Gizmos.color = Color.yellow;

        if (Started == false)
        {
            Gizmos.DrawWireSphere(transform.position, TargetPosRange);
        }
        else
        {
            Gizmos.DrawWireSphere(InitialPos, TargetPosRange);
        }
    }
}
