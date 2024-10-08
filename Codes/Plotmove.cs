using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotMovement : MonoBehaviour
{
    private Transform[] waypoints;
    private float moveSpeed;
    private int waypointIndex = 0;
    private float distanceThreshold = 0.1f;

    public void Initialize(Transform[] waypoints, float moveSpeed)
    {
        this.waypoints = waypoints;
        this.moveSpeed = moveSpeed;
        transform.position = waypoints[0].position;
    }

    private void Update()
    {
        if (waypoints != null)
        {
            MoveTowardsWaypoint();
        }
    }

    private void MoveTowardsWaypoint()
    {
        Vector3 targetPosition = waypoints[waypointIndex].position;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPosition) <= distanceThreshold)
        {
            waypointIndex = (waypointIndex + 1) % waypoints.Length;
        }
    }
}
