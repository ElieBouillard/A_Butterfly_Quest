using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyCluster : MonoBehaviour
{
    public Vector3 clusterDirection;
    private Transform playerMesh;
    private Vector3 positionBehindPlayer;
    public float clusterSpeed;
    [SerializeField]
    float currentSpeed;
    float targetSpeed;
    public bool followPlayer = false;
    Transform player;
    float randomoffset;

    void Start()
    {
        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            playerMesh = AnimationManager.m_instance.transform;
        }
        if (followPlayer)
        {
            clusterSpeed = 8;
        }
        randomoffset = Random.Range(0, 100);
    }


    void Update()
    {
        if (followPlayer)
        {
            if(player != null)
            {
                positionBehindPlayer = player.transform.position - (playerMesh.forward * 1f);
                clusterDirection = (positionBehindPlayer- transform.position).normalized;
                //clusterDirection = Vector3.Lerp(clusterDirection, ((positionBehindPlayer) - transform.position).normalized, Time.deltaTime * 2);
                float distanceToPos = Vector3.Distance(transform.position, positionBehindPlayer);
                currentSpeed = Mathf.Lerp(0, clusterSpeed, Mathf.Clamp01(distanceToPos/2));
                MoveTowardsPlayer();
            }
        }
        else
        {
            clusterDirection = new Vector3((Mathf.PerlinNoise(Time.time/5 + randomoffset, Time.time/5 + randomoffset) -.5f) * 2,0, (Mathf.PerlinNoise(Time.time/5 + randomoffset, Time.time/5 + randomoffset) - .5f) * 2);
            //clusterDirection = transform.forward + transform.right * Mathf.PerlinNoise(Time.time, Time.time)*2;
            currentSpeed = clusterSpeed;
            MoveForward();
        }

        //currentSpeed = Mathf.Lerp(0, clusterSpeed, Time.deltaTime*2);
    }

    void MoveTowardsPlayer()
    {

        if (Vector3.Distance(transform.position, positionBehindPlayer) > 0.05f)
        {
            transform.position += (clusterDirection * currentSpeed * Time.deltaTime);
        }
        //transform.position = positionBehindPlayer;
        //transform.position = Vector3.Lerp(transform.position, positionBehindPlayer, Time.deltaTime * 2);
        
    }

    void MoveForward()
    {
        transform.position += (clusterDirection * currentSpeed * Time.deltaTime);
    }  
}
