using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    Waypoints waypoints;
    [SerializeField]
    float speed = 2.0f;
    int currentPointIndex = 0;

    bool isMove;

    void Start()
    {
        isMove = true;
        if (waypoints == null || waypoints.points.Length == 0)
        {
            Debug.LogError("Waypoints are not set or empty!");
            return;
        }

        transform.position = waypoints.points[currentPointIndex].position;
    }

    void Update()
    {
        if (waypoints == null || waypoints.points.Length == 0)
        {
            return;
        }
        if (!isMove)
            return;

        MoveTowardsWaypoint();
    }

    void MoveTowardsWaypoint()
    {
        Transform targetPoint = waypoints.points[currentPointIndex];
        Vector3 direction = (targetPoint.position - transform.position).normalized;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = targetRotation;
        }

        transform.position += direction * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            currentPointIndex = (currentPointIndex + 1) % waypoints.points.Length;
        }
    }



    public void SetMovement(bool isMove)
    {
        this.isMove = isMove;
    }

}
