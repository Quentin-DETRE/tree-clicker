using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public EconomyManager economy;
    public InventoryManager inventory;

    public Text moneyUI;

    //shopContent va stocker toutes les améliorations du shop, sous la forme [nom, [prix, gainArgent/secondes, argentMinPourAfficher]]
    private List<shopItem> shopContent;
    private void Start()
    {
        //On crée les objets disponibles dans le shop
        shopContent = new List<shopItem>();

        //On ajoute au shop les items sous la forme (nom, prix, gain, temps pour le gains, effects ultérieurs (nom de l'item affecté, liste des paramettres de gains, type d'effet sur l'item))
        shopContent.Add(new shopItem("Arbre", 10, 1, 15, new effectItem("Arbre", new List<int> { 2, 3, 4, 5 }, typeEffectsItem.multiplyGains)));
        shopContent.Add(new shopItem("Volontaire", 50, 9, 30, new effectItem("Planter un arbre", new List<int> { 2, 3, 4, 5 }, typeEffectsItem.multiplyGains)));
        shopContent.Add(new shopItem("Paysan", 300, 50, 30, new effectItem("Planter un arbre", new List<int> { 2, 3, 4, 5 }, typeEffectsItem.multiplyGains)));
        shopContent.Add(new shopItem("Tracteur", 1000, 340, 60, new effectItem("Planter un arbre", new List<int> { 2, 3, 4, 5 }, typeEffectsItem.multiplyGains)));
        shopContent.Add(new shopItem("Ruche d'abeilles", 4000, 666, 30, new effectItem("Planter un arbre", new List<int> { 2, 3, 4, 5 }, typeEffectsItem.multiplyGains)));
        shopContent.Add(new shopItem("Éolienne", 9000, 3000, 60, new effectItem("Planter un arbre", new List<int> { 2, 3, 4, 5 }, typeEffectsItem.multiplyGains)));
        shopContent.Add(new shopItem("Équipe de Jardiniers", 12000, 2000, 30, new effectItem("Planter un arbre", new List<int> { 2, 3, 4, 5 }, typeEffectsItem.multiplyGains)));
        shopContent.Add(new shopItem("Réserve d'Eau", 27000, 9000, 60, new effectItem("Planter un arbre", new List<int> { 2, 3, 4, 5 }, typeEffectsItem.multiplyGains)));
        shopContent.Add(new shopItem("Serre", 49000, 4100, 15, new effectItem("Planter un arbre", new List<int> { 2, 3, 4, 5 }, typeEffectsItem.multiplyGains)));
        shopContent.Add(new shopItem("Collecte de Pluie", 66666, 11111, 30, new effectItem("Planter un arbre", new List<int> { 2, 3, 4, 5 }, typeEffectsItem.multiplyGains)));
        shopContent.Add(new shopItem("Puits d'Irrigation", 99999, 8500, 15, new effectItem("Planter un arbre", new List<int> { 2, 3, 4, 5 }, typeEffectsItem.multiplyGains)));
        shopContent.Add(new shopItem("Association green it", 135000, 225000, 30, new effectItem("Planter un arbre", new List<int> { 2, 3, 4, 5 }, typeEffectsItem.multiplyGains)));
        shopContent.Add(new shopItem("Drones de Plantation", 200000, 70000, 60, new effectItem("Planter un arbre", new List<int> { 2, 3, 4, 5 }, typeEffectsItem.multiplyGains)));
        shopContent.Add(new shopItem("Répulsif", 275000, 459000, 30, new effectItem("Planter un arbre", new List<int> { 2, 3, 4, 5 }, typeEffectsItem.multiplyGains)));
        shopContent.Add(new shopItem("Engrais", 320000, 108000, 60, new effectItem("Planter un arbre", new List<int> { 2, 3, 4, 5 }, typeEffectsItem.multiplyGains)));
        shopContent.Add(new shopItem("Laboratoire de Recherche Écologique", 390000, 130000, 60, new effectItem("Planter un arbre", new List<int> { 2, 3, 4, 5 }, typeEffectsItem.multiplyGains)));
        shopContent.Add(new shopItem("Régulateur d'humiditié", 450000, 37500, 15, new effectItem("Planter un arbre", new List<int> { 2, 3, 4, 5 }, typeEffectsItem.multiplyGains)));
        shopContent.Add(new shopItem("Modification génétique d'arbres", 600000, 200000, 30, new effectItem("Planter un arbre", new List<int> { 2, 3, 4, 5 }, typeEffectsItem.multiplyGains)));
        shopContent.Add(new shopItem("Soutien de l'ONU", 750000, 250000, 60, new effectItem("Planter un arbre", new List<int> { 2, 3, 4, 5 }, typeEffectsItem.multiplyGains)));
        shopContent.Add(new shopItem("Elfe protecteur", 5000000, 100000, 15, new effectItem("Planter un arbre", new List<int> { 2, 3, 4, 5 }, typeEffectsItem.multiplyGains)));

        InventoryManager.OnMoneyChange += changeUIMoney;

    }

    void changeUIMoney()
    {
        moneyUI.text = inventory.money.ToString();
    }

    private void OnDestroy()
    {
        Clicker.OnClick -= changeUIMoney;
    }

    //Fonction appelé quand on achete un item
    public void onBuyItem(int id)
    {
        if (inventory.money > shopContent[id].getPrice())
        {
            inventory.money -= shopContent[id].getPrice();
            inventory.itemPlayer[id] += 1;
            Debug.Log(inventory.itemPlayer[id]);
        }
    }
}

public class shopItem
{
    private
    string name;
    ulong price;
    int gain;
    int secondsForGain;
    effectItem effectItem;


    public shopItem(string itemName, ulong itemPrice, int itemGains, int secondsForItemGain, effectItem itemEffects)
    {
        name = itemName;
        price = itemPrice;
        gain = itemGains;
        secondsForGain = secondsForItemGain;
    }

    public string getName() { return name; }
    public ulong getPrice() { return price; }
    public int getGain() { return gain; }
    public int getSecondsForGain() { return secondsForGain; }
}

public class effectItem
{
    private
    string nameItemAffected;
    //gainsMultiplicator stock les multiplicateurs de l'objet cible
    List<int> gainsParameters;
    typeEffectsItem typeEffect;

    public effectItem(string nameItemAffected, List<int> gainsParameters, typeEffectsItem typeEffect)
    {
        this.nameItemAffected = nameItemAffected;
        this.gainsParameters = gainsParameters;
        this.typeEffect = typeEffect;
    }
}

public enum typeEffectsItem
{
    multiplyGains,
    divideCost
}

