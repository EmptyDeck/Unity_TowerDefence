using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int hitPoints = 2;
    [SerializeField] private int currencyWorth = 50;

    private bool isDestroyed = false;
    private EnemyScore enemyScore;

    private void Start()
    {
        enemyScore = GetComponent<EnemyScore>();
        if (enemyScore == null)
        {
            Debug.LogError("EnemyScore component not found on: " + gameObject.name);
        }
        else
        {
            //Debug.Log("EnemyScore component found on: " + gameObject.name + " with score: " + enemyScore.GetScore());
        }
    }

    public void TakeDamage(int dmg)
    {
        hitPoints -= dmg;

        if (hitPoints <= 0 && !isDestroyed)
        {
            if (enemyScore != null)
            {
                EnemySpawner.onEnemyDestroyed.Invoke(enemyScore.GetScore());
            }
            LevelManager.main.IncreaseCurrency(currencyWorth);
            isDestroyed = true;
            Destroy(gameObject);
        }
    }

    private void EnemyDestroyed()
    {
        Destroy(gameObject);
        if (enemyScore != null)
        {
            EnemySpawner.onEnemyDestroyed?.Invoke(enemyScore.GetScore());
        }
    }
}
