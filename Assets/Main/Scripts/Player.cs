using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("[ 스텟 설정 ]")]
    [SerializeField]
    [Range(0.0f, 10.0f)]
    float speed = 1.5f;

    [SerializeField]
    float jumpPower = 1.0f;

    CharacterController controller;
    Animator anim;
    Transform cam;
    [Header("[ 확인용 ]")]
    [SerializeField]
    Vector2 inputVec;
    Vector3 moveVec;

    [SerializeField]
    bool isInteractive;
    [SerializeField]
    bool isJump;
    [SerializeField]
    bool isJumping;
    float verticalVelocity;

    [Header("[ 앞 레이어 RayCast ]")]
    [SerializeField]
    float groundCheckDistance = 1.0f;
    [SerializeField]
    LayerMask groundLayer;


    void Awake()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        cam = Camera.main.transform;
    }

    void Update()
    {
        if (controller.isGrounded)
        {
            moveVec = cam.right * inputVec.x + cam.forward * inputVec.y;
            moveVec.y = 0;

            if (moveVec.magnitude > 0)
            {
                Quaternion dirQuat = Quaternion.LookRotation(moveVec);
                Quaternion nextQuat = Quaternion.Slerp(transform.rotation, dirQuat, 0.3f);
                transform.rotation = nextQuat;

                if (!CanMoveForward(moveVec))
                {
                    moveVec = Vector3.zero;
                }
            }

            //Player Jump
            if (isJump && !isJumping)
            {
                isJumping = true;
                verticalVelocity = jumpPower;
            }
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        moveVec.y = verticalVelocity;
        controller.Move(moveVec * speed * Time.deltaTime);

        if (controller.isGrounded && verticalVelocity < 0)
        {
            isJumping = false;
            verticalVelocity = 0;
        }
    }

    public void ActionMove(InputAction.CallbackContext context)
    {
        inputVec = context.ReadValue<Vector2>();
    }

    public void ActionInteractive(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isInteractive = true;
        }
        else if (context.canceled)
        {
            isInteractive = false;
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
}