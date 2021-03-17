using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    private NavMeshAgent Agent;
    public GameObject Target;
    public LayerMask GroundMask;
    public LayerMask PlayerMask;
    public LayerMask EnemyMask;
    public int obstacleMask;

    private MeshRenderer m_MeshRenderer;
    private Rigidbody rb;

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

    [Header("WaitingState")]
    public float WaitingTime;

    [Header("AttackState")]
    public float ChargeAttacksSpeed;
    public float ChannelingTimeValue;
    public float ChannelingClock;
    public bool InChargeAttack;
    private bool InitCanalisationAttack;
    public bool PlayerInAttackRange;
    public bool inChargeAttackRange;

    [Header("ChargeAttackInfo")]
    public Vector3 dirChagre;
    public float distChagre;

    [Header("DebugInfo")]
    public bool PlayerInDetectionRange;
    private float WaitingClock;
    public Vector3? posAfterAttack;
    public Vector3? RandomTargetPos;
    private Vector3 distanceToTargetPos;
    public Material[] StatesMat;
    public Vector3 InitialPos;
    private bool Started = false;
    public bool DisableAI = false;
    private float attackRange;
    public bool horsState;



    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        m_MeshRenderer = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
        horsState = false;
    }

    private void Start()
    {
        InitialPos = transform.position;
        Target = Character3D.m_instance.gameObject;
        Started = true;
        obstacleMask = LayerMask.GetMask("Grounded");
        attackRange = AttackRange;
    }

    private void Update()
    {
        PlayerInDetectionRange = Physics.CheckSphere(transform.position, DetectionRange, PlayerMask);

        PlayerInAttackRange = Physics.CheckSphere(transform.position, attackRange, PlayerMask);

        if (!PlayerInDetectionRange && !PlayerInAttackRange && !horsState)
        {
            WaitingState();
        }
        if (PlayerInDetectionRange && !PlayerInAttackRange && !horsState)
        { 
            ChaseState();        
        }

        if (PlayerInDetectionRange && PlayerInAttackRange && !horsState)
        {
            AttackState();
        }

        if (PlayerInAttackRange)
        {
            attackRange = AttackRange + CancelAttackRange;
        }
        else
        {
            attackRange = AttackRange;
        }

        if (horsState)
        {
            Agent.ResetPath();
        }
    }

    private void WaitingState()
    {
        Agent.speed = Speed;

        if (RandomTargetPos == null)
        {
            if(WaitingClock <= 0)
            {
                Agent.isStopped = false;
                SearchRandomPoint();
            }
            else if(WaitingClock > 0)
            {
                WaitingClock -= Time.deltaTime;
            }
        }
        else
        {
            Agent.SetDestination(RandomTargetPos.Value);
            Debug.DrawRay(RandomTargetPos.Value, Vector3.up, Color.yellow, 3);

            distanceToTargetPos = RandomTargetPos.Value - transform.position;

            if (distanceToTargetPos.magnitude < 1f)
            {
                WaitForNewRandPos();
            }
        }
        //StateColor
        m_MeshRenderer.material = StatesMat[0];
    }
    private void SearchRandomPoint()
    {
        float randomX = Random.Range(-RandomPosRange, RandomPosRange);
        float randomZ = Random.Range(-RandomPosRange, RandomPosRange);

        Vector3 randPos = new Vector3(InitialPos.x + randomX, transform.position.y, InitialPos.z + randomZ);

        //NavMeshPosCheck
        NavMeshPath path = new NavMeshPath();
        Agent.CalculatePath(randPos, path);
        if(path.status == NavMeshPathStatus.PathComplete)
        {
            RandomTargetPos = randPos;
        }
        else
        {
            RandomTargetPos = null;
        }
        
    }
    public void WaitForNewRandPos()
    {
        Agent.isStopped = true;
        RandomTargetPos = null;
        WaitingClock = WaitingTime;
    }

    private void ChaseState()
    {
        Agent.isStopped = false;
        Agent.speed = Speed;
        Agent.SetDestination(Target.transform.position);

        RandomTargetPos = null;
        InChargeAttack = false;
        posAfterAttack = null;

        //StateColor
        m_MeshRenderer.material = StatesMat[1];
    }
    
    private void AttackState()
    {
        if (!InChargeAttack)
        {
            Agent.isStopped = true;
            transform.LookAt(new Vector3(Target.transform.position.x, transform.position.y, Target.transform.position.z));
        }

        if (InitCanalisationAttack)
        {
            InitChargeAttack();
        }

        if(ChannelingClock <= 0)
        {
            if (posAfterAttack == null)
            {
                dirChagre = Target.transform.position - transform.position;
                distChagre = dirChagre.magnitude;
                dirChagre.Normalize();
                float factor = 2;

                Vector3 tempPosAfterAttack;

                if (Physics.Raycast(transform.position, dirChagre, out RaycastHit hit, AttackRange * 2, obstacleMask))
                {
                    tempPosAfterAttack = transform.position + hit.distance * dirChagre;
                    Debug.DrawRay(transform.position, dirChagre * hit.distance, Color.red, 3);
                }
                else
                {
                    tempPosAfterAttack = transform.position + distChagre * factor * dirChagre;
                }               

                posAfterAttack = tempPosAfterAttack;
            }
        }
        else
        {
            ChannelingClock -= Time.deltaTime;
        }

        if (posAfterAttack != null)
        {
            InChargeAttack = true;
            Agent.isStopped = false;
            Agent.speed = ChargeAttacksSpeed;
            Agent.SetDestination(posAfterAttack.Value);
            Debug.DrawRay(posAfterAttack.Value, Vector3.up, Color.cyan, 3);

            float distanceToPosAfterAttack = Vector3.Distance(posAfterAttack.Value, transform.position);

            if(distanceToPosAfterAttack < 2f)
            {
                InChargeAttack = false;
                Agent.speed = Speed;
                posAfterAttack = null;
                InitCanalisationAttack = true;
            }
        }

        //StateColor
        m_MeshRenderer.material = StatesMat[2];
    }

    private void InitChargeAttack()
    {
        ChannelingClock = ChannelingTimeValue;
        InitCanalisationAttack = false;
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
            Gizmos.DrawWireSphere(transform.position, RandomPosRange);
        }
        else
        {
            Gizmos.DrawWireSphere(InitialPos, RandomPosRange);
        }
    }
}
