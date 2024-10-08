using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Button pauseButton;
    [SerializeField] private Text buttonText;

    private bool isPaused = false;

    private void Start()
    {
        // Ensure the button is assigned
        if (pauseButton != null)
        {
            pauseButton.onClick.AddListener(TogglePause);
        }

        // Ensure the button text is assigned
        if (buttonText == null)
        {
            buttonText = pauseButton.GetComponentInChildren<Text>();
        }

        UpdateButtonText();
    }

    private void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // Pause the game
        }
        else
        {
            Time.timeScale = 1f; // Resume the game
        }

        UpdateButtonText();
    }

    private void UpdateButtonText()
    {
        if (isPaused)
        {
            buttonText.text = "Resume";
        }
        else
        {
            buttonText.text = "Pause";
        }
    }

    private void OnDestroy()
    {
        // Remove the listener when the object is destroyed
        if (pauseButton != null)
        {
            pauseButton.onClick.RemoveListener(TogglePause);
        }
    }
}
