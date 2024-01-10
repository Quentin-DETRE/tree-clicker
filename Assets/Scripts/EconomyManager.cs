using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyManager : BaseManager
{
    public static EconomyManager Instance;

    void Awake() 
    {
        if (!CheckSingletonInstance(this, ref Instance))
        {
            return; // Instance already exists, so the new one is destroyed
        }
    }
}
