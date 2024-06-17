using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using DG.Tweening;
using System;

public class Player : MonoBehaviour
{
    PlayerEvent eventController;
    CharacterController controller;
    Animator anim;
    Transform cam;

    [Header("[ 속성 및 상태 변수 설정 ]")]
    [SerializeField]
    [Range(0.0f, 10.0f)]
    float speed = 1.5f;
    float curSpeed;
    int hp = 100;

    [SerializeField]
    float jumpPower = 1.0f;

    [SerializeField]
    PlayerActionType curActionType;

    [SerializeField]
    private float climbSpeed = 3.0f;

    [SerializeField]
    bool isInvulnerablilty = false;

    [SerializeField]
    float invulnerabliltyDelay = 0.8f;


    [Header("[ 입력 및 이동 관련 여부 ]")]
    [SerializeField]
    Vector2 inputVec;
    Vector3 moveVec;

    [SerializeField]
    bool isInteracting;
    [SerializeField]
    bool isJump;
    [SerializeField]
    bool isJumping;
    [SerializeField]
    bool isClimbing;

    float verticalVelocity;

    [Header("[ ground ]")]
    [SerializeField]
    float groundCheckDistance = 1.0f;
    [SerializeField]
    LayerMask groundLayer;

    [Header("[ Interact 오브젝트 레이아웃 ]")]
    IPlayerAction nearestInteractObj = null;

    [Header("[ 기본 넉백 설정 ]")]
    [SerializeField]
    float knockbackForce = 5.0f;
    [SerializeField]
    float knockbackDuration = 0.5f;

    private bool isKnockback;
    private float knockbackEndTime;
    private Vector3 knockbackDirection;

    // Player Action 

    void Awake()
    {
        curSpeed = speed;
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        cam = Camera.main.transform;
        eventController = GetComponent<PlayerEvent>();

    }

    void Update()
    {
        if (isKnockback)
            return;

        if (hp == 0)
            return;

        if (isClimbing)
        {
            float horizontal = inputVec.x;
            float vertical = inputVec.y;

            moveVec = cam.right * horizontal;
            moveVec.y = vertical;

        }
        else
        {
            moveVec = cam.right * inputVec.x + cam.forward * inputVec.y;
            moveVec.y = 0;

            if (moveVec.magnitude > 0)
            {
                Quaternion dirQuat = Quaternion.LookRotation(moveVec);
                Quaternion nextQuat = Quaternion.Slerp(transform.rotation, dirQuat, 0.3f);
                transform.rotation = nextQuat;
            }
        }

        
        // 점프 처리
        if (controller.isGrounded && isJump && !isJumping)
        {
            isJumping = true;
            verticalVelocity = jumpPower;
            anim.SetTrigger("doJump");
        }
    }

    void FixedUpdate()
    {
        // 넉백
        if (isKnockback)
        {
            if (Time.time > knockbackEndTime)
            {
                isKnockback = false;
                moveVec = Vector3.zero;
            }
            else
            {
                moveVec = knockbackDirection * knockbackForce;
            }
        }

        // 중력
        if (controller.isGrounded || isClimbing)
        {
            if (verticalVelocity < 0)
            {
                verticalVelocity = 0;
                isJumping = false;
            }
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.fixedDeltaTime;
            moveVec.y = verticalVelocity;
        }

      
        controller.Move(moveVec * curSpeed * Time.fixedDeltaTime);
    }

    bool CanMoveForward(Vector3 moveDirection)
    {
        Vector3 origin = transform.position + controller.center;
        Vector3 direction = new Vector3(moveDirection.x, -groundCheckDistance, moveDirection.z).normalized;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, groundCheckDistance, groundLayer))
        {
            return true;
        }

        return false;
    }

    public void SetInteract(IPlayerAction playerActionInterface)
    {
        nearestInteractObj = playerActionInterface;
    }

    void Interact(InteractionObjectType t)
    {
        nearestInteractObj.ClickPerformAction();

        switch (t)
        {
            
        }
    }

    #region 데미지 관련 ( 넉백, 데미지, 데스 ) 
   
    void Dead()
    {
        // 플레이어가 죽는 순간 
        anim.SetTrigger("doDeath");
    }

    public void Damage(int num)
    {
        if (isInvulnerablilty)
            return;

        hp -= num;
        // TODO : 데미지 액션

        if (hp <= 0)
        {
            hp = 0;
            Dead();
        }
        else
        {
            SetInvulerablility();
            anim.SetTrigger("doDamage");
        }
    }

    public void ApplyKnockback(Vector3 direction, float knockbackF, float delay)
    {
        if (isInvulnerablilty)
            return;

        //Debug.DrawRay(transform.position, direction * 10, Color.red, 2.0f);

        knockbackForce = knockbackF;
        knockbackDuration = delay;
        isKnockback = true;
        knockbackEndTime = Time.time + knockbackDuration;
        knockbackDirection = direction.normalized;
    }

    public void ApplyKnockback(Vector3 direction)
    {
        if (isInvulnerablilty)
            return;

        //Debug.DrawRay(transform.position, direction * 10, Color.red, 2.0f);

        isKnockback = true;
        knockbackEndTime = Time.time + knockbackDuration;
        knockbackDirection = direction.normalized;
    }

    void SetInvulerablility()
    {
        if (!isInvulnerablilty)
            CoSetInvulerablility();
    }

    IEnumerator CoSetInvulerablility()
    {
        isInvulnerablilty = true;
        yield return new WaitForSeconds(invulnerabliltyDelay);
        isInvulnerablilty = false;
    }

    public void OnFire()
    {
        if(!isInvulnerablilty)
            StartCoroutine(CoFire());
    }

    IEnumerator CoFire()
    {
        isJump = true;
        isInvulnerablilty = true;
        yield return new WaitForSeconds(0.02f);
        isJump = false;
        yield return new WaitForSeconds(invulnerabliltyDelay);
      
        isInvulnerablilty = false;
    }

    #endregion

    #region 스피드 관련 함수

    public void SlowSpeed(float degree)
    {
        curSpeed -= degree;
    }

    void FastSpeed(float degree)
    {
        curSpeed += degree;
    }

    public void ResetSpeed()
    {
        curSpeed = speed;
    }
    #endregion

    #region Trigger 이벤트 
    private void OnTriggerEnter(Collider other)
    {
        IPlayerTrigger interactable = other.GetComponent<IPlayerTrigger>();
        if (interactable != null)
        {
            interactable.OnPlayerEnter(this);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        IPlayerTrigger interactable = other.GetComponent<IPlayerTrigger>();
        if (interactable != null)
        {
            interactable.OnPlayerExit(this);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        IPlayerTrigger interactable = collision.transform.GetComponent<IPlayerTrigger>();
        if (interactable != null)
        {
            interactable.OnPlayerEnter(this);
        }

    }

    #endregion

    #region 사다리 타기 
    public void SetClimbing(Vector3 pos)
    {
        isClimbing = true;
        Quaternion dirQuat = Quaternion.LookRotation(pos);
        transform.rotation = dirQuat;
    }

    public void ReleseClimbing()
    {
        isClimbing = false;
    }

    #endregion

    #region 입력 이벤트 함수 
    public void ActionMove(InputAction.CallbackContext context)
    {
        inputVec = context.ReadValue<Vector2>();

        if (inputVec.x == 0 && inputVec.y == 0)
        {
            anim.SetBool("isWalk", false);
            
        }
        else
        {
            anim.SetBool("isWalk", true);
            eventController.StartEffect(EffectType.WALK);
            //eventController.StopEffect(EffectType.WALK);
        }
            
    }

    public void ActionInteractive(InputAction.CallbackContext context)
    {
        if (context.canceled && nearestInteractObj != null)
        {
            isInteracting = true;
            Interact(nearestInteractObj.SetPlayerInteraction());
            Debug.Log("CLICK");
            anim.SetTrigger("doInteraction");
        }    
    }

    public void ActionJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isJump = true;
        }
        else if (context.canceled)
        {
            isJump = false;
        }
    }
    #endregion

    public PlayerActionType GetActionType()
    {
        return curActionType;
    }
}
