using System.Collections;
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
    private Rigidbody m_rb;
    Vector3 target_Velocity;
    Vector3 jumpDirection = Vector3.up;

    [Header("Jump")]
    public float jumpTime;
    public AnimationCurve jumpCurve;
    public float frameVelocityCoefficient;
    [Range(0.01f, 0.2f)]
    public float frameAccuracy = 0.1f;
    public float jumpForce;
    public float GravityBoost;
    public float DetectionDistanceGround = 1;

    [Header("Layers")]
    public LayerMask ground_Layer;

    [Header("Movements")]
    public float maxSpeed = 20;
    private float currentSpeed = 0;
    private bool FreezeInput = false;
    private float freezeClock;
    private bool forceNoJump;


    [Header("Inputs")]
    public float horizontalInput;
    public float verticalInput;
    //public Animator animator;
    public GameObject m_camera;


    [Header("MainCamera axis")]
    public Vector3 directionForward;
    public Vector3 directionRight;

    [Header("Animation Binding")]
    public AnimationManager m_animManager;

    public bool debug = false;

    void Awake()
    {
        if (TryGetComponent<Rigidbody>(out Rigidbody attached_rigidbody))
        {
            m_rb = attached_rigidbody;
        }
        m_instance = this;
    }
    private void Start()
    {
        m_animManager = AnimationManager.m_instance;
    }

    void Update()
    {
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
        
        //Camera X & Z axis
        directionForward = new Vector3(m_camera.transform.forward.x, 0f, m_camera.transform.forward.z).normalized;
        directionRight = new Vector3(m_camera.transform.right.x, 0f, m_camera.transform.right.z).normalized;       

        if (debug)
        {
            Debug.DrawRay(transform.position, directionForward * 10, Color.blue);
            Debug.DrawRay(transform.position, directionRight * 10, Color.red);
        }

        // Jump
        if (Input.GetAxis("Jump") > 0 && IsGrounded() && forceNoJump == false)
        {
            jumpTime = 0;

            m_animManager.jumpTrigger = true; //anim
            Debug.Log("test");
        }
        if (jumpTime != -1 && jumpTime >= 0 && jumpTime < jumpCurve.keys[jumpCurve.length - 1].time)
        {
            jumpTime += Time.deltaTime;
        }
        else
        {
            jumpTime = -1;

            m_animManager.jumpTrigger = false; //anim
        }

        if((Input.GetAxis("Jump") <= 0) || IsGrounded() == false)
        {
            m_animManager.jumpTrigger = false; //anim
        }

        //Vitesse de déplacement
        currentSpeed = maxSpeed;

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
        m_animManager.playerTargetDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

       
        m_animManager.isGrounded = IsGrounded();


        //test freePosPlayer
        if (Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            FreezePosPlayer(1f);
        } 

        //FreezePlayer
        if(freezeClock > 0)
        {
            freezeClock -= Time.deltaTime;
        }
        else
        {
            FreezeInput = false;
            forceNoJump = false;
        }
    }

    void FixedUpdate()
    {
        if (gameObject.GetComponent<Rigidbody>().velocity.magnitude > 25)
        {
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        //Main velocity operation
        target_Velocity = directionForward * currentSpeed * verticalInput + directionRight * currentSpeed * horizontalInput+ new Vector3(0, (m_rb.velocity.y + (-9.81f * GravityBoost)) * ((jumpTime != -1) ? 0 : 1), 0);

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
        m_rb.velocity = target_Velocity;
    }

    public bool IsGrounded()
    {
        bool groundRayCast = Physics.Raycast(transform.position, Vector3.down, DetectionDistanceGround, ground_Layer);
        return groundRayCast;
    }

    public void FreezePosPlayer(float value, bool cantJump = false)
    {
        freezeClock = value;
        FreezeInput = true;
        forceNoJump = cantJump;
        Debug.Log("Freezing");
    }
}