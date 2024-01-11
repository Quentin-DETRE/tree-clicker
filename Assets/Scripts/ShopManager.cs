using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public int money = 0;

    public Text moneyUI;

    private int maxMoneyPlayer;
    //shopContent va stocker toutes les am�liorations du shop, sous la forme [nom, [prix, gainArgent/secondes, argentMinPourAfficher]]
    private List<shopItem> shopContent;

    private Dictionary<string, int> itemPlayer;

    private void Start()
    {
        //maxMoneyPlayer d�signe le maximum d'argent que le joueur � eu, qui perment d'afficher les am�liorations suivantes
        maxMoneyPlayer = money;


        moneyUI.text = money.ToString();

        //On cr�e les objets disponibles dans le shop
        shopContent = new List<shopItem>();

        //On ajoute au shop les items sous la forme (nom, prix, gain, temps pour le gains, effects ult�rieurs (nom de l'item affect�, liste des multiplicateurs de gains))
        shopContent.Add(new shopItem("Arbre", 10, 1, 15, new effectItem("Arbre", new List<int> { 2, 3, 4, 5 })));
        shopContent.Add(new shopItem("Volontaire", 50, 9, 30, new effectItem("Planter un arbre", new List<int> { 2, 3, 4, 5 })));
        shopContent.Add(new shopItem("Paysan", 300, 50, 30, new effectItem("Planter un arbre", new List<int> { 2, 3, 4, 5 })));
        shopContent.Add(new shopItem("Tracteur", 1000, 340, 60, new effectItem("Planter un arbre", new List<int> { 2, 3, 4, 5 })));
        shopContent.Add(new shopItem("Ruche d'abeilles", 4000, 666, 30, new effectItem("Planter un arbre", new List<int> { 2, 3, 4, 5 })));
        shopContent.Add(new shopItem("�olienne", 9000, 3000, 60, new effectItem("Planter un arbre", new List<int> { 2, 3, 4, 5 })));
        shopContent.Add(new shopItem("�quipe de Jardiniers", 12000, 2000, 30, new effectItem("Planter un arbre", new List<int> { 2, 3, 4, 5 })));
        shopContent.Add(new shopItem("R�serve d'Eau", 27000, 9000, 60, new effectItem("Planter un arbre", new List<int> { 2, 3, 4, 5 })));
        shopContent.Add(new shopItem("Serre", 49000, 4100, 15, new effectItem("Planter un arbre", new List<int> { 2, 3, 4, 5 })));
        shopContent.Add(new shopItem("Collecte de Pluie", 66666, 11111, 30, new effectItem("Planter un arbre", new List<int> { 2, 3, 4, 5 })));
        shopContent.Add(new shopItem("Puits d'Irrigation", 99999, 8500, 15, new effectItem("Planter un arbre", new List<int> { 2, 3, 4, 5 })));
        shopContent.Add(new shopItem("Association green it", 135000, 225000, 30, new effectItem("Planter un arbre", new List<int> { 2, 3, 4, 5 })));
        shopContent.Add(new shopItem("Drones de Plantation", 200000, 70000, 60, new effectItem("Planter un arbre", new List<int> { 2, 3, 4, 5 })));
        shopContent.Add(new shopItem("R�pulsif", 275000, 459000, 30, new effectItem("Planter un arbre", new List<int> { 2, 3, 4, 5 })));
        shopContent.Add(new shopItem("Engrais", 320000, 108000, 60, new effectItem("Planter un arbre", new List<int> { 2, 3, 4, 5 })));
        shopContent.Add(new shopItem("Laboratoire de Recherche �cologique", 390000, 130000, 60, new effectItem("Planter un arbre", new List<int> { 2, 3, 4, 5 })));
        shopContent.Add(new shopItem("R�gulateur d'humiditi�", 450000, 37500, 15, new effectItem("Planter un arbre", new List<int> { 2, 3, 4, 5 })));
        shopContent.Add(new shopItem("Modification g�n�tique d'arbres", 600000, 200000, 30, new effectItem("Planter un arbre", new List<int> { 2, 3, 4, 5 })));
        shopContent.Add(new shopItem("Soutien de l'ONU", 750000, 250000, 60, new effectItem("Planter un arbre", new List<int> { 2, 3, 4, 5 })));
        shopContent.Add(new shopItem("elfe protecteur", 5000000, 100000, 15, new effectItem("Planter un arbre", new List<int> { 2, 3, 4, 5 })));

        itemPlayer = new Dictionary<string, int>();
        itemPlayer.Add("Planter un arbre",0);
        itemPlayer.Add("Volontaire", 0);
        itemPlayer.Add("Paysan", 0);
        itemPlayer.Add("Tracteur", 0);
        itemPlayer.Add("Ruche d'abeilles", 0);
        itemPlayer.Add("�olienne", 0);
        itemPlayer.Add("�quipe de Jardiniers",0);
        itemPlayer.Add("R�serve d'Eau", 0);
        itemPlayer.Add("Serre", 0);
        itemPlayer.Add("Drones de Plantation",0);
        itemPlayer.Add("Puits d'Irrigation", 0);
        itemPlayer.Add("Association green it",0);
        itemPlayer.Add("Collecte de Pluie", 0);
        itemPlayer.Add("R�pulsif",0);
        itemPlayer.Add("Engrais", 0);
        itemPlayer.Add("Fertilisant Organique", 0);
        itemPlayer.Add("Laboratoire de Recherche �cologique", 0);
        itemPlayer.Add("Modification g�n�tique d'arabes", 0);
        itemPlayer.Add("Soutien de l'ONU", 0);
        itemPlayer.Add("Dieux elfe des bois", 0);
    }

    //Fonction appel� quand on achete un item
    void onBuyItem(string itemName)
    {

    }

    // Update is called once per frame
    void Update()
    {
        moneyUI.text = money.ToString();
        //Debug.Log(money);
        if (money > maxMoneyPlayer)
        {
            maxMoneyPlayer = money;
        }
    
    }
}

public class shopItem
{
    private
    string name;
    int price;
    int gain;
    int secondsForGain;
    effectItem effectItem;


    public shopItem(string itemName, int itemPrice, int itemGains, int secondsForItemGain, effectItem itemEffects)
    {
        name = itemName;
        price = itemPrice;
        gain = itemGains;
        secondsForGain = secondsForItemGain;
    }
}

public class effectItem
{
    private
    string nameItemAffected;
    //gainsMultiplicator stock les multiplicateurs de l'objet cible
    List<int> gainsMultiplicator;

    public effectItem(string nameItemAffected, List<int> gainsMultiplicator)
    {
        this.nameItemAffected = nameItemAffected;
        this.gainsMultiplicator = gainsMultiplicator;
    }
}
