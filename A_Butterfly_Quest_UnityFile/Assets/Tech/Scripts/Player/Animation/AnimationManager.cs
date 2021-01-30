using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationManager : MonoBehaviour
{

    private Animator m_anim;

    private Vector3 cameraForward;
    private Vector3 cameraRight;

    private float playerSpeed;
    private Vector2 playerTargetDir;
    private Vector2 playerCurDir;
    [SerializeField]
    private float playerDirOffset;

    private bool playerFocused;
    private bool jumpTrigger;

    private float cur_playerSpeed;


    [SerializeField]
    private float inputLerpSpeed = 1.0f;

    public float debugValue;

    private Transform m_parentMesh;

    //Torso Bending
    [SerializeField]
    private float curTorsoBend = 0.0f;
    [SerializeField]
    private float targetTorsoBend = 0.0f;
    [SerializeField]
    private float TorsoBendMultiplier = 1f;
    [SerializeField]
    private float torsoMaxRange = 0.4f;
    [SerializeField]
    private float minSpeedForBending = .3f;
    private bool canTorsoImpulse = false;
    private bool TorsoImpulseTrigger = false;
    private bool reorientateTrigger = false;

    private void Awake()
    {
        m_anim = GetComponent<Animator>();
        m_parentMesh = this.transform;
    }

    void Start()
    {
        cameraForward = Vector3.forward;
        cameraRight = Vector3.right;
    }


    void Update()
    {
        ////Debug area
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        playerSpeed = Mathf.Max(Mathf.Abs(horizontal), Mathf.Abs(vertical));
        playerTargetDir = new Vector2(horizontal, vertical);
        if (Input.GetKeyDown(KeyCode.Joystick1Button1)) 
        {
            playerFocused = !playerFocused;
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            jumpTrigger = true;
        }
        ////

        //cur_playerSpeed = Mathf.Lerp(cur_playerSpeed, playerSpeed, Time.deltaTime * inputLerpSpeed);

        

      
        Vector3 inputPos = cameraForward * playerTargetDir.y + cameraRight * playerTargetDir.x;
        Vector3 meshPos = m_parentMesh.transform.forward;
        Vector3 offset = inputPos.normalized - meshPos.normalized;
        //DEBUG
        Debug.DrawRay(transform.position + Vector3.up * 2, offset, Color.magenta);
        Debug.DrawRay(transform.position + Vector3.up * 2, m_parentMesh.transform.right, Color.green);
        Debug.DrawRay(transform.position, cameraForward * playerTargetDir.y + cameraRight * playerTargetDir.x, Color.cyan);
        Debug.DrawRay(transform.position, m_parentMesh.transform.forward, Color.red);



        if (playerSpeed > minSpeedForBending && playerFocused == false)
        {
            //calculate playerDirOFFSET
            playerDirOffset = Vector3.Dot(offset, m_parentMesh.transform.right);

            if (Mathf.Abs(playerDirOffset) <= torsoMaxRange)
            {
                HandleTorsoBend();
                if (canTorsoImpulse)
                {
                    TorsoImpulseTrigger = true;
                    canTorsoImpulse = false;
                }
            }
            else
            {
                HandleReorientation();
            }
        }
        else
        {
            playerDirOffset = 0.0f;
            targetTorsoBend = playerDirOffset;

            if (!canTorsoImpulse)
            {
                canTorsoImpulse = true;
            }
            
        }

        if(playerSpeed >= 0.01f)
        {
            FacePlayerInput();
        }

        curTorsoBend = Mathf.Lerp(curTorsoBend, targetTorsoBend, Time.deltaTime * 20);
        //if(Mathf.Abs(curTorsoBend) <= 0.1f)
        //{
        //    curTorsoBend = 0.0f;
        //}

        HandleAnimatorBindings(); 
    }

    void FacePlayerInput()
    {
        if(playerFocused == false)
        {
            //mesh forward lerp towards new dir
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(playerTargetDir.x,0,playerTargetDir.y));
            m_parentMesh.rotation = Quaternion.Slerp(m_parentMesh.rotation, targetRotation, Time.deltaTime * 10);
        }
        else
        {
            //follow camera forward
        }
    }

    void HandleTorsoBend()
    {
        targetTorsoBend = playerDirOffset * TorsoBendMultiplier;
    }

    void HandleReorientation()
    {
        reorientateTrigger = true;
        Debug.DrawRay(transform.position, Vector3.up * 5, Color.black);
        //speed à 0
    }

    void HandleAnimatorBindings()
    {
        m_anim.SetFloat("Speed", playerSpeed);
        m_anim.SetFloat("DirX", playerTargetDir.x);
        m_anim.SetFloat("DirY", playerTargetDir.y);
        m_anim.SetBool("Focused", playerFocused);
        if (jumpTrigger)
        {
            m_anim.SetTrigger("Jump");
            jumpTrigger = false;
        }

        m_anim.SetFloat("TorsoBend", curTorsoBend);
        if (TorsoImpulseTrigger)
        {
            m_anim.SetTrigger("TorsoImpulse");
            TorsoImpulseTrigger = false;
        }

        if (reorientateTrigger)
        {
            m_anim.SetTrigger("ReorientatePlayer");
            reorientateTrigger = false;
        }

    
    }
}
