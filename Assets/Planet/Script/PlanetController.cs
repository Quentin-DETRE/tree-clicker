using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    public Vector3 planetRotate;

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Rotate(planetRotate * Time.deltaTime);
    }
}
