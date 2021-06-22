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
    private float _detectionDistanceGround;
    public float OffSetX = 0.5f;
    public float OffSetY = 0.5f;
    public LayerMask ground_Layer;
    public bool grounded = true;

    [Header("Movements")]
    public float maxSpeed = 20;
    public float ClampVelocityY;
    private float currentSpeed = 0;
    public bool FreezeInput = false;
    public bool ForceFreeze = false;
    private float freezeClock;
    [HideInInspector]
    public bool forceNoJump;
    [HideInInspector]
    public bool freezeDirection;


    [Header("DashValues")]
    public float DashSpeed = 1f;
    public float DashDuration = 1f;
    public float DashCouldown = 2f;
    public float DashIllusionCouldown = 4f;
    private float currDashCD;
    private float m_DashSpeed = 0f;
    [HideInInspector]
    public float clockDash = 0f;
    [HideInInspector]
    public float clockCanDash;
    private bool CanDash;
    private bool dashIsIllusion;
    public bool dashDebug;
    private int m_butterflyTypeSelectionIndex;
    public GameObject IllusionMeshItem;
    private Vector3 DashDir;

    [Header("Hunt")]
    public GameObject NetCollider;
    public GameObject NetVisualbox;
    private Renderer CatchVisualisationRenderer;

    [Header("KnockBack")]
    public bool inKnockBack;

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
        _detectionDistanceGround = DetectionDistanceGround;
        IllusionMeshItem.SetActive(false);
        Physics.IgnoreCollision(GetComponent<Collider>(), IllusionMeshItem.GetComponent<Collider>(), true);

        //NetVisualCollider
        CatchVisualisationRenderer = NetVisualbox.GetComponent<Renderer>();
        NetVisualbox.SetActive(false);

    }
    bool checkStepSound = false;
    void Update()
    {
        grounded = IsGrounded();

        //Inputs
        if (FreezeInput == false && ForceFreeze == false)
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
        if (grounded == false && m_animManager.wasGrounded == true && (jumpTime <= -0.8f || jumpTime > +.35f))
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
        DashHudUpdate();
        KnockBackUpdate();
        StairMovementUpdate();
        HuntNetHitUpdate();
        NetVisualBoxColliderUptade();
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
        if (Physics.Raycast(transform.position + new Vector3(OffSetX, 0, 0), Vector3.down, _detectionDistanceGround, ground_Layer))
        {
            groundRayCast = true;
        }
        if (Physics.Raycast(transform.position + new Vector3(-OffSetX, 0, 0), Vector3.down, _detectionDistanceGround, ground_Layer))
        {
            groundRayCast = true;
        }
        if (Physics.Raycast(transform.position + new Vector3(0, 0, OffSetY), Vector3.down, _detectionDistanceGround, ground_Layer))
        {
            groundRayCast = true;
        }
        if (Physics.Raycast(transform.position + new Vector3(0, 0, -OffSetY), Vector3.down, _detectionDistanceGround, ground_Layer))
        {
            groundRayCast = true;
        }

        return groundRayCast;
    }

    private void StairMovementUpdate()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position + new Vector3(OffSetX, 0, 0), Vector3.down, out hit, _detectionDistanceGround) || Physics.Raycast(transform.position + new Vector3(-OffSetX, 0, 0), Vector3.down, out hit, _detectionDistanceGround) || Physics.Raycast(transform.position + new Vector3(0, 0, OffSetY), Vector3.down, out hit, _detectionDistanceGround) || Physics.Raycast(transform.position + new Vector3(0, 0, -OffSetY), Vector3.down, out hit, _detectionDistanceGround))
        {
            if(hit.transform.tag == "Stair")
            {
                _detectionDistanceGround = 2f;
            }
        }
        else
        {
            _detectionDistanceGround = DetectionDistanceGround;
        }
    }

    public void FreezePosPlayer(float duration, bool CantJump = false, bool FreezeDirection = false)
    {
        freezeClock = duration;
        FreezeInput = true;
        forceNoJump = CantJump;
        freezeDirection = FreezeDirection;
    }

    bool checkPlaySound;
    public void InitDash(int DashType)
    {
        ButterflyBehaviourV2 currButterfly;
        currButterfly = ButterflyInventory.Instance.GetFirstButterfly(DashType);
        ButterflyInventory.Instance.ShootedButterfly(currButterfly);
        ButterflyInventory.Instance.SetButterflyToRecovery(currButterfly);
        FreezePosPlayer(DashDuration, true, true);
        AnimationManager.m_instance.dashTrigger = true;
        AnimationManager.m_instance.canPlayStepSound = false;
        AudioManager.instance.m_audioSource2.clip = AudioManager.instance.dashsSounds[DashType];
        AudioManager.instance.m_audioSource2.Play();

        checkPlaySound = true;
        DashDir = PlayerMesh.transform.forward;
        clockDash = DashDuration;
        m_DashSpeed = DashSpeed;
        CanDash = false;
        if (DashType != 1)
        {
            clockCanDash = DashCouldown;
            currDashCD = DashCouldown;
        }
        else
        {
            clockCanDash = DashIllusionCouldown;
            currDashCD = DashIllusionCouldown;
            dashIsIllusion = true;
            IllusionMeshItem.transform.position = transform.position + Vector3.up * 0.3f;
            IllusionMeshItem.transform.rotation = AnimationManager.m_instance.gameObject.transform.rotation;
            IllusionMeshItem.SetActive(true);
        }        
    }

    public void DashUpdate()
    {
        m_butterflyTypeSelectionIndex = ButterflyTypeSelection.Instance.SelectionTypeValue;

        if ((Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown("left shift")) && CanDash && ButterflyInventory.Instance.ButterflyInInventory[m_butterflyTypeSelectionIndex].Count > 0)
        {
            InitDash(m_butterflyTypeSelectionIndex);
        }

        //DashDuration
        if (clockDash > 0)
        {
            clockDash -= Time.deltaTime;
            m_rb.velocity = new Vector3(m_rb.velocity.x, 0.2f, m_rb.velocity.z);
        }
        else
        {
            m_DashSpeed = 0;
            m_rb.velocity = new Vector3(m_rb.velocity.x, m_rb.velocity.y, m_rb.velocity.z);
            if (checkPlaySound)
            {
                AnimationManager.m_instance.canPlayStepSound = true;
                checkPlaySound = false;
            }
        }

        //DashCouldownDuration
        if (clockCanDash > 0)
        {
            clockCanDash -= Time.deltaTime;
        }
        else
        {
            CanDash = true;
            if (dashIsIllusion)
            {
                IllusionMeshItem.SetActive(false);
            }
        }
    }

    public void DashHudUpdate()
    {
        if(ButterflyInventory.Instance.ButterflyInInventory[m_butterflyTypeSelectionIndex].Count == 0 || CanDash == false)
        {
            UIManager.instance.DashSprite.color = UIManager.instance.DashColors[3];
        }
        else
        {
            UIManager.instance.DashSprite.color = UIManager.instance.DashColors[m_butterflyTypeSelectionIndex];
        }

        UIManager.instance.DashCd.color = UIManager.instance.DashColors[m_butterflyTypeSelectionIndex];

        UIManager.instance.DashCd.fillAmount = clockCanDash / currDashCD;
    }

    public bool GetCanDash()
    {
        return CanDash;
    }

    private float clockBeforeNetHit;
    private float clockAfterNetHit;
    private bool canClockAfterNetHit = false;

    private float clockNetHitCd;

    private bool checkDrawHitbox;
    bool charged = false;
    public bool inHuntNetHit;
    private void HuntNetHitUpdate()
    {
        //Chasse Coup de Filet
        if (!Shoot.Instance.Aiming)
        {
            if (Input.GetAxis("Fire1") > 0.7f && checkDrawHitbox == false)
            {
                ShowNetVisualBoxCollider(true);
                checkDrawHitbox = true;
            }
            else if (Input.GetAxisRaw("Fire1") == 0f && checkDrawHitbox == true && charged)
            {
                inHuntNetHit = true;
                checkDrawHitbox = false;
                charged = false;

                clockBeforeNetHit = 0.3f;
                canClockAfterNetHit = true;
                clockNetHitCd = 1f;

                m_animManager.netTrigger = true;
                VFXManager.m_instance.ShowNet(true);
                AudioManager.instance.Play("Net");
                ShowNetVisualBoxCollider(false);
            }
            else if(Input.GetAxisRaw("Fire1") == 0f && checkDrawHitbox == true && !charged)
            {
                checkDrawHitbox = false;
                ShowNetVisualBoxCollider(false);
            }
        }
        else
        {
            checkDrawHitbox = false;
            ShowNetVisualBoxCollider(false);
        }


        //if (Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown("f") && !Shoot.Instance.Aiming && clockNetHitCd <= 0)
        //{
        //    clockBeforeNetHit = 0.3f;
        //    canClockAfterNetHit = true;
        //    clockNetHitCd = 1f;

        //    m_animManager.netTrigger = true;
        //    VFXManager.m_instance.ShowNet(true);
        //    AudioManager.instance.Play("Net");
        //}

        if (clockBeforeNetHit > 0)
        {
            
            clockBeforeNetHit -= Time.deltaTime;
        }
        else
        {
            if (canClockAfterNetHit)
            {
                inHuntNetHit = false;
                clockAfterNetHit = 0.3f;
                canClockAfterNetHit = false;
            }
        }

        if (clockAfterNetHit > 0)
        {
            NetCollider.SetActive(true);
            clockAfterNetHit -= Time.deltaTime;
        }
        else
        {
            if (NetCollider.activeSelf)
            {
                ButterflyInventory.Instance.CatchButterfly(NetCollider.GetComponent<NetCollider>().GetButterflyAndClearList());
                NetCollider.SetActive(false);
            }
 
        }

        if(clockNetHitCd > 0)
        {
            clockNetHitCd -= Time.deltaTime;
        }
    }

    float currErosionValue;
    float clockShowVisualBox = 0f;
    bool canClock;
    private void ShowNetVisualBoxCollider(bool value)
    {
        if (value)
        {
            NetVisualbox.SetActive(true);
            clockShowVisualBox = 1;
            canClock = true;
        }
        else
        {
            NetVisualbox.GetComponent<Collider>().enabled = false;
            NetVisualbox.SetActive(false);
            currErosionValue = 1f;
            canClock = false;
        }
    }

    private void NetVisualBoxColliderUptade()
    {
        if (canClock)
        {
            if (clockShowVisualBox > 0)
            {
                clockShowVisualBox -= Time.deltaTime;
                charged = false;
                NetVisualbox.GetComponent<Collider>().enabled = false;
            }
            else
            {
                charged = true;
                NetVisualbox.GetComponent<Collider>().enabled = true;
            }
        }

        currErosionValue = clockShowVisualBox;

        if(currErosionValue < 0)
        {
            currErosionValue = 0;
        }

        CatchVisualisationRenderer.material.SetFloat("_Erosion", currErosionValue);
    }

    public void InitKnockBack()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
        inKnockBack = true;
        AnimationManager.m_instance.canPlayStepSound = false;
    }

    private void KnockBackUpdate()
    {
        if (inKnockBack)
        {
            if (grounded)
            {
                AnimationManager.m_instance.canPlayStepSound = true;
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