using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuffTowerUIHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool isMouseOver = false;
    private BuffTowerBase buffTower;

    [Header("UI References")]
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button upgradeButton;

    void Start()
    {
        upgradeButton.onClick.AddListener(Upgrade);
        upgradeUI.SetActive(false);
    }

    public void SetBuffTower(BuffTowerBase tower)
    {
        buffTower = tower;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOver = true;
        UIManager.main.SetHoveringState(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;
        UIManager.main.SetHoveringState(false);
        upgradeUI.SetActive(false);
    }

    private void Upgrade()
    {
        if (buffTower != null)
        {
            buffTower.Upgrade();
        }
    }

    public void OpenUpgradeUI()
    {
        upgradeUI.SetActive(true);
    }

    public void CloseUpgradeUI()
    {
        upgradeUI.SetActive(false);
    }
    private void OnMouseDown()
    {
        OpenUpgradeUI();
    }

}
