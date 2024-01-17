using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : BaseManager
{
    public static InventoryManager Instance;
    public ScientificNumber Seeds { get; private set; } = new ScientificNumber(10);
    public ScientificNumber maxSeedsPlayerHad { get; private set; } = new ScientificNumber(10);

    public Dictionary<string, int> ownedUpgrades = new Dictionary<string, int>();


    public static event System.Action<ScientificNumber> OnSeedsChanged;
    void Awake() 
    {
        if (!CheckSingletonInstance(this, ref Instance))
        {
            return; // Instance already exists, so the new one is destroyed
        }
    }

    public void AddSeeds(ScientificNumber amount)
    {
        Seeds =  Seeds + amount;
        // Mettre à jour l'UI ou d'autres systèmes ici
        maxSeedsPlayerHad = maxSeedsPlayerHad + amount;
        OnSeedsChanged?.Invoke(Seeds);
    }

    public void RemoveSeeds(ScientificNumber amount)
    {
        Seeds -= amount;
        // Mettre à jour l'UI ou d'autres systèmes ici
        OnSeedsChanged?.Invoke(Seeds);
    }

    public int GetOwnedUpgradeCount(string upgradeName)
    {
        if (ownedUpgrades.TryGetValue(upgradeName, out int count))
        {
            return count;
        }
        return 0;
    }
}
