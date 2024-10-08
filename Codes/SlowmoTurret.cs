using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMoTurret : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float freezeTime = 2f;
    [SerializeField] private float attacksPerSecond = 0.25f; // This means the turret attacks every 4 seconds

    [Header("References")]
    [SerializeField] private LayerMask enemyMask;

    private float timeUntilFire;

    void Update()
    {
        timeUntilFire += Time.deltaTime;
        if (timeUntilFire >= 1f / attacksPerSecond)
        {
            FreezeEnemies();
            timeUntilFire = 0f;
        }
    }

    private void FreezeEnemies()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, targetingRange, enemyMask);
        if (hits.Length > 0)
        {
            foreach (var hit in hits)
            {
                EnemyMove enemy = hit.transform.GetComponent<EnemyMove>();
                if (enemy != null)
                {
                    enemy.UpdateSpeed(0.5f);
                    StartCoroutine(ResetEnemySpeed(enemy));
                }
            }
        }
    }

    private IEnumerator ResetEnemySpeed(EnemyMove enemy)
    {
        yield return new WaitForSeconds(freezeTime);
        enemy.ResetSpeed();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, targetingRange);
    }
}
