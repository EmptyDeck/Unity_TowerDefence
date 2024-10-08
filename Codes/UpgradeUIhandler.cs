using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UpgradeUIHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool isMouseOver = false;
    private TowerBase tower;
    private int level = 1;

    [Header("References")]
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] upgradeSprites;

    // void Start()
    // {
    //     upgradeButton.onClick.AddListener(Upgrade);
    //     upgradeUI.SetActive(false);
    // }

    // DEBUG 

    void Start()
    {
        if (upgradeButton != null)
        {
            Debug.Log("Upgrade button found");
            upgradeButton.onClick.AddListener(Upgrade);
        }
        else
        {
            Debug.LogError("Upgrade button is not assigned");
        }

        if (upgradeUI != null)
        {
            upgradeUI.SetActive(false);
        }
        else
        {
            Debug.LogError("Upgrade UI is not assigned");
        }
    }


    public void SetTower(TowerBase tower)
    {
        this.tower = tower;
        level = tower.Level; // Ensure level is in sync with the tower's level
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOver = true;
        UIManager.main.SetHoveringState(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;
        UIManager.main.SetHoveringState(false);
        upgradeUI.SetActive(false);
    }

    private void OnMouseDown()
    {
        OpenUpgradeUI();
    }

    public void OpenUpgradeUI()
    {
        upgradeUI.SetActive(true);
    }

    public void CloseUpgradeUI()
    {
        upgradeUI.SetActive(false);
        UIManager.main.SetHoveringState(false);
    }

    // public void Upgrade()
    // {
    //     Debug.Log("trying to upgrade");
    //     if (tower.GetBaseUpgradeCost() > LevelManager.main.currency)
    //     {
    //         Debug.Log("Not enough money to upgrade");
    //         return;
    //     }

    //     LevelManager.main.SpendCurrency(CalculateCost());
    //     tower.Upgrade(); // Upgrade the tower
    //     CloseUpgradeUI();
    //     UIManager.main.SetHoveringState(false);
    //     Debug.Log("Upgrade Complete");

    //     // Change the sprite to match the upgrade level
    //     UpdateTowerSprite();
    // }
    // DEBUG

    public void Upgrade()
    {
        Debug.Log("Upgrade button clicked"); // Add this log

        if (tower.GetBaseUpgradeCost() > LevelManager.main.currency)
        {
            Debug.Log("Not enough money to upgrade");
            return;
        }

        LevelManager.main.SpendCurrency(CalculateCost());
        tower.Upgrade(); // Upgrade the tower
        CloseUpgradeUI();
        UIManager.main.SetHoveringState(false);
        Debug.Log("Upgrade Complete");

        // Change the sprite to match the upgrade level
        UpdateTowerSprite();
    }


    private int CalculateCost()
    {
        return Mathf.RoundToInt(tower.GetBaseUpgradeCost() * Mathf.Pow(level, 0.8f));
    }

    private void UpdateTowerSprite()
    {
        if (level - 1 < upgradeSprites.Length)
        {
            spriteRenderer.sprite = upgradeSprites[level - 1];
        }
    }
}
