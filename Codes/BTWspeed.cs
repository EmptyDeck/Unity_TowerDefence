using UnityEngine;

public class AttackSpeedBuffTower : BuffTowerBase
{
    [SerializeField] private float attackSpeedBuffAmount;

    public override void ApplyBuff(TowerBase tower)
    {
        tower.ApplyBuff(attackSpeedBuffAmount, BuffType.AttackSpeed);
    }

    public override void RemoveBuff(TowerBase tower)
    {
        tower.RemoveBuff(attackSpeedBuffAmount, BuffType.AttackSpeed);
    }

}
