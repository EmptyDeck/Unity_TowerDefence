using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private int[] enemyScores; // New array to store scores for each enemy type

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float baseEnemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;
    [SerializeField] private float maxEnemiesPerSecond = 15f; // Max enemies per second

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;
    private float enemiesPerSecond; // Variable for scaling enemies per second

    public static UnityEvent<int> onEnemyDestroyed; // Updated to pass the score value

    private void Awake()
    {
        if (onEnemyDestroyed == null)
        {
            onEnemyDestroyed = new UnityEvent<int>();
        }
        onEnemyDestroyed.AddListener(OnEnemyDestroyed);

        // Verify that enemyPrefabs and enemyScores arrays are of the same length
        if (enemyPrefabs.Length != enemyScores.Length)
        {
            Debug.LogError("Enemy prefabs and scores arrays are not of the same length!");
        }
    }

    private void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    private void Update()
    {
        if (!isSpawning) return;

        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / enemiesPerSecond) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            timeSinceLastSpawn = 0f;
            enemiesLeftToSpawn--;
            enemiesAlive++;
        }
    }

    private IEnumerator SpawnWaves()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenWaves);
            StartWave();
            yield return new WaitForSeconds(1f / enemiesPerSecond * enemiesLeftToSpawn); // Wait for the wave to finish spawning
            EndWave();
        }
    }

    private void StartWave()
    {
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
        enemiesPerSecond = Mathf.Clamp(baseEnemiesPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor), 0, maxEnemiesPerSecond);
        Debug.Log("Starting Wave: " + currentWave + " with " + enemiesLeftToSpawn + " enemies at " + enemiesPerSecond + " enemies per second.");
    }

    private void SpawnEnemy()
    {
        int index = Random.Range(0, enemyPrefabs.Length);
        GameObject prefabToSpawn = enemyPrefabs[index];
        GameObject enemy = Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);

        if (enemy != null)
        {
            EnemyScore enemyScore = enemy.GetComponent<EnemyScore>();
            if (enemyScore != null)
            {
                enemyScore.Initialize(enemyScores[index]); // Pass the score value to the enemy
                //Debug.Log("Spawned enemy: " + prefabToSpawn.name + " with score: " + enemyScores[index]);
            }
            else
            {
                Debug.LogError("Enemy prefab does not have an EnemyScore component: " + prefabToSpawn.name);
            }
        }
        else
        {
            Debug.LogError("Failed to instantiate enemy prefab: " + prefabToSpawn.name);
        }
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }

    private void OnEnemyDestroyed(int score)
    {
        enemiesAlive--;
        LevelManager.main.AddScore(score); // Update the score

        if (enemiesAlive <= 0 && enemiesLeftToSpawn <= 0)
        {
            EndWave();
        }
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        Debug.Log("Wave Ended. Starting new wave in " + timeBetweenWaves + " seconds.");
    }
}
