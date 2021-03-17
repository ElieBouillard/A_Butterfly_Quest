using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyCluster : MonoBehaviour
{
    public Vector3 clusterDirection;
    public float clusterSpeed;
    public bool followPlayer = false;
    Transform player;


    void Start()
    {
        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }


    void Update()
    {
        if (followPlayer)
        {
            if(player != null)
            {
                clusterDirection = (player.transform.position) - transform.position;
                clusterSpeed = 5;
                MoveTowardsPlayer();
            }
        }
    }

    void MoveTowardsPlayer()
    {
        if(Vector3.Distance(transform.position,player.transform.position) > 1)
        {
            transform.position += (clusterDirection * clusterSpeed * Time.deltaTime);
        }
        
    }
}
