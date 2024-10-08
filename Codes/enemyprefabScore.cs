using UnityEngine;

public class EnemyScore : MonoBehaviour
{
    private int scoreValue;

    public void Initialize(int score)
    {
        scoreValue = score;
    }

    public int GetScore()
    {
        return scoreValue;
    }

    private void OnDestroy()
    {
        EnemySpawner.onEnemyDestroyed.Invoke(scoreValue);
    }
}
