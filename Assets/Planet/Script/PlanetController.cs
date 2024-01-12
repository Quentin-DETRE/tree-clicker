using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    public Vector3 planetRotate;

    bool rotate = true;

    // Update is called once per frame
    void Update()
    {
        if (rotate)
        {
            transform.Rotate(planetRotate * Time.deltaTime);
        }
        if (Input.GetMouseButtonUp(1))
        {
            rotate = !rotate;
        }
    }
}
