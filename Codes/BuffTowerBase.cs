using UnityEngine;
using UnityEngine.UI;

public abstract class BuffTowerBase : MonoBehaviour
{
    [Header("Upgrade Attributes")]
    public int level = 1;
    public int upgradeCost = 100;
    protected TowerBase assignedTower;

    protected Animator animator;

    [Header("UI References")]
    [SerializeField] private BuffTowerUIHandler uiHandler; // UI 핸들러 참조

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        PlayAnimation();

        if (uiHandler != null)
        {
            uiHandler.SetBuffTower(this);
        }
    }

    protected void PlayAnimation()
    {
        if (animator != null)
        {
            animator.Play("BuffAnimation");
        }
    }

    public abstract void ApplyBuff(TowerBase tower);
    public abstract void RemoveBuff(TowerBase tower);

    public void Upgrade()
    {
        if (upgradeCost > LevelManager.main.currency)
        {
            Debug.Log("Not enough money to upgrade");
            return;
        }

        LevelManager.main.SpendCurrency(upgradeCost);
        level++;
        ApplyBuff(assignedTower); // 업그레이드 후 버프 재적용
        uiHandler.CloseUpgradeUI();
    }

    public void OpenUpgradeUI()
    {
        if (uiHandler != null)
        {
            uiHandler.OpenUpgradeUI();
        }
    }

    public void CloseUpgradeUI()
    {
        if (uiHandler != null)
        {
            uiHandler.CloseUpgradeUI();
        }
    }
}
