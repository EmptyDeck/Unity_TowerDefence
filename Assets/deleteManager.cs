using UnityEngine;

public class DeleteManager : MonoBehaviour
{
    public static DeleteManager main;

    private bool isDeleteMode = false;

    void Awake()
    {
        if (main != null)
        {
            Debug.LogError("More than one DeleteManager in the scene!");
            return;
        }
        main = this;
    }

    public void ToggleDeleteMode()
    {
        isDeleteMode = !isDeleteMode;
        Debug.Log("Delete Mode: " + (isDeleteMode ? "ON" : "OFF"));
    }

    public bool IsDeleteMode()
    {
        return isDeleteMode;
    }
}
