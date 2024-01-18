using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class EconomyManager : BaseManager
{
    public static EconomyManager Instance;

    public ScientificNumber SeedsPerSecond { get; private set; } = new ScientificNumber(0);
    public ScientificNumber SeedsPerClick { get; private set; } = new ScientificNumber(1);


    private Dictionary<string, List<Modifier>> modifiers = new Dictionary<string, List<Modifier>>();

    void Awake() 
    {
        if (!CheckSingletonInstance(this, ref Instance))
        {
            return; // Instance already exists, so the new one is destroyed
        }
    }

    void FixedUpdate()
    {
        InventoryManager.Instance.AddSeeds(SeedsPerSecond * Time.fixedDeltaTime);
    }

    public void UpdateSeedsPerClick()
    {
        ScientificNumber additiveBonus = new ScientificNumber(0);
        double multiplicativeBonus = 1;

        if (modifiers.ContainsKey("click"))
        {
            foreach (var modifier in modifiers["click"])
            {
                if (modifier.type == ModifierType.ClickYield)
                {
                    additiveBonus += modifier.additiveValue;
                    multiplicativeBonus *= 1 + modifier.multiplicativeValue;
                }
            }
        }

        SeedsPerClick = (additiveBonus + 1) * multiplicativeBonus;
    }

    public void ApplyModifier(Modifier modifier)
    {
        string target = modifier.targetUpgradeName;
        if (!modifiers.ContainsKey(target))
        {
            modifiers[target] = new List<Modifier>();
        }

        // Fusionner les modificateurs si un modificateur pour la même cible existe déjà
        bool modifierExists = false;
        for (int i = 0; i < modifiers[target].Count; i++)
        {
            if (modifiers[target][i].type == modifier.type)
            {
                Modifier existingModifier = modifiers[target][i];
                existingModifier.additiveValue += modifier.additiveValue;
                existingModifier.multiplicativeValue += modifier.multiplicativeValue;
                modifiers[target][i] = existingModifier;
                modifierExists = true;
                break;
            }
        }

        if (!modifierExists)
        {
            modifiers[target].Add(modifier);
        }
        if (modifier.type == ModifierType.ClickYield)
        {
            UpdateSeedsPerClick();
        }

        // Mise à jour de la logique économique en fonction des modificateurs
        UpdateSeedsPerSecond();
    }

    public ScientificNumber GetModifiedProduction(string upgradeName, ScientificNumber baseProduction)
    {
        if (!modifiers.ContainsKey(upgradeName))
            return baseProduction;

        ScientificNumber modifiedProduction = baseProduction;
        foreach (var modifier in modifiers[upgradeName])
        {
            if (modifier.type == ModifierType.Production)
            {
                modifiedProduction += (modifier.additiveValue + baseProduction) * modifier.multiplicativeValue;
            }
        }
        return modifiedProduction;
    }

    public void UpdateSeedsPerSecond()
    {
        SeedsPerSecond = new ScientificNumber(0);
        foreach (var upgrade in InventoryManager.Instance.ownedUpgrades)
        {
            var UpgradeObject = UpgradeManager.Instance.GetUpgradeObject(upgrade.Key);
            if (UpgradeObject != null)
            {
                // Calculer la production basée sur le nombre d'upgrades possédés
                var baseProduction = UpgradeObject.productionPerSecond * upgrade.Value;

                // Obtenir la production modifiée
                var modifiedProduction = GetModifiedProduction(upgrade.Key, baseProduction);

                // Ajouter la production modifiée à SeedsPerSecond
                SeedsPerSecond += modifiedProduction;
            }
        }
    }
}