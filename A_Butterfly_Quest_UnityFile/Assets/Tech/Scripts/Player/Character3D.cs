﻿using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine.Events;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using UnityEngine.SceneManagement;

public class Character3D : MonoBehaviour
{
    public static Character3D m_instance;

    [Header("Physics")]

    [HideInInspector]
    public Rigidbody m_rb;
    Vector3 target_Velocity;
    Vector3 jumpDirection = Vector3.up;
    private GameObject PlayerMesh;

    [Header("Jump")]
    public float jumpTime;
    public AnimationCurve jumpCurve;
    public float frameVelocityCoefficient;
    [Range(0.01f, 0.2f)]
    public float frameAccuracy = 0.1f;
    public float jumpForce;
    public float GravityBoost;
    public float jumpDelay = 0.25f;
    public float jumpTimer = 0f;

    [Header("RaysGrounded")]
    public bool IsGroundedDebug;
    public float DetectionDistanceGround = 1;
    public float OffSetX = 0.5f;
    public float OffSetY = 0.5f;
    public LayerMask ground_Layer;
    public bool grounded = true;

    [Header("Movements")]
    public float maxSpeed = 20;
    public float ClampVelocityY;
    private float currentSpeed = 0;
    private bool FreezeInput = false;
    private float freezeClock;
    private bool forceNoJump;
    private bool freezeDirection;

    [Header("DashValues")]
    public float DashSpeed = 1f;
    public float DashDuration = 1f;
    public float DashIllusionDuration = 4f;
    private float m_DashSpeed = 0f;
    private float clockDash = 0f;
    private float clockCanDash = 0f;
    public bool canDash = true;
    public bool dashDebug;
    private int m_butterflyTypeSelectionIndex;
    public GameObject IllusionMeshItem;
    private Vector3 DashDir;

    [Header("KnockBack")]
    public bool inKnockBack;
    private float KnockBackClock;

    [Header("Inputs")]
    public GameObject m_camera;
    public float horizontalInput;
    public float verticalInput;
    private Vector2 inputDirection;

    [Header("MainCamera axis")]
    public bool debugCameraAxis = false;
    public Vector3 directionForward;
    public Vector3 directionRight;

    [Header("Animation Binding")]
    public AnimationManager m_animManager;


    void Awake()
    {
        if (TryGetComponent<Rigidbody>(out Rigidbody attached_rigidbody))
        {
            m_rb = attached_rigidbody;
        }
        m_instance = this;

        PlayerMesh = gameObject.transform.GetChild(0).gameObject;

    }
    private void Start()
    {
        m_animManager = AnimationManager.m_instance;
    }

    void Update()
    {
        grounded = IsGrounded();

        //Inputs
        if (FreezeInput == false)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");         
        }
        else
        {
            horizontalInput = 0;
            verticalInput = 0;
        }
        if (!freezeDirection)
        {
            inputDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }

        //Camera X & Z axis
        directionForward = new Vector3(m_camera.transform.forward.x, 0f, m_camera.transform.forward.z).normalized;
        directionRight = new Vector3(m_camera.transform.right.x, 0f, m_camera.transform.right.z).normalized;       

        if (debugCameraAxis)
        {
            Debug.DrawRay(transform.position, directionForward * 10, Color.blue);
            Debug.DrawRay(transform.position, directionRight * 10, Color.red);
        }

        // Jump
        if (Input.GetButtonDown("Jump"))
        {
            jumpTimer = Time.time + jumpDelay;
        }

        if(jumpTimer > Time.time && grounded && forceNoJump == false)
        {
            jumpTime = 0;

            m_animManager.jumpTrigger = true; //anim

            jumpTimer = 0;
        }

        if (jumpTime != -1 && jumpTime >= 0 && jumpTime < jumpCurve.keys[jumpCurve.length - 1].time)
        {
            jumpTime += Time.deltaTime;
        }
        else
        {
            jumpTime = -1;

            //m_animManager.jumpTrigger = false; //anim
        }

        if(grounded == false)
        {
            //Debug.Log("test");
            //m_animManager.jumpTrigger = false; //anim
        }

        //Vitesse de déplacement
        if (Shoot.Instance.Aiming == false)
        {
            currentSpeed = maxSpeed;
        }
        else
        {
            currentSpeed = maxSpeed/2;
        }
        

        //Animation Run
        //if (animator != null)
        //{
        //    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Action_anim") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Death_anim"))
        //    {
        //        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
        //    }
        //}


        //Animation Binding
        m_animManager.cameraForward = directionForward;
        m_animManager.cameraRight = directionRight;
        m_animManager.playerSpeed = Mathf.Max(Mathf.Abs(Input.GetAxis("Horizontal")), Mathf.Abs(Input.GetAxis("Vertical")));
        m_animManager.playerTargetDir = inputDirection;
        //m_animManager.airboneTrigger = grounded;
        if (grounded == false && m_animManager.wasGrounded == true && jumpTime <= -0.8f)
        {
            m_animManager.airboneTrigger = true;
            m_animManager.wasGrounded = false;
        }
        if(grounded && !m_animManager.wasGrounded)
        {
            m_animManager.wasGrounded = true;
        }      

        //FreezePlayer
        if (freezeClock > 0)
        {
            freezeClock -= Time.deltaTime;
        }
        else
        {
            FreezeInput = false;
            forceNoJump = false;
            freezeDirection = false;
        }

        DashUpdate();
        KnockBackUpdate();
    }

    void FixedUpdate()
    {
        //Main velocity operation
        target_Velocity = directionForward * currentSpeed * verticalInput + directionRight * currentSpeed * horizontalInput + new Vector3(0, (m_rb.velocity.y + (-9.81f * GravityBoost)) * ((jumpTime != -1) ? 0 : 1), 0) + DashDir * m_DashSpeed;

        //Jump velocity operation
        if (jumpTime != -1)
        {
            if ((jumpTime + frameAccuracy) < jumpCurve.keys[jumpCurve.length - 1].time)
            {
                frameVelocityCoefficient = jumpCurve.Evaluate(jumpTime + frameAccuracy) - jumpCurve.Evaluate(jumpTime);
            }

            target_Velocity += jumpDirection * (jumpForce * frameVelocityCoefficient);
        }

        //Assign final velocity
        if (!inKnockBack)
        {
            m_rb.velocity = target_Velocity;
        }

        //ClampY Velocity
        if (m_rb.velocity.y > ClampVelocityY && jumpTime == -1)
        {
            m_rb.velocity = new Vector3(m_rb.velocity.x, ClampVelocityY, m_rb.velocity.z);
        }
    }

    public bool IsGrounded()
    {
        bool groundRayCast = false;
        if (Physics.Raycast(transform.position + new Vector3(OffSetX, 0, 0), Vector3.down, DetectionDistanceGround, ground_Layer))
        {
            groundRayCast = true;
        }
        if (Physics.Raycast(transform.position + new Vector3(-OffSetX, 0, 0), Vector3.down, DetectionDistanceGround, ground_Layer))
        {
            groundRayCast = true;
        }
        if (Physics.Raycast(transform.position + new Vector3(0, 0, OffSetY), Vector3.down, DetectionDistanceGround, ground_Layer))
        {
            groundRayCast = true;
        }
        if (Physics.Raycast(transform.position + new Vector3(0, 0, -OffSetY), Vector3.down, DetectionDistanceGround, ground_Layer))
        {
            groundRayCast = true;
        }


        return groundRayCast;
    }

    public void FreezePosPlayer(float duration, bool CantJump = false, bool FreezeDirection = false)
    {
        freezeClock = duration;
        FreezeInput = true;
        forceNoJump = CantJump;
        freezeDirection = FreezeDirection;
    }

    public void InitDash(float dashSpeed, float dashDuration, float canDashDuration, bool illusionDash)
    {
        FreezePosPlayer(dashDuration, true, true);
        AnimationManager.m_instance.dashTrigger = true;
        DashDir = PlayerMesh.transform.forward;
        clockDash = dashDuration;
        clockCanDash = canDashDuration;
        m_DashSpeed = dashSpeed;
        canDash = false;
        if (illusionDash)
        {
            illusionMeshTemp = Instantiate(IllusionMeshItem, transform.position, Quaternion.identity);
            Physics.IgnoreCollision(GetComponent<Collider>(), illusionMeshTemp.GetComponent<Collider>(), true);
        }
    }

    GameObject illusionMeshTemp;

    public void DashUpdate()
    {
        m_butterflyTypeSelectionIndex = ButterflyTypeSelection.Instance.SelectionTypeValue;

        if ((Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown("left shift")) && canDash)
        {
            if(m_butterflyTypeSelectionIndex != 1)
            {
                InitDash(DashSpeed, DashDuration, DashDuration, false);
            }
            else
            {
                InitDash(DashSpeed, DashDuration, DashIllusionDuration, true);
            }
        }

        if(clockDash > 0)
        {
            clockDash -= Time.deltaTime;
        }
        else
        {
            m_DashSpeed = 0;
        }
        

        if (clockCanDash > 0)
        {
            clockCanDash -= Time.deltaTime;
        }
        else
        {
            canDash = true;
            if(illusionMeshTemp != null)
            {
                Destroy(illusionMeshTemp);
            }
        }
    }

    public void InitKnockBack()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
        inKnockBack = true;
    }

    private void KnockBackUpdate()
    {
        if (inKnockBack)
        {
            if (grounded)
            {
                inKnockBack = false;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (IsGroundedDebug)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position + new Vector3(OffSetX, 0, 0), transform.TransformDirection(Vector3.down * DetectionDistanceGround));
            Gizmos.DrawRay(transform.position + new Vector3(-OffSetX, 0, 0), transform.TransformDirection(Vector3.down * DetectionDistanceGround));
            Gizmos.DrawRay(transform.position + new Vector3(0, 0, OffSetY), transform.TransformDirection(Vector3.down * DetectionDistanceGround));
            Gizmos.DrawRay(transform.position + new Vector3(0, 0, -OffSetY), transform.TransformDirection(Vector3.down * DetectionDistanceGround));
        }
    }
}