using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    private TowerBase selectedTower;



    void Awake()
    {
        if (main != null)
        {
            Debug.LogError("More than one BuildManager in the scene!");
            return;
        }
        main = this;
    }

    public void SetSelectedTower(TowerBase towerPrefab)
    {
        selectedTower = towerPrefab;
    }


    public TowerBase GetSelectedTower()
    {
        return selectedTower;
    }
    public void BuildTowerOn(Plot plot)
    {
        if (selectedTower == null)
        {
            Debug.LogWarning("No tower selected to build.");
            return;
        }

        if (LevelManager.main.currency < selectedTower.GetBaseUpgradeCost())
        {
            Debug.Log("You can't afford this tower");
            return;
        }

        LevelManager.main.SpendCurrency(selectedTower.GetBaseUpgradeCost());
        TowerBase towerInstance = Instantiate(selectedTower, plot.transform.position, Quaternion.identity);
        towerInstance.AssignPlot(plot); // Assign the plot to the tower
        plot.SetTower(towerInstance); // Use SetTower to handle parenting and positioning
    }
    // New method to handle toggle button changes
    public void HandleToggleChange(ToggleButton toggle)
    {
        if (toggle.Checked)
        {
            // Assuming each ToggleButton has a TowerBase prefab assigned to it
            TowerBase selectedTowerPrefab = toggle.GetComponent<TowerBase>();

            if (selectedTowerPrefab != null)
            {
                SetSelectedTower(selectedTowerPrefab);
                Debug.Log("Selected Tower: " + selectedTowerPrefab.name);
            }
            else
            {
                Debug.LogError("ToggleButton does not have a TowerBase component.");
            }
        }
    }





}
