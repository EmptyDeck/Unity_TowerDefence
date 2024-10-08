using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class EventScreenManager : MonoBehaviour
{
    public GameObject logTextPrefab; // Prefab for the log text element
    public Transform contentTransform; // The Content GameObject of the Scroll View

    private Queue<GameObject> logEntries = new Queue<GameObject>();

    void Start()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDestroy()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        GameObject logEntry = Instantiate(logTextPrefab, contentTransform);
        logEntry.GetComponent<Text>().text = logString;

        logEntries.Enqueue(logEntry);

        // Optionally limit the number of log entries to prevent overflow
        if (logEntries.Count > 100)
        {
            Destroy(logEntries.Dequeue());
        }
    }
}
