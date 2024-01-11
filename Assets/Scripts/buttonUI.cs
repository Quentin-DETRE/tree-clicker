using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class buttonUI : MonoBehaviour
{
    public int itemId;
    public Text itemCountUI;

    private int currentItemCount;
    private void Start()
    {
        currentItemCount = InventoryManager.Instance.itemPlayer[itemId];
        itemCountUI.text = currentItemCount.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        if (InventoryManager.Instance.itemPlayer[itemId] < currentItemCount)
        {
            currentItemCount = InventoryManager.Instance.itemPlayer[itemId];
            itemCountUI.text = currentItemCount.ToString();
        }
    }
}
