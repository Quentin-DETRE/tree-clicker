using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : BaseManager
{
    public static EventManager Instance;

    void Awake() 
    {
        if (!CheckSingletonInstance(this, ref Instance))
        {
            return; // Instance already exists, so the new one is destroyed
        }
    }
}
