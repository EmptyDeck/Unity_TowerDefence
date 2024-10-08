using UnityEngine;

public class SplashTower : TowerBase
{
    [SerializeField] private float splashRadius = 3f;
    [SerializeField] private int splashDamage = 10;


    protected override void Attack()
    {
        animator.SetBool("isAttacking", true);
        timeUntilNextAttack += Time.deltaTime;
        if (timeUntilNextAttack >= 1f / attackSpeed)
        {

            SplashAttack();
            timeUntilNextAttack = 0f;
        }
    }

    private void SplashAttack()
    {
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(target.position, splashRadius, enemyMask);
        foreach (Collider2D enemy in enemiesHit)
        {
            Health enemyHealth = enemy.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(splashDamage);
            }
        }

        GameObject hitEffect = Instantiate(hitEffectPrefab, target.position, Quaternion.identity);
        Destroy(hitEffect, 1f); // Destroy the effect after 1 second
    }

    public override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = Color.red;
        if (target != null)
        {
            Gizmos.DrawWireSphere(target.position, splashRadius);
        }
    }
}
