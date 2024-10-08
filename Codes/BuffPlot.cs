// using System.Collections.Generic;
// using UnityEngine;

// public class BuffPlot : MonoBehaviour
// {
//     [Header("References")]
//     [SerializeField] private SpriteRenderer sr;
//     [SerializeField] private Color hoverColor;

//     private Color startColor;
//     private BuffTower buffTower;
//     private List<TowerBase> towersInRange = new List<TowerBase>();

//     private void Start()
//     {
//         startColor = sr.color;
//     }

//     private void OnMouseEnter()
//     {
//         sr.color = hoverColor;
//     }

//     private void OnMouseExit()
//     {
//         sr.color = startColor;
//     }

//     private void OnMouseDown()
//     {
//         if (UIManager.main.IsHoveringUI()) return;

//         if (buffTower != null)
//         {
//             buffTower.OpenUpgradeUI();
//             return;
//         }

//         BuffTower towerToBuild = BuildManager.main.GetSelectedTower() as BuffTower;

//         if (towerToBuild == null)
//         {
//             Debug.LogError("No BuffTower selected to build.");
//             return;
//         }

//         if (towerToBuild.GetBaseUpgradeCost() > LevelManager.main.currency)
//         {
//             Debug.Log("You can't afford this tower");
//             return;
//         }

//         LevelManager.main.SpendCurrency(towerToBuild.GetBaseUpgradeCost());
//         buffTower = Instantiate(towerToBuild, transform.position, Quaternion.identity);
//         buffTower.AssignPlot(this);
//     }

//     public void SetBuffTower(BuffTower tower)
//     {
//         this.buffTower = tower;
//     }

//     public BuffTower GetBuffTower()
//     {
//         return buffTower;
//     }
// }
