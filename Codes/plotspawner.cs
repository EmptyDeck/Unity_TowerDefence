using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotSpawner : MonoBehaviour
{
    [Header("Plot Settings")]
    public GameObject plotPrefab;
    public int numberOfPlots;
    public float moveSpeed = 2f;

    [Header("Waypoint Manager")]
    public WaypointManager waypointManager;

    private List<GameObject> plots = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(SpawnPlots());
    }

    private IEnumerator SpawnPlots()
    {
        if (waypointManager == null)
        {
            Debug.LogError("WaypointManager is not assigned!");
            yield break;
        }

        float circumference = waypointManager.CalculateCircumference();
        float timeToCompleteLoop = circumference / moveSpeed;
        float spawnInterval = timeToCompleteLoop / numberOfPlots;

        for (int i = 0; i < numberOfPlots; i++)
        {
            Vector3 spawnPosition = waypointManager.waypoints[0].position;
            GameObject plot = Instantiate(plotPrefab, spawnPosition, Quaternion.identity);
            plots.Add(plot);

            PlotMovement plotMovement = plot.GetComponent<PlotMovement>();
            plotMovement.Initialize(waypointManager.waypoints, moveSpeed);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
