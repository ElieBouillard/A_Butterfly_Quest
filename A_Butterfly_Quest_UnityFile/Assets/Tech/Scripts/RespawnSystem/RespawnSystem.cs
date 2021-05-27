using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnSystem : MonoBehaviour
{
    public static RespawnSystem instance;

    [Header("Parametres")]
    public float DeathAnimTime;

    private GameObject Player;

    [HideInInspector]
    public Vector3 currRespawnCoord;
    [HideInInspector]
    public Quaternion currRespawnOrientation;

    private Image BlackScreenDeath;
    private float deathClock;
    private bool canDeathClock;
    private bool canRespawn;
    private float blackScreenClock;
    private int deathCount;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Player = Character3D.m_instance.gameObject;
        BlackScreenDeath = UIManager.instance.BlackScreenDeath;
        BlackScreenDeath.color = new Color(0, 0, 0, 0);
        currRespawnCoord = gameObject.transform.position;
        currRespawnOrientation = gameObject.transform.rotation;
    }

    private void Update()
    {
        DeathUpdate();
    }

    public void Death()
    {
        Character3D.m_instance.FreezePosPlayer(DeathAnimTime + 1.5f, true, true);
        deathClock = DeathAnimTime;
        canDeathClock = true;
    }

    public void DeathUpdate()
    {
        if (canDeathClock)
        {
            if (deathClock > 0)
            {
                deathClock -= Time.deltaTime;
            }
            else
            {
                blackScreenClock = 1.5f;
                canDeathClock = false;
            }
        }       

        if(blackScreenClock > 0)
        {
            blackScreenClock -= Time.deltaTime;
            BlackScreenDeath.gameObject.GetComponent<Animator>().SetBool("Opace", true);
            canRespawn = true;
        }
        else
        {
            if (canRespawn)
            {
                Respawn();
                canRespawn = false;
                BlackScreenDeath.gameObject.GetComponent<Animator>().SetBool("Opace", false);
            }
        }
    }

    public void Respawn()
    {
        deathCount++;
        Player.transform.position = currRespawnCoord;
        Player.gameObject.transform.GetChild(0).gameObject.transform.rotation = currRespawnOrientation;
        Player.GetComponent<HealthSystem>().Respawn();
        Shoot.Instance.ResetFreeLookBehindPlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<RespawnTrigger>())
        {
            RespawnTrigger currRespawnTrigger = other.GetComponent<RespawnTrigger>();
            currRespawnCoord = currRespawnTrigger.GetCoorRespawnPoint();
            currRespawnOrientation = currRespawnTrigger.GetOrientationRespawnPoint();
            Debug.Log("-RESPAWNSYSTEM- Save at " + currRespawnCoord);
        }
    }
}
