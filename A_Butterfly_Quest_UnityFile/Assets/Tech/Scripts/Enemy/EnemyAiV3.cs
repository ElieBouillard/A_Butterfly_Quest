using UnityEngine;
using UnityEngine.AI;

public class EnemyAiV3 : MonoBehaviour
{
    public enum EnemyState {Patroling, Chasing, Attacking}
    public EnemyState m_enemyState;

    [Header("Links")]
    private Animator m_Animator;
    private NavMeshAgent m_Agent;
    private GameObject TargetObj;

    [Header("Ranges")]
    [Range(0f, 20f)]
    public float PatrolingRange;
    [Range(0f, 20f)]
    public float ChasingRange;
    [Range(0f, 2f)]
    public float AttackingRange;

    [Header("PatrolingParametres")]
    public float PatrolingSpeed;

    [Header("ChasingParametres")]
    public float ChasingSpeed;

    [Header("AttackParametres")]
    public Collider HitCollider;

    [Header("Debug")]
    public bool ShowDebugRay;

    //PatrolingDebug
    private bool started;
    private Vector3 initialPos;
    private Vector3? targetPosPatroling;
    private NavMeshPath m_NavMeshPathPatroling;
    private float clockPatrolingPause;

    //AttackingDebug
    private bool inAttack;

    private float distanceToTarget;

    private GameObject IllusionMeshPrefab;
    private bool checkIllsionClone;


    private void Start()
    {
        //Links
        m_Agent = gameObject.GetComponent<NavMeshAgent>();
        m_Animator = gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();
        TargetObj = Character3D.m_instance.gameObject;

        //PatrolingStart
        started = true;
        targetPosPatroling = null;
        initialPos = transform.position;

        //Attack
        HitCollider.enabled = false;

        //IllusionClone
        IllusionMeshPrefab = Character3D.m_instance.IllusionMeshItem;
        Physics.IgnoreCollision(GetComponent<Collider>(), IllusionMeshPrefab.GetComponent<Collider>(), true);
    }

    private void Update()
    {
        StateMachineSystem();
        RangesSystem();
        //AnimatorSystem();

        //IllusionClodeSystem
        if (IllusionMeshPrefab.activeSelf)
        {
            if (!checkIllsionClone)
            {
                TargetObj = IllusionMeshPrefab;
                checkIllsionClone = true;
            }
        }
        else
        {
            if (checkIllsionClone)
            {
                TargetObj = Character3D.m_instance.gameObject;
                checkIllsionClone = false;
            }
        }
    }

    #region System
    private void StateMachineSystem()
    {
        if(m_enemyState == EnemyState.Patroling)
        {
            PatrolingUpdate();
        }

        if(m_enemyState == EnemyState.Chasing)
        {
            ChasingUpdate();
        }

        if(m_enemyState == EnemyState.Attacking)
        {
            AttackingUpdate();
            m_Agent.isStopped = true;
        }
        else
        {
            m_Agent.isStopped = false;
        }
    }

    private void RangesSystem()
    {
        distanceToTarget = (TargetObj.transform.position - transform.position).magnitude;

        if (!inAttack)
        {
            if (distanceToTarget > ChasingRange)
            {
                m_enemyState = EnemyState.Patroling;
            }

            if (distanceToTarget < ChasingRange && distanceToTarget > AttackingRange)
            {
                m_enemyState = EnemyState.Chasing;
            }
        }       

        if (distanceToTarget < AttackingRange)
        {
            m_enemyState = EnemyState.Attacking;
        }
    }

    private void AnimatorSystem()
    {

    }
    #endregion

    #region Patroling
    private void PatrolingUpdate()
    {
        if(targetPosPatroling == null)
        {
            if (clockPatrolingPause > 0)
            {
                clockPatrolingPause -= Time.deltaTime;
            }
            else
            {
                SearchTagetPosPatroling();
            }
        }
        else
        {
            m_Animator.SetBool("Walk", true);

            m_Agent.SetDestination(targetPosPatroling.Value);

            if (ShowDebugRay)
                Debug.DrawRay(targetPosPatroling.Value, Vector3.up * 2f, Color.blue, 2f);

            //If agent reach targetPosPatroling
            if (!m_Agent.pathPending)
            {
                if(m_Agent.remainingDistance <= m_Agent.stoppingDistance)
                {
                    if(m_Agent.hasPath || m_Agent.velocity.sqrMagnitude <= 0.5f)
                    {
                        m_Animator.SetBool("Walk", false);

                        targetPosPatroling = null;
                        clockPatrolingPause = 2f;
                    }
                }
            }

            if (m_Agent.pathStatus == NavMeshPathStatus.PathPartial)
            {
                targetPosPatroling = null;
            }
        }
        m_Agent.speed = PatrolingSpeed;
        m_Agent.acceleration = 10;
        //AnimSystem
        if (m_Animator.GetBool("Run"))
        {
            m_Animator.SetBool("Run", false);
        }
    }

    private void SearchTagetPosPatroling()
    {
        m_NavMeshPathPatroling = new NavMeshPath();
        float RandomX = Random.Range(-PatrolingRange, PatrolingRange);
        float RandomZ = Random.Range(-PatrolingRange, PatrolingRange);
        Vector3? tempTargetPosPatroling = new Vector3(initialPos.x + RandomX, transform.position.y, initialPos.z + RandomZ);
        if (Vector3.Distance(tempTargetPosPatroling.Value, initialPos) < PatrolingRange && m_Agent.CalculatePath(tempTargetPosPatroling.Value, m_NavMeshPathPatroling) && Vector3.Distance(tempTargetPosPatroling.Value, transform.position) > 1.5f) 
        {
            targetPosPatroling = tempTargetPosPatroling;
        }
        else
        {
            targetPosPatroling = null;
        }
    }
    #endregion

    private void ChasingUpdate()
    {
        m_Agent.SetDestination(TargetObj.transform.position);
        m_Agent.speed = ChasingSpeed;
        m_Agent.acceleration = 50f;

        //AnimSystem
        if(m_Animator.GetBool("Walk"))
        {
            m_Animator.SetBool("Walk", false);
        }
        m_Animator.SetBool("Run", true);
    }

    float AttackingClock;
    private void AttackingUpdate()
    {
        if (!inAttack)
        {
           
            m_Animator.SetTrigger("Attack");
            inAttack = true;
            AttackingClock = 2f;

        }
        else
        {
            if(AttackingClock > 0)
            {
                AttackingClock -= Time.deltaTime;
                gameObject.transform.LookAt(new Vector3(TargetObj.transform.position.x, transform.position.y, TargetObj.transform.position.z));
            }
            else
            {
                inAttack = false;
            }
        }

        if (AttackingClock < 1.3f && AttackingClock > 0.7f)
        {
            HitCollider.enabled = true;
        }
        else
        {
            HitCollider.enabled = false;
        }

        //AnimSystem
        if (m_Animator.GetBool("Walk"))
        {
            m_Animator.SetBool("Walk", false);
        }
        if (m_Animator.GetBool("Run"))
        {
            m_Animator.SetBool("Run", false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        if (started)
        {
            Gizmos.DrawWireSphere(initialPos, PatrolingRange);
        }
        else
        {
            Gizmos.DrawWireSphere(transform.position, PatrolingRange);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ChasingRange);
    }
}
