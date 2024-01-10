using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : BaseManager
{
    public static UpgradeManager Instance;

    void Awake() 
    {
        if (!CheckSingletonInstance(this, ref Instance))
        {
            return; // Instance already exists, so the new one is destroyed
        }
    }
}
