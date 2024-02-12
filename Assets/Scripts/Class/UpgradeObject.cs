using UnityEngine;

[System.Serializable]
public class UpgradeArray
{
    public UpgradeObject[] upgrades;
}


[System.Serializable]
public class UpgradeObject
{
    public int id;
    public string upgradeName;
    public string spriteName;
    [SerializeField]
    public ScientificNumber cost;
    [SerializeField]
    public ScientificNumber productionPerSecond;
    public UpgradeRequirement[] requirements;
    public UpgradeStep[] upgradeSteps;
}

[System.Serializable]
public struct UpgradeRequirement
{
    public string requiredUpgradeName;
    public int requiredAmount;
}

[System.Serializable]
public struct UpgradeStep
{
    public int threshold;
    public Modifier modifier;

    public string description;
}

[System.Serializable]
public struct Modifier
{
    public ModifierType type;
    public string targetUpgradeName; 
    [SerializeField] 
    public ScientificNumber additiveValue;
    public double multiplicativeValue;
}

public enum ModifierType
{
    Price, Production, ClickYield
}