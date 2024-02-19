using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbiteController : MonoBehaviour
{
    public Transform planet; // Référence à l'objet planète
    protected Camera _camera;
    protected Vector3[] camera_transform;

    protected float rotationSpeed = 20.0f; // Vitesse de rotation

    protected float minZoom = 30f;
    protected float maxZoom = 70f;
    protected float sensitivity = 10f;

    private void Start()
    {
        _camera = gameObject.GetComponent<Camera>();
        camera_transform = new Vector3[] { transform.position, transform.eulerAngles };
    }

    void Update()
    {
        // Rotation autour de la planète
        transform.RotateAround(planet.position, -(Vector3.up + Vector3.right * 2f), rotationSpeed * Time.deltaTime);

        if (Input.GetMouseButton(1))
        {
            float zoom = _camera.fieldOfView;
            zoom += Input.GetAxis("Mouse ScrollWheel") * -sensitivity;
            zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
            _camera.fieldOfView = zoom;
        }
    }

    private void OnDisable()
    {
        _camera.fieldOfView = Mathf.Clamp(60f, minZoom, maxZoom);
        transform.position = camera_transform[0];
        transform.eulerAngles = camera_transform[1];
    }
}
