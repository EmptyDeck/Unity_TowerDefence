// using System.Collections.Generic;
// using UnityEngine;

// public class InvisibleBuffRange : MonoBehaviour
// {
//     private List<BuffPlot> buffPlots = new List<BuffPlot>();
//     private List<TowerBase> towersInRange = new List<TowerBase>();

//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         TowerBase tower = other.GetComponent<TowerBase>();
//         if (tower != null)
//         {
//             towersInRange.Add(tower);
//             ApplyBuffs(tower);
//         }
//     }

//     private void OnTriggerExit2D(Collider2D other)
//     {
//         TowerBase tower = other.GetComponent<TowerBase>();
//         if (tower != null)
//         {
//             towersInRange.Remove(tower);
//             RemoveBuffs(tower);
//         }
//     }

//     public void RegisterBuffPlot(BuffPlot buffPlot)
//     {
//         buffPlots.Add(buffPlot);
//     }

//     private void ApplyBuffs(TowerBase tower)
//     {
//         foreach (BuffPlot plot in buffPlots)
//         {
//             BuffTower buffTower = plot.GetBuffTower();
//             if (buffTower != null)
//             {
//                 buffTower.ApplyBuff(tower);
//             }
//         }
//     }

//     private void RemoveBuffs(TowerBase tower)
//     {
//         foreach (BuffPlot plot in buffPlots)
//         {
//             BuffTower buffTower = plot.GetBuffTower();
//             if (buffTower != null)
//             {
//                 buffTower.RemoveBuff(tower);
//             }
//         }
//     }
// }
