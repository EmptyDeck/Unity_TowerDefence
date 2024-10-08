using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    private Color startColor;
    private TowerBase tower;

    private void Start()
    {
        startColor = sr.color;
    }

    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    private void OnMouseDown()
    {
        if (UIManager.main.IsHoveringUI()) return;

        if (DeleteManager.main.IsDeleteMode())
        {
            DeleteTower();
            return;
        }

        if (tower != null)
        {
            tower.OpenUpgradeUI();
            return;
        }

        TowerBase towerToBuild = BuildManager.main.GetSelectedTower();

        if (towerToBuild == null)
        {
            Debug.LogError("No tower selected to build.");
            return;
        }

        if (towerToBuild.GetBaseUpgradeCost() > LevelManager.main.currency)
        {
            Debug.Log("You can't afford this tower");
            return;
        }

        LevelManager.main.SpendCurrency(towerToBuild.GetBaseUpgradeCost());
        tower = Instantiate(towerToBuild, transform.position, Quaternion.identity);
        tower.AssignPlot(this);

        UpgradeUIHandler upgradeUIHandler = tower.GetComponentInChildren<UpgradeUIHandler>();
        if (upgradeUIHandler != null)
        {
            upgradeUIHandler.SetTower(tower);
        }
        else
        {
            Debug.LogError("UpgradeUIHandler not found on the tower object.");
        }
    }

    public void DeleteTower()
    {
        if (tower != null)
        {
            Destroy(tower.gameObject);
            tower = null;
            Debug.Log("Tower deleted");
        }
    }


    public void SetTower(TowerBase tower)
    {
        this.tower = tower;
        tower.transform.SetParent(transform);
        tower.transform.localPosition = Vector3.zero;
        UpgradeUIHandler upgradeUIHandler = tower.GetComponentInChildren<UpgradeUIHandler>();
        if (upgradeUIHandler != null)
        {
            upgradeUIHandler.SetTower(tower);
        }
        else
        {
            Debug.LogError("UpgradeUIHandler not found on the tower object.");
        }
    }


    public TowerBase GetTower()
    {
        return tower;
    }
}
