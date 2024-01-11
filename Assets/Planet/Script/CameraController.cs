using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Camera camera;

    public GameObject target;
    float speed = 5.0f;

    float minZoom = 10f;
    float maxZoom = 100f;
    float sensitivity = 17f;

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            transform.RotateAround(target.transform.position, transform.up, Input.GetAxis("Mouse X") * speed);
            transform.RotateAround(target.transform.position, transform.right, Input.GetAxis("Mouse Y") * -speed);
        }

        float zoom = camera.fieldOfView;
        zoom += Input.GetAxis("Mouse ScrollWheel") * -sensitivity;
        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        camera.fieldOfView = zoom;
    }
}
