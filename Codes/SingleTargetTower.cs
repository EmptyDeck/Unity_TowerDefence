using UnityEngine;

public class SingleTargetTower : TowerBase
{
    [SerializeField] private int damage = 10; // Damage dealt per attack

    protected override void Attack()
    {
        timeUntilNextAttack += Time.deltaTime;
        if (timeUntilNextAttack >= 1f / attackSpeed)
        {
            // Trigger the attack animation
            animator.SetBool("isAttacking", true);

            if (target != null)
            {
                // Apply damage to the target
                Health targetHealth = target.GetComponent<Health>();
                if (targetHealth != null)
                {
                    targetHealth.TakeDamage(damage);
                }

                // Instantiate the hit effect at the enemy's position
                GameObject hitEffect = Instantiate(hitEffectPrefab, target.position, Quaternion.identity);
                Destroy(hitEffect, 1f); // Destroy the effect after 1 second
            }

            timeUntilNextAttack = 0f;
        }
    }

    protected override void FindTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, targetingRange, enemyMask);
        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
        else
        {
            target = null;
        }
    }
}
