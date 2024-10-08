using UnityEngine;
using UnityEngine.UI;

public class SpeedButton : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Button speedButton;
    [SerializeField] private Text buttonText;

    private int speedState = 0; // 0: Normal, 1: 2x, 2: 4x

    private void Start()
    {

        // Ensure the button is assigned
        if (speedButton != null)
        {
            speedButton.onClick.AddListener(ToggleSpeed);
        }

        // Ensure the button text is assigned
        if (buttonText == null)
        {
            buttonText = speedButton.GetComponentInChildren<Text>();
        }

        UpdateButtonText();
    }

    private void ToggleSpeed()
    {
        speedState = (speedState + 1) % 3;

        switch (speedState)
        {
            case 0:
                Time.timeScale = 1f; // Normal speed
                break;
            case 1:
                Time.timeScale = 2f; // 2x speed
                break;
            case 2:
                Time.timeScale = 4f; // 4x speed
                break;
        }

        UpdateButtonText();
    }

    private void UpdateButtonText()
    {
        switch (speedState)
        {
            case 0:
                buttonText.text = "Speed: 1x";
                break;
            case 1:
                buttonText.text = "Speed: 2x";
                break;
            case 2:
                buttonText.text = "Speed: 4x";
                break;
        }
    }

    private void OnDestroy()
    {
        // Remove the listener when the object is destroyed
        if (speedButton != null)
        {
            speedButton.onClick.RemoveListener(ToggleSpeed);
        }
    }
}
