using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsManager : BaseManager
{
    public static OptionsManager Instance;

    void Awake() 
    {
        if (!CheckSingletonInstance(this, ref Instance))
        {
            return; // Instance already exists, so the new one is destroyed
        }
    }
}
