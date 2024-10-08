using UnityEngine;

public class GasTower : TowerBase
{
    [Header("Gas Tower Attributes")]
    [SerializeField] private float damage = 15f; // Damage per application
    [SerializeField] private float gasRadius = 3f;
    [SerializeField] private GameObject attackEffectPrefab; // Effect to show on the tower

    protected override void Start()
    {
        base.Start();

        animator.SetBool("isAttacking", true);
        InvokeRepeating(nameof(ApplyGasDamage), 0f, 3f); // Apply damage every 3 seconds
    }

    protected override void Attack() // dont need it
    {
        // Trigger the attack animation
        // Instantiate the attack effect on the tower

    }


    private void ApplyGasDamage() // every X seconds
    {

        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, gasRadius, enemyMask);
        foreach (Collider2D enemy in enemiesInRange)// attack enemy in range
        {
            Health enemyHealth = enemy.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage((int)damage);
            }
        }
        if (attackEffectPrefab != null)// attack effect
        {
            GameObject attackEffect = Instantiate(attackEffectPrefab, transform.position, Quaternion.identity, transform);
            Destroy(attackEffect, 1f); // Destroy the effect after 1 second
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, gasRadius);
    }
}
