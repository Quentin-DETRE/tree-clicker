using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotation : MonoBehaviour
{
    private Vector3 RotationPoint;

    private bool rotate = true;

    private void Start()
    {
        RotationPoint = new Vector3(0.0f, 7.5f, 0.0f);
    }

    private void Update()
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
