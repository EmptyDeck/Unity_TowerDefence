using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager main;

    [Header("UI Elements")]
    [SerializeField] private Text scoreText;
    [SerializeField] private Text topScoresText;
    [SerializeField] private Text livesText;

    private void Awake()
    {
        main = this;
    }

    private bool isHoveringUI = false;

    public void SetHoveringState(bool state)
    {
        isHoveringUI = state;
    }

    public bool IsHoveringUI()
    {
        return isHoveringUI;
    }

    public void UpdateScoreText(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    public void UpdateTopScoresText(List<int> topScores)
    {
        if (topScoresText != null)
        {
            topScoresText.text = "Top Scores:\n";
            for (int i = 0; i < topScores.Count; i++)
            {
                topScoresText.text += (i + 1) + ". " + topScores[i] + "\n";
            }
        }
    }

    public void UpdateLivesText(int lives)
    {
        if (livesText != null)
        {
            livesText.text = "Lives: " + lives;
        }
    }
}
