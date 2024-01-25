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
    private Text hintText;
    [SerializeField]
    private Image upgradeImage;
    private UpgradeObject upgradeData;
    public void Initialize(UpgradeObject upgrade, Sprite upgradeSprite, string hint)
    {
        upgradeData = upgrade;
        nameText.text = upgrade.upgradeName;
        costText.text = upgrade.cost.ToString();
        prodText.text = upgrade.productionPerSecond.ToString();
        countText.text = InventoryManager.Instance.GetOwnedUpgradeCount(upgrade.upgradeName).ToString();
        upgradeImage.sprite = upgradeSprite;
        hintText.text = hint;
    }

    public void UpdateData()
    {
        Debug.Log("UpdateData");
        Debug.Log(upgradeData.upgradeName);
        Debug.Log(InventoryManager.Instance.GetOwnedUpgradeCount(upgradeData.upgradeName));
        costText.text = upgradeData.cost.ToString();
        countText.text = InventoryManager.Instance.GetOwnedUpgradeCount(upgradeData.upgradeName).ToString();
        hintText.text = UIManager.Instance.GetCurrentUpgradeStepDescription(upgradeData);
    }

    public void OnClick()
    {
        UpgradeManager.Instance.BuyUpgrade(upgradeData.upgradeName);
    }
}
