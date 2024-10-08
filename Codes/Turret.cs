// using UnityEngine;
// using UnityEngine.UI;

// public class Turret : MonoBehaviour
// {
//     [Header("Attributes")]
//     [SerializeField] private float targetingRange = 5f;
//     [SerializeField] private float rotationSpeed = 200f;
//     [SerializeField] private float bulletsPerSecond = 1f;
//     [SerializeField] private int baseUpgradeCost = 100;

//     [Header("References")]
//     [SerializeField] private Transform turretRotationPoint;
//     [SerializeField] private GameObject bulletPrefab;
//     [SerializeField] private Transform firingPoint;
//     [SerializeField] private LayerMask enemyMask;
//     [SerializeField] private GameObject upgradeUI;
//     [SerializeField] private Button upgradeButton;
//     [SerializeField] private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer
//     [SerializeField] private Sprite[] upgradeSprites; // Array of sprites for each upgrade level

//     private Transform target;
//     private float timeUntilFire;
//     private int level = 1;
//     private float bpsBase;
//     private float targetingRangeBase;

//     void Start()
//     {
//         bpsBase = bulletsPerSecond;
//         targetingRangeBase = targetingRange;
//         upgradeButton.onClick.AddListener(Upgrade);
//         upgradeUI.SetActive(false);
//     }

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

//     private void OnMouseEnter()
//     {
//         OpenUpgradeUI();
//     }

//     private void OnMouseExit()
//     {
//         CloseUpgradeUI();
//     }

//     public void OpenUpgradeUI()
//     {
//         upgradeUI.SetActive(true);
//     }

//     public void CloseUpgradeUI()
//     {
//         upgradeUI.SetActive(false);
//         UIManager.main.SetHoveringState(false);
//     }

//     public void Upgrade()
//     {
//         Debug.Log("botton clicked");

//         if (baseUpgradeCost > LevelManager.main.currency)
//         {
//             Debug.Log("not enough money to upgrade");
//             return;
//         }

//         LevelManager.main.SpendCurrency(CalculateCost());
//         level++;
//         bulletsPerSecond = CalculateBPS();
//         targetingRange = CalculateRange();
//         CloseUpgradeUI();
//         UIManager.main.SetHoveringState(false);
//         Debug.Log("Upgrade Complete");

//         // Change the sprite to match the upgrade level
//         UpdateTurretSprite();
//     }

//     private int CalculateCost()
//     {
//         return Mathf.RoundToInt(baseUpgradeCost * Mathf.Pow(level, 0.8f));
//     }

//     private float CalculateBPS()
//     {
//         return bpsBase * Mathf.Pow(level, 0.5f);
//     }

//     private float CalculateRange()
//     {
//         return targetingRangeBase * Mathf.Pow(level, 0.4f);
//     }

//     private void OnDrawGizmosSelected()
//     {
//         Gizmos.color = Color.cyan;
//         Gizmos.DrawWireSphere(transform.position, targetingRange);
//     }

//     private void UpdateTurretSprite()
//     {
//         if (level - 1 < upgradeSprites.Length)
//         {
//             spriteRenderer.sprite = upgradeSprites[level - 1];
//         }
//     }
// }
