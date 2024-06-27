using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Waypoints waypoints;
    public float speed = 2.0f;
    private int currentPointIndex = 0;

    void Start()
    {
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
}
