// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEditor;
// public class Turret : MonoBehaviour
// {
//     [Header("Attributes")]
//     [SerializeField] private float targetingRange = 5f;
//     [SerializeField] private float rotationSpeed = 200f;
//     [SerializeField] private float bulletsPerSecond = 1f;

//     [Header("References")]
//     [SerializeField] private Transform turretRotationPoint;
//     [SerializeField] private GameObject bulletPrefab;
//     [SerializeField] private Transform firingPoint;
//     [SerializeField] private LayerMask enemyMask;

//     private Transform target;
//     private float timeUntilFire;

//     void Update()
//     {
//         if (target == null)
//         {
//             FindTarget();
//         }
//         else
//         {
//             if (!IsTargetInRange())
//             {
//                 target = null;
//             }
//             else
//             {
//                 RotateTowardsTarget();
//                 Shoot();
//             }
//         }
//     }

//     private void FindTarget()
//     {
//         RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, Vector2.zero, 0f, enemyMask);
//         if (hits.Length > 0)
//         {
//             target = hits[0].transform;
//         }
//     }

//     private bool IsTargetInRange()
//     {
//         return Vector2.Distance(target.position, transform.position) <= targetingRange;
//     }

//     private void RotateTowardsTarget()
//     {
//         Vector3 direction = target.position - transform.position;
//         float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
//         Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
//         turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
//     }

//     private void Shoot()
//     {
//         timeUntilFire += Time.deltaTime;
//         if (timeUntilFire >= 1f / bulletsPerSecond)
//         {
//             GameObject bullet = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
//             Bullet bulletScript = bullet.GetComponent<Bullet>();
//             bulletScript.SetTarget(target);
//             timeUntilFire = 0f;
//         }
//     }

//     private void OnDrawGizmosSelected()
//     {
//         Gizmos.color = Color.cyan;
//         Gizmos.DrawWireSphere(transform.position, targetingRange);
//     }
// }
