using UnityEngine;

public class AttackRangeBuffTower : BuffTowerBase
{
    [SerializeField] private float attackRangeBuffAmount;

    public override void ApplyBuff(TowerBase tower)
    {
        tower.ApplyBuff(attackRangeBuffAmount, BuffType.AttackRange);
    }

    public override void RemoveBuff(TowerBase tower)
    {
        tower.RemoveBuff(attackRangeBuffAmount, BuffType.AttackRange);
    }

}
