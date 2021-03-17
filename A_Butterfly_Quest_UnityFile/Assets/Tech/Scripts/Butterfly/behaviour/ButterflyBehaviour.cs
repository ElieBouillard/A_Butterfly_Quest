using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyBehaviour : MonoBehaviour
{
    Vector3 offsetInCluster;
    public ButterflyCluster parentCluster;
    public Vector3 direction;
    public float speed;
    private Vector3 selfDirection = Vector3.forward;
    private float selfSpeed = 5;


    void Start()
    {
        FindOffsetInCluster();
    }

    void Update()
    {
        if (parentCluster != null)
        {
            direction = parentCluster.clusterDirection;
            speed = parentCluster.clusterSpeed;
            transform.position = Vector3.Lerp(transform.position, parentCluster.transform.position + offsetInCluster, Time.deltaTime * Random.Range(0.2f,1));
        }
        else
        {
            direction = selfDirection;
            speed = selfSpeed;
            MoveForward();
        }
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
}
