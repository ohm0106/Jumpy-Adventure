using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform player;

    [SerializeField]
    Vector3 offset;

    [SerializeField]
    float smoothSpeed = 0.125f;

    void Start()
    {
        player = FindAnyObjectByType<Player>().gameObject.transform; // Todo
        transform.position = player.position + offset;
    }

    void OnEnable()
    {
        FindAnyObjectByType<PlayerEvent>().OnTeleportPlayer += SetCameraMovePos; 
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(player);
    }

    void SetCameraMovePos(bool isMove)
    {
        Vector3 desiredPosition = player.position + new Vector3();
        transform.position = desiredPosition;
        transform.LookAt(player);
    }
}