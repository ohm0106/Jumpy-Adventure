using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    [Range(0.0f, 10.0f)]
    float speed = 1.5f;

    CharacterController controller;
    Animator anim;
    Transform cam;

    [SerializeField]
    Vector2 inputVec;
    Vector3 moveVec;

    [SerializeField]
    bool isInteractive;

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
        Debug.Log("controller.isGrounded : " + controller.isGrounded);
        if (controller.isGrounded)
        {
            moveVec = cam.right * inputVec.x + cam.forward * inputVec.y;
            moveVec.y = 0;

            if (moveVec.magnitude > 0)
            {
                Quaternion dirQuat = Quaternion.LookRotation(moveVec);
                Quaternion nextQuat = Quaternion.Slerp(transform.rotation, dirQuat, 0.3f);
                transform.rotation = nextQuat;

                // 앞쪽에 그라운드가 있는지 확인
                if (!CanMoveForward(moveVec))
                {
                    moveVec = Vector3.zero; // 그라운드가 없으면 이동 멈춤
                }
            }
        }
    }

    void FixedUpdate()
    {
        controller.SimpleMove(moveVec * speed * Time.deltaTime * 50);
    }

    public void ActionMove(InputAction.CallbackContext context)
    {
        inputVec = context.ReadValue<Vector2>();
    }

    public void ActionInteractive(InputAction.CallbackContext context)
    {
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