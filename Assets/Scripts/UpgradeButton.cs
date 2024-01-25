using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI nameText;
    [SerializeField] 
    private Text costText;
    [SerializeField]
    private Text prodText;
    [SerializeField] 
    private Text countText;
    [SerializeField]
    private Image upgradeImage;
    private UpgradeObject upgradeData;
    public void Initialize(UpgradeObject upgrade, Sprite upgradeSprite)
    {
        upgradeData = upgrade;
        nameText.text = upgrade.upgradeName;
        costText.text = upgrade.cost.ToString();
        prodText.text = upgrade.productionPerSecond.ToString();
        countText.text = InventoryManager.Instance.GetOwnedUpgradeCount(upgrade.upgradeName).ToString();
        upgradeImage.sprite = upgradeSprite;
    }

    public void UpdateData()
    {
        costText.text = upgradeData.cost.ToString();
        countText.text = InventoryManager.Instance.GetOwnedUpgradeCount(upgradeData.upgradeName).ToString();
    }

    public void OnClick()
    {
        Debug.Log("Clicked on " + upgradeData.upgradeName);
        UpgradeManager.Instance.BuyUpgrade(upgradeData.upgradeName);
    }
}
