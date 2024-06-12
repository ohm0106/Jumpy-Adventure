using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    CharacterController controller;
    Animator anim;
    Transform cam;

    [Header("[ �Ӽ� �� ���� ���� ���� ]")]
    [SerializeField]
    [Range(0.0f, 10.0f)]
    float speed = 1.5f;
    float curSpeed;
    int hp = 100;

    [SerializeField]
    float jumpPower = 1.0f;

    [SerializeField]
    PlayerActionType curActionType;

    [Header("[ �Է� �� �̵� ���� ���� ]")]
    [SerializeField]
    Vector2 inputVec;
    Vector3 moveVec;

    [SerializeField]
    bool isInteracting;
    [SerializeField]
    bool isJump;
    [SerializeField]
    bool isJumping;
    float verticalVelocity;

    [Header("[ ground ]")]
    [SerializeField]
    float groundCheckDistance = 1.0f;
    [SerializeField]
    LayerMask groundLayer;

    [Header("[ Interact ������Ʈ ���̾ƿ� ]")]
    PlayerActionInterface nearestInteractObj = null;

    void Awake()
    {
        curSpeed = speed;
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        cam = Camera.main.transform;
    }

    void Update()
    {
        moveVec = cam.right * inputVec.x + cam.forward * inputVec.y;
        moveVec.y = 0;

        if (moveVec.magnitude > 0)
        {
            Quaternion dirQuat = Quaternion.LookRotation(moveVec);
            Quaternion nextQuat = Quaternion.Slerp(transform.rotation, dirQuat, 0.3f);
            transform.rotation = nextQuat;

          /*  if (!CanMoveForward(moveVec) && !isJumping)
            {
                moveVec = Vector3.zero;
            }*/
        }

        // ���� ó��
        if (controller.isGrounded && isJump && !isJumping)
        {
            isJumping = true;
            verticalVelocity = jumpPower;
        }


    }

    void FixedUpdate()
    {
        if (controller.isGrounded)
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
        }

        moveVec.y = verticalVelocity;
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

    void Interact(InteractionObjectType t) // TODO : ��ȣ�ۿ� ������Ʈ type ���� ���� �� ��. 
    {
        switch (t)
        {
            
        }

    }

    void Dead()
    {
        // �÷��̾ �״� ���� 
        // hp = 0 �϶� 
    }


    public void Damage(int num)
    {
        hp -= num;
        // TODO : ������ �׼�

        if (hp <= 0)
            Dead();
    }

    #region ���ǵ� ���� �Լ�
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

    #region �Է� �̺�Ʈ �Լ� 
    public void ActionMove(InputAction.CallbackContext context)
    {
        inputVec = context.ReadValue<Vector2>();
    }

    public void ActionInteractive(InputAction.CallbackContext context)
    {
        if (context.canceled && nearestInteractObj != null)
        {
            isInteracting = true;
            Interact(nearestInteractObj.SetPlayerInteraction());
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
