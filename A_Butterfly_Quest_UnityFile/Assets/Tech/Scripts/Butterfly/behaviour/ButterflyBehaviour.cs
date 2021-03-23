using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButterflyBehaviour : MonoBehaviour
{
    Vector3 offsetInCluster;
    public ButterflyCluster parentCluster;
    public Vector3 direction;
    public Vector3 targetPosition;
    public float speed;
    private Vector3 selfDirection = Vector3.forward;
    private float selfSpeed = 5;
    private float noiseForce = 0.1f;
    private Vector3 noiseOffset;
    private float randomSpeedModifier;
    private float steeringSpeed = 3;

    void Start()
    {
        FindOffsetInCluster();
        randomSpeedModifier = Random.Range(.8f, 1.2f);
    }

    void Update()
    {
        NoiseMovement();
        if (parentCluster != null)
        {
            if(Vector3.Distance(transform.position,targetPosition) > 0.1f)
            {
                //direction = parentCluster.clusterDirection;
                speed = 6;
                //speed = selfSpeed;
                direction = Vector3.Lerp(direction, (targetPosition - transform.position).normalized + (noiseOffset.normalized * noiseForce), Time.deltaTime * steeringSpeed);
                transform.forward = direction.normalized;
                transform.position += direction * speed * Time.deltaTime;
            }
           
        }
        else
        {
            direction = selfDirection;
            speed = selfSpeed;
            MoveForward();
        }

        CalculateNextPosition();
    }

    void CalculateNextPosition()
    {
        targetPosition = parentCluster.transform.position + offsetInCluster /*+ noiseOffset*/;
    }

    void AssignNewCluster(ButterflyCluster newCluster)
    {
        parentCluster = newCluster;
        if (newCluster == null)
        {

        }
        else
        {
            FindOffsetInCluster();
        }
    }

    void MoveForward()
    {
        transform.position += (direction * speed * Time.deltaTime);
    }

    void FindOffsetInCluster()
    {
        //find new offset
        offsetInCluster = new Vector3(Random.Range(-.5f, .5f), Random.Range(-.5f, .5f), Random.Range(-.5f, .5f));
    }

    void NoiseMovement()
    {
        noiseOffset = new Vector3(Mathf.PerlinNoise(0, Time.time) * 2f - 1f, Mathf.PerlinNoise(0, Time.time) * 1f - 1f, Mathf.PerlinNoise(0, Time.time)) * noiseForce;
    }
}
