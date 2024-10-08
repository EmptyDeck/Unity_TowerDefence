using UnityEngine;

public class TowerStable : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float bulletsPerSecond = 1f;
    [SerializeField] private int baseUpgradeCost = 100;

    [Header("References")]
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Animator animator; // Reference to the tower's Animator
    [SerializeField] private GameObject hitEffectPrefab; // Reference to the hit effect prefab

    private Transform target;
    private float timeUntilFire;
    private int level = 1;
    private float bpsBase;
    private float targetingRangeBase;

    void Start()
    {
        bpsBase = bulletsPerSecond;
        targetingRangeBase = targetingRange;
        // upgradeButton.onClick.AddListener(Upgrade);
        upgradeUI.SetActive(false);
    }

    void Update()
    {
        if (target == null)
        {
            FindTarget();
        }
        else
        {
            if (!IsTargetInRange())
            {
                target = null;
                // Switch to idle state
                animator.SetBool("tower_att", false);
            }
            else
            {
                Attack();
            }
        }
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, Vector2.zero, 0f, enemyMask);
        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    private bool IsTargetInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void Attack()
    {
        timeUntilFire += Time.deltaTime;
        if (timeUntilFire >= 1f / bulletsPerSecond)
        {
            // Trigger the attack animation
            animator.SetBool("tower_att", true);

            // Instantiate the hit effect at the enemy's position
            GameObject hitEffect = Instantiate(hitEffectPrefab, target.position, Quaternion.identity);

            // Ensure it's on the correct sorting layer
            SpriteRenderer hitEffectSpriteRenderer = hitEffect.GetComponent<SpriteRenderer>();
            if (hitEffectSpriteRenderer != null)
            {
                hitEffectSpriteRenderer.sortingLayerName = "Effects"; // Ensure this sorting layer is above the enemies
            }

            // The hit effect will play its animation and destroy itself

            // Reset the firing timer
            timeUntilFire = 0f;
        }
    }


    private void LateUpdate()
    {
        // Ensure we reset the attack animation state if not attacking
        if (target == null || !IsTargetInRange())
        {
            animator.SetBool("tower_att", false);
        }
    }

    private void OnMouseEnter()
    {
        OpenUpgradeUI();
    }

    private void OnMouseExit()
    {
        CloseUpgradeUI();
    }

    public void OpenUpgradeUI()
    {
        upgradeUI.SetActive(true);
    }

    public void CloseUpgradeUI()
    {
        upgradeUI.SetActive(false);
        UIManager.main.SetHoveringState(false);
    }

    public void Upgrade()
    {
        if (baseUpgradeCost > LevelManager.main.currency)
        {
            Debug.Log("not enough money to upgrade");
            return;
        }

        LevelManager.main.SpendCurrency(CalculateCost());
        level++;
        bulletsPerSecond = CalculateBPS();
        targetingRange = CalculateRange();
        CloseUpgradeUI();
        UIManager.main.SetHoveringState(false);
        Debug.Log("Upgrade Complete");

        // Change the sprite to match the upgrade level
        // UpdateTurretSprite();
    }

    private int CalculateCost()
    {
        return Mathf.RoundToInt(baseUpgradeCost * Mathf.Pow(level, 0.8f));
    }

    private float CalculateBPS()
    {
        return bpsBase * Mathf.Pow(level, 0.5f);
    }

    private float CalculateRange()
    {
        return targetingRangeBase * Mathf.Pow(level, 0.4f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, targetingRange);
    }
}