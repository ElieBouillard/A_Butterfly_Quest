using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAiTest : MonoBehaviour
{
    public enum PlayerState { waiting, movement }
    public PlayerState currState;

    public float waitingTime;
    private float m_clock;

    public float Speed;
    public float MoveRangeMax;
    public float MoveRangeMin;

    public Vector3 m_InitialPosition;
    public Vector3 randomPosTarget;

    private void Awake()
    {
        currState = PlayerState.waiting;
        m_clock = waitingTime;
    }

    private void Start()
    {
        m_InitialPosition = transform.position;
    }

    private void Update()
    {
        if (currState == PlayerState.waiting)
        {
            if (m_clock > 0)
            {
                m_clock -= Time.deltaTime;
            }
            else if (m_clock <= 0)
            {
                randomPosTarget = GetRandomPosition();
                Debug.Log(randomPosTarget);
                currState = PlayerState.movement;
            }
        }

        else if (currState == PlayerState.movement)
        {
            Vector3 direction = randomPosTarget - transform.position;

            float distTargetToPlayer = direction.magnitude;
            float distPlayerToInitPos = Time.deltaTime * Speed;

            if (distPlayerToInitPos > distTargetToPlayer)
            {
                transform.position = randomPosTarget;
                m_clock = waitingTime;
                currState = PlayerState.waiting;
            }
            else
            {
                direction.Normalize();
                transform.position += Time.deltaTime * direction * Speed;
            }
        }
    }

    Vector3 GetRandomPosition()
    {
        Vector3 positionRandom = Random.insideUnitSphere;
        Vector3 position = new Vector3(positionRandom.x, 0, positionRandom.z);

        return position + m_InitialPosition  * MoveRangeMax;
    }
}


