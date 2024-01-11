using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickerManager : MonoBehaviour
{
    [SerializeField]
    private ShopManager shopManager;
     
    public void treeClicked()
    {
        shopManager.money += 1;
    }
}
