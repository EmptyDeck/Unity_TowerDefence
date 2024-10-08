using UnityEngine;

public abstract class TowerBase : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] protected float targetingRange = 5f;
    [SerializeField] protected float attackSpeed = 1f;
    [SerializeField] protected int baseUpgradeCost = 100;

    [Header("References")]
    [SerializeField] protected LayerMask enemyMask;
    [SerializeField] protected GameObject upgradeUI;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Sprite[] upgradeSprites;
    [SerializeField] protected Animator animator;
    [SerializeField] protected GameObject hitEffectPrefab;



    /// BUFF
    public float TargetingRange
    {
        get { return targetingRange; }
        set { targetingRange = value; }
    }

    public float AttackSpeed
    {
        get { return attackSpeed; }
        set { attackSpeed = value; }
    }
    public virtual void ApplyBuff(float amount, BuffType buffType)
    {
        switch (buffType)
        {
            case BuffType.AttackPower:
                // Apply attack power buff
                break;
            case BuffType.AttackSpeed:
                AttackSpeed += amount;
                break;
            case BuffType.AttackRange:
                TargetingRange += amount;
                break;
        }
    }

    public virtual void RemoveBuff(float amount, BuffType buffType)
    {
        switch (buffType)
        {
            case BuffType.AttackPower:
                // Remove attack power buff
                break;
            case BuffType.AttackSpeed:
                AttackSpeed -= amount;
                break;
            case BuffType.AttackRange:
                TargetingRange -= amount;
                break;
        }
    }
    /////////

    protected Transform target;
    protected float timeUntilNextAttack;
    protected bool isAttacking;

    protected int level = 1;

    public int Level
    {
        get { return level; }
        private set { level = value; }
    }

    protected float baseAttackSpeed;
    protected float baseTargetingRange;
    protected Plot assignedPlot;



    protected virtual void Start()
    {
        baseAttackSpeed = attackSpeed;
        baseTargetingRange = targetingRange;
        upgradeUI.SetActive(false);
    }


    public void AssignPlot(Plot plot)
    {
        assignedPlot = plot;
    }

    // Update method modification

    void Update()
    {
        if (assignedPlot != null)
        {
            transform.position = assignedPlot.transform.position;
            //Debug.LogError(assignedPlot.transform.position);
        }

        if (target == null)
        {
            FindTarget();
        }
        else
        {
            if (!IsTargetInRange())
            {
                target = null;
                animator.SetBool("isAttacking", false);
            }
            else
            {
                Attack();
            }
        }
    }

    protected virtual void FindTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, targetingRange, enemyMask);
        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    protected bool IsTargetInRange()
    {
        return target != null && Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    protected abstract void Attack();
    public void OpenUpgradeUI()
    {
        if (upgradeUI != null)
        {
            upgradeUI.SetActive(true);
        }
        else
        {
            Debug.LogError("Upgrade UI is not assigned.");
        }
    }

    public int GetBaseUpgradeCost()
    {
        return baseUpgradeCost;
    }

    public void Upgrade()
    {
        level++;
        attackSpeed = CalculateAttackSpeed();
        targetingRange = CalculateTargetingRange();
        UpdateSprite();
    }

    protected void UpdateSprite()
    {
        if (level - 1 < upgradeSprites.Length)
        {
            spriteRenderer.sprite = upgradeSprites[level - 1];
        }
    }


    protected int CalculateUpgradeCost()
    {
        return Mathf.RoundToInt(baseUpgradeCost * Mathf.Pow(level, 0.8f));
    }

    protected float CalculateAttackSpeed()
    {
        return baseAttackSpeed * Mathf.Pow(level, 0.5f);
    }

    protected float CalculateTargetingRange()
    {
        return baseTargetingRange * Mathf.Pow(level, 0.4f);
    }



    public virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, targetingRange);
    }
}
