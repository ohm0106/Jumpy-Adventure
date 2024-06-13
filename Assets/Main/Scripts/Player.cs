using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using DG.Tweening;

public class Player : MonoBehaviour
{
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
    PlayerActionInterface nearestInteractObj = null;

    [Header("[ 기본 넉백 설정 ]")]
    [SerializeField]
    float knockbackForce = 5.0f;
    [SerializeField]
    float knockbackDuration = 0.5f;

    private bool isKnockback;
    private float knockbackEndTime;
    private Vector3 knockbackDirection;

    void Awake()
    {
        curSpeed = speed;
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        cam = Camera.main.transform;
    }

    void Update()
    {
        if (isKnockback)
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

    public void SetInteract(PlayerActionInterface playerActionInterface)
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

    void Dead()
    {
        // 플레이어가 죽는 순간 
        anim.SetTrigger("doDeath");
    }

    public void Damage(int num)
    {
        hp -= num;
        // TODO : 데미지 액션

        if (hp <= 0)
        {
            hp = 0;
            Dead();
        }
        else
        {
            anim.SetTrigger("doDamage");
        }
    }

    void ApplyKnockback(Vector3 direction, float knockbackF, float delay)
    {
        knockbackForce = knockbackF;
        knockbackDuration = delay;
        isKnockback = true;
        knockbackEndTime = Time.time + knockbackDuration;
        knockbackDirection = direction.normalized;
    }

    void ApplyKnockback(Vector3 direction)
    {
        isKnockback = true;
        knockbackEndTime = Time.time + knockbackDuration;
        knockbackDirection = direction.normalized;
    }

    #region 스피드 관련 함수
    public void SlowSpeed(float degree)
    {
        curSpeed -= degree;
    }

    public void FastSpeed(float degree)
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
        if (other.CompareTag("Spider Web"))
        {
            SlowSpeed(3f);
        }
        else if (other.CompareTag("Water"))
        {
            Damage(100);
        }
        else if (other.CompareTag("DamageTrab"))
        {
            Vector3 knockbackDirection = (transform.position - other.transform.position).normalized;
            ApplyKnockback(knockbackDirection);
            Damage(5);
        }else if (other.CompareTag("Ladder"))
        {
            isClimbing = true;
            Quaternion dirQuat = Quaternion.LookRotation(other.transform.position);
            transform.rotation = dirQuat;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Spider Web"))
        {
            ResetSpeed();
        }else if (other.CompareTag("Ladder"))
        {
            isClimbing = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag =="BulletA")
        {
            Vector3 knockbackDirection = (transform.position - collision.transform.position).normalized;
            ApplyKnockback(knockbackDirection);
            Damage(20);
        }
    }
    #endregion

    #region 입력 이벤트 함수 
    public void ActionMove(InputAction.CallbackContext context)
    {
        inputVec = context.ReadValue<Vector2>();

        if (inputVec.x == 0 && inputVec.y == 0)
            anim.SetBool("isWalk", false);
        else
            anim.SetBool("isWalk", true);
    }

    public void ActionInteractive(InputAction.CallbackContext context)
    {
        if (context.canceled && nearestInteractObj != null)
        {
            isInteracting = true;
            Interact(nearestInteractObj.SetPlayerInteraction());
            Debug.Log("CLICK");
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
