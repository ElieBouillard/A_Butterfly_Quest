using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIv2 : MonoBehaviour
{
    //Etat
    public enum States {Patroling, Chasing, Attacking, TurnBack, KnockBack, Off}
    public States m_State;

    [Header("References")]
    public GameObject Target;
    public LayerMask PlayerMask;
    public LayerMask _obstacleMask;

    [Header("Ranges")]
    [Range(0, 20)]
    public float PatrolingRange;
    [Range(0, 20)]
    public float ChasingRange;
    [Range(0, 20)]
    public float AttackingRange;
    [Range(0, 5)]
    public float AttackingCancelRange;

    [Header("Statistiques")]
    public float Damage;
    public float PatrolingSpeed;
    public float ChasingSpeed;
    public float AttackingSpeed;
    public float TurnBackSpeed;
    public float Acceleration;

    [Header("PatrolingValues")]
    public float WaitingTime;
    public Vector3? RandomPatrolingPos;
    private Vector3 DistanceToPatrolingPos;
    private float WaitingPatrolingClock;

    [Header("AttackingValue")]
    public float TimeAfterAttacking;
    public Vector3? AttackingPos;
    public bool inAttack;

    [Header("TurnBackValue")]
    public float TimeAfterTurnBack;
    public Vector3? TurnBackPos1;
    public Vector3? TurnBackPos2;

    [Header("Animation")]
    public Animator m_animator;

    [Header("Debug")]
    public float minDistStop;
    public bool useRange;
    private NavMeshAgent Agent;
    private bool _started;
    private Vector3 InitialPos;
    private float AttackingRangeTemp;
    private bool canResetPath;

    private GameObject IllusionMeshPrefab;

    private void Start()
    {
        //ReferenceAuto
        Agent = gameObject.GetComponent<NavMeshAgent>();

        InitialPos = transform.position;
        _started = true;
        Target = Character3D.m_instance.gameObject;
        AttackingRangeTemp = AttackingRange;
        Agent.acceleration = Acceleration;

        m_animator = transform.GetChild(0).gameObject.GetComponent<Animator>();
        IllusionMeshPrefab = Character3D.m_instance.IllusionMeshItem;
        Physics.IgnoreCollision(GetComponent<Collider>(), IllusionMeshPrefab.GetComponent<Collider>(), true);
    }

    bool check = true;
    private void Update()
    {
        if (m_State != States.TurnBack && m_State != States.Off)
        {
            useRange = true;
        }

        if(m_State != States.Attacking)
        {
            Agent.acceleration = Acceleration;
        }
        else
        {
            Agent.acceleration = 200;
        }

        RangeSystem();
        StateSystem();

        if (IllusionMeshPrefab.activeSelf)
        {
            if (!check)
            {
                Target = IllusionMeshPrefab;
                check = true;
            }
  
        }
        else
        {
            if (check)
            {
                Target = Character3D.m_instance.gameObject;
                check = false;
            }

        }
    }

    private void RangeSystem()
    {

        bool playerInAttackRange = Physics.CheckSphere(transform.position, AttackingRangeTemp, PlayerMask);
        bool playerInChasingRange = Physics.CheckSphere(transform.position, ChasingRange, PlayerMask);


        if (useRange)
        {
            if (!playerInAttackRange && !playerInChasingRange && !inAttack)
            {
                m_State = States.Patroling;
            }
            if (!playerInAttackRange && playerInChasingRange && !inAttack)
            {
                m_State = States.Chasing;
            }
            if (playerInAttackRange && playerInChasingRange)
            {
                AttackingRangeTemp = AttackingRange + AttackingCancelRange;
                m_State = States.Attacking;
            }
            else
            {
                AttackingRangeTemp = AttackingRange;
            }
        }      
    }

    private void StateSystem()
    {

        if(m_State == States.Patroling)
        {
            Patroling();
            if (Agent.isStopped)
            {
                Agent.isStopped = false;
            }
        }
        if(m_State == States.Chasing)
        {
            Chasing();
            SetAllAnimBool(false);
            m_animator.SetBool("Run", true);
            if (Agent.isStopped)
            {
                Agent.isStopped = false;
            }
        }
        if(m_State == States.Attacking)
        {
            SetAllAnimBool(false);
            //m_animator.SetBool("Attack", true);
            Attacking();
        }
        if(m_State == States.TurnBack)
        {
            TurnBack();
        }
        if(m_State == States.Off)
        {
            Off();
            if(nextState == States.Attacking)
            {
               
            }
            else
            {
                SetAllAnimBool(false);
            }
        }
    }

    private void Patroling()
    {
        Agent.speed = PatrolingSpeed;

        if (RandomPatrolingPos == null)
        {
            if (WaitingPatrolingClock <= 0)
            {
                SearchPatrolingPos();

                //Anim Start Walk
                SetAllAnimBool(false);
                m_animator.SetBool("Walk", true);
            }
            else if (WaitingPatrolingClock > 0)
            {
                WaitingPatrolingClock -= Time.deltaTime;
            }
        }
        else
        {
            Agent.SetDestination(RandomPatrolingPos.Value);
            Debug.DrawRay(RandomPatrolingPos.Value, Vector3.up, Color.yellow, 3);

            DistanceToPatrolingPos = RandomPatrolingPos.Value - transform.position;

            if (DistanceToPatrolingPos.magnitude < 1f)
            {
                WaitForNewPatrolingPos();

                //Anim start Idle
                m_animator.SetBool("Walk", false);
            }
        }
    }

    private void SearchPatrolingPos()
    {
        float randomX = Random.Range(-PatrolingRange, PatrolingRange);
        float randomZ = Random.Range(-PatrolingRange, PatrolingRange);

        Vector3 randPos = new Vector3(InitialPos.x + randomX, transform.position.y, InitialPos.z + randomZ);
        NavMeshPath path = new NavMeshPath();
        Agent.CalculatePath(randPos, path);

        if((randPos - InitialPos).magnitude < PatrolingRange)
        {
            if (path.status == NavMeshPathStatus.PathComplete)
            {
                RandomPatrolingPos = randPos;
            }
            else
            {
                RandomPatrolingPos = null;
            }
        }
        else
        {
            RandomPatrolingPos = null;
        }
    }

    private void WaitForNewPatrolingPos()
    {
        RandomPatrolingPos = null;
        WaitingPatrolingClock = WaitingTime;
    }

    bool AttackAfterChasing;
    private void Chasing()
    {
        Agent.speed = ChasingSpeed;
        Agent.SetDestination(Target.transform.position);

        canResetPath = true;
        AttackingPos = null;
        AttackAfterChasing = true;
    }

    float clockAttackAnim;

    bool canFxCharge;
    private void Attacking()
    {
        if (AttackAfterChasing){
            Agent.isStopped = true;
            TurnOff(TimeAfterTurnBack, States.Attacking);
            AttackAfterChasing = false;
        }
        else
        {
            if (Agent.isStopped)
            {
                Agent.isStopped = false;
            }

            if (!canResetPath)
            {
                Agent.ResetPath();
                canResetPath = false;
            }

            Vector3 dirAttack;
            float distAttack;
            if (AttackingPos == null)
            {
                Vector3 Targetpos = new Vector3(Target.transform.position.x, transform.position.y, Target.transform.position.z);
                dirAttack = Targetpos - transform.position;
                distAttack = dirAttack.magnitude;
                dirAttack.Normalize();

                Vector3 AttackingPosTemp;
                //if (Physics.Raycast(transform.position, dirAttack, out RaycastHit hit, AttackingRange * 3f, _obstacleMask))
                //{
                //    Debug.DrawRay(transform.position, dirAttack * AttackingRange * 3f, Color.cyan, 2f);
                //    AttackingPosTemp = transform.position + (hit.distance - 1.5f) * dirAttack;
                //    Debug.DrawRay(transform.position, dirAttack * hit.distance, Color.red, 3);
                //    minDistStop = 3f;
                //}
                //else
                //{
                //    float distAttackTemp = distAttack;
                //    AttackingPosTemp = transform.position + distAttackTemp * 2 * dirAttack;
                //    minDistStop = 0.5f;
                //}
                
                float distAttackTemp = distAttack;
                AttackingPosTemp = transform.position + distAttackTemp * 2f * dirAttack;
                minDistStop = 1f;

                if(Physics.Raycast(transform.position, dirAttack, distAttackTemp * 2.5f, _obstacleMask))
                {
                    AttackingPos = Target.transform.position;
                }
                else
                {
                    AttackingPos = AttackingPosTemp;
                }               

                inAttack = true;
                clockAttackAnim = 0.4f;
                canFxCharge = true;
            }
            else
            {
                float distToAttackPos;
                if (canFxCharge)
                {
                    //FX CHARGE
                    canFxCharge = false;
                }
                
                m_animator.SetBool("Attack", true);

                if (clockAttackAnim > 0)
                {
                    clockAttackAnim -= Time.deltaTime;
                    distToAttackPos = Mathf.Infinity;
                }
                else
                {
                    Agent.speed = AttackingSpeed;
                    Agent.SetDestination(AttackingPos.Value);
                    Debug.DrawRay(AttackingPos.Value, Vector3.up, Color.cyan, 3);

                }
                    distToAttackPos = (AttackingPos.Value - transform.position).magnitude;

                if (distToAttackPos < 2f)
                {
                    inAttack = false;
                    TurnOff(TimeAfterAttacking, States.TurnBack);
                    AttackingPos = null;
                }
            }                    
        }
    }

    bool reachPos1;
    bool reachPos2;

    private void TurnBack()
    {
        m_animator.SetBool("Walk", true);
        useRange = false;
        Agent.speed = TurnBackSpeed;
        if(TurnBackPos1 == null)
        {
            Vector3 dir = Target.transform.position - transform.position;
            float dist = dir.magnitude;
            dir.Normalize();

            TurnBackPos2 = transform.position + 1 * dir;
            Debug.DrawRay(TurnBackPos2.Value, Vector3.up, Color.magenta, 10);
            dir = Quaternion.Euler(0, 60, 0) * dir;
            TurnBackPos1 = transform.position + 0.5f * dir;
            Debug.DrawRay(TurnBackPos1.Value, Vector3.up, Color.magenta, 10);
            reachPos1 = false;
            reachPos2 = false;
        }
        else
        {
            if (!reachPos1)
            {
                Agent.SetDestination(TurnBackPos1.Value);
                float dist1 = Vector3.Distance(transform.position, TurnBackPos1.Value);
                if(dist1 < 1f)
                {
                    reachPos1 = true;
                }
            }
            else if(!reachPos2)
            {
                Agent.SetDestination(TurnBackPos2.Value);
                float dist2 = Vector3.Distance(transform.position, TurnBackPos2.Value);
                if(dist2 < 1f)
                {
                    reachPos2 = true;
                }
            }
            else if (reachPos1 && reachPos2)
            {
                m_animator.SetBool("Walk", false);
                TurnOff(TimeAfterTurnBack, States.Attacking);
                reachPos1 = false;
                reachPos2 = false;
                TurnBackPos1 = null;
                TurnBackPos2 = null;
            }
        }
    }

    public float clockOff;
    States nextState;

    public void TurnOff(float time, States state)
    {
        m_State = States.Off;
        clockOff = time;
        nextState = state;
    }

    private void Off()
    {
        useRange = false;
        if(clockOff > 0)
        {
            clockOff -= Time.deltaTime;    
            //if(nextState == States.Attacking)
            //{
            //    transform.LookAt(new Vector3(Target.transform.position.x, transform.position.y, Target.transform.position.z));
            //}
        }
        else if(clockOff <=0)
        {
            m_State = nextState;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Character3D>())
        {
            HealthSystem m_healthSystem = other.gameObject.GetComponent<HealthSystem>();
            m_healthSystem.TakeDamage(Damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, ChasingRange);
        Gizmos.color = Color.red;
        if(_started == false)
        {
            Gizmos.DrawWireSphere(transform.position, AttackingRange);
            Gizmos.DrawWireSphere(transform.position, AttackingRange + AttackingCancelRange);
        }
        else
        {
            Gizmos.DrawWireSphere(transform.position, AttackingRangeTemp);
        }

        Gizmos.color = Color.yellow;
        if (_started == false)
        {
            Gizmos.DrawWireSphere(transform.position, PatrolingRange);
        }
        else
        {
            Gizmos.DrawWireSphere(InitialPos, PatrolingRange);
        }
    }

    #region animation
    private void SetAllAnimBool(bool value)
    {
        m_animator.SetBool("Walk", value);
        m_animator.SetBool("Attack", value);
        m_animator.SetBool("Attack2", value);
        m_animator.SetBool("Run", value);
    }
    #endregion
}
