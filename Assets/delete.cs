using UnityEngine;
using UnityEngine.UI;

public class DeleteButton : MonoBehaviour
{
    private ToggleButton toggleButton;

    void Start()
    {
        toggleButton = GetComponent<ToggleButton>();
        toggleButton.CheckedChanged.AddListener(OnDeleteToggleChanged);
    }

    private void OnDeleteToggleChanged(ToggleButton button)
    {
        if (button.Checked)
        {
            DeleteManager.main.ToggleDeleteMode();
        }
        else
        {
            DeleteManager.main.ToggleDeleteMode();
        }
    }
}
