using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectDestruction : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Detruction());
    }

    private IEnumerator Detruction()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
