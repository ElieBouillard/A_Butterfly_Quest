using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationManager : MonoBehaviour
{
    public bool DEBUG = false;

    //Singleton
    public static AnimationManager m_instance;

    //Components
    private Animator m_anim;
    private Transform m_parentMesh;

    //Inputs
    public Vector3 cameraForward;
    public Vector3 cameraRight;
    public float playerSpeed;
    public Vector2 playerTargetDir;
    private float playerDirOffset;
    public bool playerFocused;
    public bool jumpTrigger;
    public bool isGrounded;

    //Freeze rotations
    private bool freezeRotations;


    //Torso Bending
    private float curTorsoBend = 0.0f;
    private float targetTorsoBend = 0.0f;
    [SerializeField]
    private float TorsoBendMultiplier = 1f;
    [SerializeField]
    private float torsoMaxRange = 0.45f;
    [SerializeField]
    private float minSpeedForBending = .3f;

    //Torso Impulse
    private bool canTorsoImpulse = false;
    private bool TorsoImpulseTrigger = false;

    //Reorientation
    private bool reorientateTrigger = false;
    private float reorientateTimer;
    [SerializeField]
    private float reorientateDuration = .3f;
    [SerializeField]
    private float reorientateSpeed = 300.0f;
    private Vector3 reorientateDirection;
    private float reorientateCooldown = 0.0f;
    private float rotationDirection = 1f;
    private float negativeReorientationTreshold = -0.5f;

    //Shooting
    private float shootAnim_RandomIndex = 0.0f;
    public bool shootTrigger;

    //Shouting
    private bool shoutTrigger;

    //Dashing
    private bool dashTrigger;

    private void Awake()
    {
        m_instance = this;
        m_anim = GetComponent<Animator>();
        m_parentMesh = this.transform;
    }

    void Start()
    {
        if (DEBUG)
        {
            cameraForward = Vector3.forward;
            cameraRight = Vector3.right;
            reorientateTimer = -1.0f;
        }
        
    }


    void Update()
    {

        Vector3 inputPos = cameraForward * playerTargetDir.y + cameraRight * playerTargetDir.x;
        Vector3 meshPos = m_parentMesh.transform.forward;
        Vector3 offset = inputPos.normalized - meshPos.normalized;
        playerDirOffset = Vector3.Dot(offset, m_parentMesh.transform.right);

        //Check for freezes
        if (!m_anim.GetCurrentAnimatorStateInfo(1).IsName("None") /* || other conditions for freeze*/)
        {
            if (!freezeRotations)
            {
                freezeRotations = true;
            }
           
        }
        else
        {
            if (freezeRotations)
            {
                freezeRotations = false;
            }
        }


        if (playerFocused == false && playerSpeed > 0.01f && !freezeRotations)
        {

            float OrientationDir = Vector3.Dot(inputPos.normalized, meshPos.normalized);
            if (OrientationDir >= torsoMaxRange) //cone range
            {
                if(playerSpeed > minSpeedForBending)
                {
                    HandleTorsoBend();
                    if (canTorsoImpulse)
                    {
                        TorsoImpulseTrigger = true;
                        canTorsoImpulse = false;
                    }
                }
               
                
            }
            else
            {
                //If offset is too big, start reorientating 
                if (reorientateTimer <= -0.8f && reorientateCooldown <= -.8f)
                {
                    reorientateDirection = inputPos;
                    reorientateTrigger = true;
                    //speed à 0

                    if (Vector3.Dot(reorientateDirection.normalized, m_parentMesh.transform.forward.normalized) >= negativeReorientationTreshold && Mathf.Sign(playerDirOffset) < 0)
                    {
                        rotationDirection = -1f;
                    }
                    else
                    {
                        rotationDirection = 1f;
                    }

                    reorientateTimer = 0;

                    
                }

            }

            ////bypass walk anim with big changes
            //if ()
            //{

            //}
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

        //Reorientate pause timer
        if(reorientateTimer >= -.1f)
        {
            HandleReorientation();
            reorientateTimer += Time.deltaTime;
            if(reorientateTimer >= reorientateDuration)
            {
                reorientateCooldown = 0f;
                reorientateTimer = -1f;          
            }
            targetTorsoBend = 0;
        }

        //wait before being able to reorientate
        if(reorientateCooldown >= -.1f)
        {
            reorientateCooldown += Time.deltaTime;
            if(reorientateCooldown >= .5f)
            {
                reorientateCooldown = -1f;
            }
        }

        //Basic Input facing with a lerp
        if (playerSpeed >= 0.01f && reorientateTimer <= -0.8f)
        {
            if (!freezeRotations) //Check if we're doing an overriding action
            {
                FacePlayerInput();
            }
            
        }

        //Update torso bend
        curTorsoBend = Mathf.Lerp(curTorsoBend, targetTorsoBend, Time.deltaTime * 20);

        //Interface with Animator component
        HandleAnimatorBindings();



        // ******** DEBUG *********

        if (DEBUG)
        {
            ////Debug area**********
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            playerSpeed = Mathf.Max(Mathf.Abs(horizontal), Mathf.Abs(vertical));
            playerTargetDir = new Vector2(horizontal, vertical);
            if (Input.GetKeyDown(KeyCode.Joystick1Button4))
            {
                playerFocused = !playerFocused;
            }
            if (Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                jumpTrigger = true;
            }
            if (Input.GetKeyDown(KeyCode.Joystick1Button5))
            {
                shootTrigger = true;
            }
            ////************
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            shoutTrigger = true;
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            dashTrigger = true;
        }

        //Debug.DrawRay(transform.position + Vector3.up * 2, offset, Color.magenta);
        //Debug.DrawRay(transform.position + Vector3.up * 2, m_parentMesh.transform.right, Color.green);
        Debug.DrawRay(transform.position, inputPos, Color.cyan);
        Debug.DrawRay(transform.position, cameraForward * playerTargetDir.y + cameraRight * playerTargetDir.x, Color.cyan);
        Debug.DrawRay(transform.position, m_parentMesh.transform.forward, Color.red);
        //Debug.DrawRay(transform.position, m_parentMesh.transform.right - m_parentMesh.transform.forward, Color.red);
        Debug.DrawRay(transform.position, m_parentMesh.transform.forward * (1- torsoMaxRange*2) + m_parentMesh.transform.right * torsoMaxRange*2, Color.red);
        Debug.DrawRay(transform.position, m_parentMesh.transform.forward * (1- torsoMaxRange*2) + -m_parentMesh.transform.right * torsoMaxRange*2, Color.red);
    }

    void FacePlayerInput()
    {
        if(playerFocused == false)
        {
            //mesh forward lerp towards new dir
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(playerTargetDir.x, 0, playerTargetDir.y));
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
        float offsetFromTarget = Vector3.Dot(reorientateDirection.normalized, m_parentMesh.transform.forward.normalized);
        if (offsetFromTarget <= 0.95f)
        {
            m_parentMesh.localEulerAngles = new Vector3(m_parentMesh.localEulerAngles.x, m_parentMesh.localEulerAngles.y + (reorientateSpeed * rotationDirection * Time.deltaTime) / reorientateDuration, m_parentMesh.localEulerAngles.z);            
        }
        else
        {       
            reorientateTimer = -1f;
            reorientateCooldown = 0f;
        }

    }

    void HandleAnimatorBindings()
    {
        m_anim.SetFloat("Speed", playerSpeed);
        m_anim.SetFloat("DirX", playerTargetDir.x);
        m_anim.SetFloat("DirY", playerTargetDir.y);
        m_anim.SetBool("Focused", playerFocused);
        m_anim.SetBool("Grounded",isGrounded);
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

        if (shootTrigger)
        {
            m_anim.SetFloat("RandomShoot", Random.Range(0, 2));
            m_anim.SetTrigger("Shoot");
            shootTrigger = false;
        }

        if (shoutTrigger)
        {
            m_anim.SetTrigger("Shout");
            shoutTrigger = false;
        }

        if (dashTrigger)
        {
            m_anim.SetTrigger("Dash");
            dashTrigger = false;
        }
    
    }
}
