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
    int maxhp = 100;

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
    bool isTeleport;
    [SerializeField]
    bool isJump;
    [SerializeField]
    bool isJumping;
    [SerializeField]
    bool isClimbing;
    [SerializeField]
    bool isMove;

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

    [Header("[기본 공격 설정]")]
    [SerializeField]
    Weapon weapon;
    float attackRange = 2f; // 공격 범위
    float attackAngle = 60f; // 공격 범위 내 각도
    float attackCooldown = 0.5f; // 공격 쿨다운 시간
    float lastAttackTime;
    bool isAttack;

    [SerializeField]
    LayerMask enemyLayer;

    void Awake()
    {
        curSpeed = speed;
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        cam = Camera.main.transform;
        eventController = GetComponent<PlayerEvent>();
        isMove = true;
    }

    void OnEnable()
    {
        eventController.OnMovePlayer += SetMove;
        eventController.OnTeleportPlayer += SetTeleporting;
    }
    void OnDisable()
    {
        eventController.OnMovePlayer -= SetMove;
        eventController.OnTeleportPlayer -= SetTeleporting;
    }
    void Update()
    {

        if (isMove == false)
            return;

        if (isKnockback)
            return;

        if (hp == 0)
            return;

        if (isAttack)
            return;

        if (Input.GetMouseButtonDown(0) && Time.time >= lastAttackTime + attackCooldown)
        {
            StartCoroutine(PerformAttack());
            lastAttackTime = Time.time;
            return;
        }

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
            isJump = false;
        }
    }

    void FixedUpdate()
    {
        if (isMove == false)
            return;

        if (isAttack)
            return;

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

    void SetMove(bool isMove)
    {
        this.isMove = isMove;
    }
    void SetTeleporting(bool isMove)
    {
        this.isMove = isMove;
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
/*
        switch (t)
        {
            
        }*/
    }

    #region 데미지 관련 ( 넉백, 데미지, 데스 ) 
   
    void Dead()
    {
        // 플레이어가 죽는 순간 
        anim.SetTrigger("doDeath");
        eventController.SetPlayerDead();
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
        eventController.GetCurrentPlayerHP(hp, maxhp);
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

        IWeaponTrigger weaponTrigger = other.GetComponent<IWeaponTrigger>();
        if (weaponTrigger != null && weapon!= weaponTrigger.GetThis())
        {
            weaponTrigger.OnObjectEnter( transform,(Vector3 v) => { ApplyKnockback(v);}, (int i) => { Damage(i); });
        }

    }
    private void OnTriggerExit(Collider other)
    {
        IPlayerTrigger interactable = other.GetComponent<IPlayerTrigger>();
        if (interactable != null)
        {
            interactable.OnPlayerExit(this);
        }

        IWeaponTrigger weaponTrigger = other.GetComponent<IWeaponTrigger>();
        if (weaponTrigger != null && weapon != weaponTrigger.GetThis())
        {
            weaponTrigger.OnObjectExit();
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
        else if(isMove)
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
            anim.SetTrigger("doBehavior");
        }    
    }

    public void ActionJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isJump = true;
        }
    }
    #endregion

    #region 공격 
    IEnumerator PerformAttack()
    {
        isAttack = true;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 targetPoint = hit.point;
            Vector3 direction = (targetPoint - transform.position).normalized;

            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = lookRotation;

            anim.SetTrigger("doAttack");
            weapon.Use();
        }

        yield return new WaitForSeconds(0.8f);
        weapon.StopUse();
        isAttack = false;
    }

    #endregion
}
