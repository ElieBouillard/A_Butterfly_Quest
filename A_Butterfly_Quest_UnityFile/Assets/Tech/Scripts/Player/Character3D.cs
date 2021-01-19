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
    Vector3 current_Velocity;
    Vector3 jumpDirection = Vector3.up;
    float velocityLerp;
    [Range(0, 3)]
    public float velocityDamping;
    public bool airControlBoost;

    Vector3 startJumpPosition;
    public float jumpTime;
    public AnimationCurve jumpCurve;
    public float frameVelocityCoefficient;
    [Range(0.01f, 0.2f)]
    public float frameAccuracy = 0.1f;

    [Header("Acceleration")]
    public AnimationCurve accelerationCurve;
    public float currentSpeed = 0;
    public float maxSpeed = 20;
    public float accelerationCoefficient = 1;
    public float accelerationTime;

    [Header("Layers")]
    public LayerMask ground_Layer;
    public LayerMask wall_Layer;

    [Header("Movements")]
    public float movementSpeed;
    public float jumpForce;
    public float GravityBoost;
    public float DetectionDistance = 1;
    public float DetectionDistanceGround = 1;

    [Header("Inputs")]
    public float horizontalInput;
    public float verticalInput;

    public Animator animator;
    public GameObject m_camera;

    public bool debug = false;

    public Vector3 directionForward;
    public Vector3 directionRight;


    void Awake()
    {
        if (TryGetComponent<Rigidbody>(out Rigidbody attached_rigidbody))
        {
            m_rb = attached_rigidbody;
        }
        m_instance = this;
    }


    void Start()
    {
      
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        directionForward = new Vector3(m_camera.transform.forward.x, 0f, m_camera.transform.forward.z).normalized;
        directionRight = new Vector3(m_camera.transform.right.x, 0f, m_camera.transform.right.z).normalized;


        if (debug)
        {
            Debug.DrawRay(transform.position, directionForward * 10, Color.blue);
            Debug.DrawRay(transform.position, directionRight * 10, Color.red);
        }

        //run
        if (animator != null)
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Action_anim") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Death_anim"))
            {
                animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
            }
        }
        

        // Jump
        if ((Input.GetKeyDown(KeyCode.Space) /*|| Input manette*/  && IsGrounded()))
        {
            jumpTime = 0;   
        }

        if (jumpTime != -1 && jumpTime >= 0 && jumpTime < jumpCurve.keys[jumpCurve.length - 1].time)
        {
            jumpTime += Time.deltaTime;
        }
        else
        {
            jumpTime = -1;
        }

        // acceleration 
        //currentSpeed = Mathf.Lerp(0, maxSpeed, (accelerationCoefficient * accelerationCurve.Evaluate(accelerationTime)) + ((IsGrounded() == true) ? 0 : 1) * ((airControlBoost == true) ? 1 : 0));

        //if (horizontalInput != 0 && accelerationTime == -1)
        //{
        //    accelerationTime = 0;
        //}
        //if (accelerationTime != -1 && accelerationTime >= 0 && accelerationTime < accelerationCurve.keys[accelerationCurve.length - 1].time)
        //{
        //    accelerationTime += Time.deltaTime;
        //}
        //else
        //{
        //    if (horizontalInput == 0)
        //    {
        //        accelerationTime = -1;
        //    }
        //}

        currentSpeed = maxSpeed;
    }

    void FixedUpdate()
    {
        if (gameObject.GetComponent<Rigidbody>().velocity.magnitude > 25)
        {
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        //main velocity operation
        target_Velocity = directionForward * currentSpeed * verticalInput + directionRight * currentSpeed * horizontalInput+ new Vector3(0, (m_rb.velocity.y + (-9.81f * GravityBoost)) * ((jumpTime != -1) ? 0 : 1), 0);

        if (jumpTime != -1)
        {
            if ((jumpTime + frameAccuracy) < jumpCurve.keys[jumpCurve.length - 1].time)
            {
                frameVelocityCoefficient = jumpCurve.Evaluate(jumpTime + frameAccuracy) - jumpCurve.Evaluate(jumpTime);
            }

            target_Velocity += jumpDirection * (jumpForce * frameVelocityCoefficient);
        }

        //assign final velocity
        m_rb.velocity = target_Velocity;
    }

    public bool IsGrounded()
    {
        bool groundRayCast = Physics.Raycast(transform.position, Vector3.down, DetectionDistanceGround, ground_Layer);
        Debug.Log(groundRayCast);
        return groundRayCast;
    }
}