using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // For reloading the scene

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    [Header("References")]
    public Transform startPoint;
    public Transform[] path;

    [Header("Currency")]
    public int currency = 100;

    [Header("Score")]
    private int currentScore = 0;
    private List<int> topScores = new List<int>();

    [Header("Game Over")]
    public int maxLives = 20;
    private int currentLives;
    public GameObject gameOverPanel; // Reference to the game over panel
    public GameObject replayButton; // Reference to the replay button

    private void Awake()
    {
        main = this;
        LoadTopScores();
        currentLives = maxLives;
        gameOverPanel.SetActive(false);
        replayButton.SetActive(false);
    }

    public void IncreaseCurrency(int amount)
    {
        currency += amount;
    }

    public bool SpendCurrency(int amount)
    {
        if (amount <= currency)
        {
            currency -= amount;
            return true;
        }
        else
        {
            Debug.Log("You do not have enough to purchase this item");
            return false;
        }
    }

    public void AddScore(int score)
    {
        currentScore += score;
        UIManager.main.UpdateScoreText(currentScore);
        CheckForTopScore();
    }

    private void CheckForTopScore()
    {
        if (topScores.Count < 5 || currentScore > topScores[topScores.Count - 1])
        {
            topScores.Add(currentScore);
            topScores.Sort((a, b) => b.CompareTo(a)); // Sort descending
            if (topScores.Count > 5)
            {
                topScores.RemoveAt(topScores.Count - 1);
            }
            SaveTopScores();
            UIManager.main.UpdateTopScoresText(topScores);
        }
    }

    private void SaveTopScores()
    {
        for (int i = 0; i < topScores.Count; i++)
        {
            PlayerPrefs.SetInt("TopScore" + i, topScores[i]);
        }
        PlayerPrefs.Save();
    }

    private void LoadTopScores()
    {
        topScores.Clear();
        for (int i = 0; i < 5; i++)
        {
            if (PlayerPrefs.HasKey("TopScore" + i))
            {
                topScores.Add(PlayerPrefs.GetInt("TopScore" + i));
            }
        }
    }

    public void EnemyReachedEnd()
    {
        currentLives--;
        UIManager.main.UpdateLivesText(currentLives);
        if (currentLives <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
        gameOverPanel.SetActive(true);
        replayButton.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

    public void ReplayGame()
    {
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }
}
