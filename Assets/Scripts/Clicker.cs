using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicker : MonoBehaviour
{


    public static event System.Action OnClick;

    public void treeClicked()
    {
        OnClick?.Invoke();
    }
}
