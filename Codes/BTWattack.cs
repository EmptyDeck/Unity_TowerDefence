using UnityEngine;

public class BTWattack : BuffTowerBase
{
    [SerializeField] private float attackPowerBuffAmount;

    public override void ApplyBuff(TowerBase tower)
    {
        tower.ApplyBuff(attackPowerBuffAmount, BuffType.AttackPower);
    }

    public override void RemoveBuff(TowerBase tower)
    {
        tower.RemoveBuff(attackPowerBuffAmount, BuffType.AttackPower);
    }
}
