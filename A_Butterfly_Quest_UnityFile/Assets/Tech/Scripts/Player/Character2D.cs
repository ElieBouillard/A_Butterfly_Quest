using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine.Events;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using UnityEngine.SceneManagement;

public class Character2D : MonoBehaviour
{
    public static Character2D m_instance;

    [Header("Physics")]
    private Rigidbody2D m_rb;
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
    bool isJumping;

    [Header("Inputs")]
    public float horizontalInput;
    public float verticalInput;

    public Animator animator;
    public GameObject cam;

    Vector2 rayDir;
    public bool grounded;

    void Awake()
    {
        if (TryGetComponent<Rigidbody2D>(out Rigidbody2D attached_rigidbody))
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
    

        //run
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Action_anim") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Death_anim"))
        {
            animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
          
        }

        // Jump

        if ((Input.GetKeyDown(KeyCode.Space) /*|| Input manette*/  && IsGrounded()))
        {
            jumpTime = 0;
            isJumping = true;   
        }

        if (jumpTime != -1 && jumpTime >= 0 && jumpTime < jumpCurve.keys[jumpCurve.length - 1].time)
        {
            jumpTime += Time.deltaTime;

        }
        else
        {
            jumpTime = -1;
            isJumping = false;
        }


        // acceleration 

        currentSpeed = Mathf.Lerp(0, maxSpeed, (accelerationCoefficient * accelerationCurve.Evaluate(accelerationTime)) + ((IsGrounded() == true) ? 0 : 1) * ((airControlBoost == true) ? 1 : 0));

        if (horizontalInput != 0 && accelerationTime == -1)
        {
            accelerationTime = 0;
        }
        if (accelerationTime != -1 && accelerationTime >= 0 && accelerationTime < accelerationCurve.keys[accelerationCurve.length - 1].time)
        {
            accelerationTime += Time.deltaTime;
        }
        else
        {
            if (horizontalInput == 0)
            {
                accelerationTime = -1;
            }
        }


    }
    void FixedUpdate()
    {



        if (gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > 25)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }

        //main velocity operation
        target_Velocity = new Vector3(horizontalInput * currentSpeed, 0, 0) + new Vector3(0, (m_rb.velocity.y + (-9.81f * GravityBoost)) * ((jumpTime != -1) ? 0 : 1), 0);



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

        RaycastHit2D hit0 = Physics2D.Raycast(transform.position, Vector2.down, DetectionDistanceGround, ground_Layer);
        return ((hit0.collider != null));
    }
}














