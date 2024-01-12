using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : BaseManager
{
    public static InventoryManager Instance;
    public ulong money;
    public ulong maxMoneyPlayerHad;
    public List<int> itemPlayer;

    public static event System.Action OnMoneyChange;
    void Awake() 
    {
        if (!CheckSingletonInstance(this, ref Instance))
        {
            return; // Instance already exists, so the new one is destroyed
        }

        //maxMoneyPlayer d�signe le maximum d'argent que le joueur � eu, qui perment d'afficher les am�liorations suivantes
        maxMoneyPlayerHad = money;


        //Ceci est l'inventaire temporaire du joueur, � sauvegarder ailleurs
        itemPlayer = new List<int>() { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 };
        //itemPlayer = new Dictionary<string, int>();
        //itemPlayer.Add("Arbre", 0);
        //itemPlayer.Add("Volontaire", 0);
        //itemPlayer.Add("Paysan", 0);
        //itemPlayer.Add("Tracteur", 0);
        //itemPlayer.Add("Ruche d'abeilles", 0);
        //itemPlayer.Add("�olienne", 0);
        //itemPlayer.Add("�quipe de Jardiniers", 0);
        //itemPlayer.Add("R�serve d'Eau", 0);
        //itemPlayer.Add("Serre", 0);
        //itemPlayer.Add("Collecte de Pluie", 0);
        //itemPlayer.Add("Puits d'Irrigation", 0);
        //itemPlayer.Add("Association green it", 0);
        //itemPlayer.Add("Drones de Plantation", 0);
        //itemPlayer.Add("R�pulsif", 0);
        //itemPlayer.Add("Engrais", 0);
        //itemPlayer.Add("Laboratoire de Recherche �cologique", 0);
        //itemPlayer.Add("R�gulateur d'humiditi�", 0);
        //itemPlayer.Add("Modification g�n�tique d'arabes", 0);
        //itemPlayer.Add("Soutien de l'ONU", 0);
        //itemPlayer.Add("Elfe protecteur", 0);
    }

    private void Start()
    {
        Clicker.OnClick += addMoney;
    }

    private void OnDestroy()
    {
        Clicker.OnClick -= addMoney;
    }

    private void Update()
    {
        if (money > maxMoneyPlayerHad)
        {
            maxMoneyPlayerHad = money;
        }


    }
    private void addMoney()
    {
        money++;
        OnMoneyChange?.Invoke();

    }
    public void addItem(int id)
    {
        itemPlayer[id] += 1;
        
    }


}
