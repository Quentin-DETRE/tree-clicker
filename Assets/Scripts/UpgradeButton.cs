using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public UpgradeObject upgradeInfo;
    public Button button;
    public Text priceText; // Assurez-vous d'avoir une référence au texte du prix

    public void Initialize(UpgradeObject upgrade)
    {
        upgradeInfo = upgrade;
        UpdateButton();
    }

    public void UpdateButton()
    {
        // Mettre à jour le texte, l'image et l'état actif/inactif du bouton
        priceText.text = upgradeInfo.cost.ToString();
        // Ici, vous pouvez ajouter d'autres logiques pour gérer l'état du bouton
    }

    public void OnClick()
    {
        UpgradeManager.Instance.BuyUpgrade(upgradeInfo.upgradeName);
    }
}
