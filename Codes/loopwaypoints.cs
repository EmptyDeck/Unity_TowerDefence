using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    public static WaypointManager instance;

    [Header("Waypoints")]
    public Transform[] waypoints;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public float CalculateCircumference()
    {
        float length = 0f;
        for (int i = 0; i < waypoints.Length; i++)
        {
            int nextIndex = (i + 1) % waypoints.Length;
            length += Vector3.Distance(waypoints[i].position, waypoints[nextIndex].position);
        }
        return length;
    }
}
