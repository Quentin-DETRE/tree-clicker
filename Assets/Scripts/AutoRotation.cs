using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotation : MonoBehaviour
{
    public Vector3 RotationPoint;

    bool rotate = true;

    void Update()
    {
        if (WorldManager.Instance._TestBuildIsNull())
        {
            if (rotate)
            {
                transform.Rotate(RotationPoint * Time.deltaTime);
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            rotate = !rotate;
        }
    }
}
