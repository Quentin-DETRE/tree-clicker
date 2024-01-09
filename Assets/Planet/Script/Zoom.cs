using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    [SerializeField] private Camera camera;

    float zoom = 10f;

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
    }

    private void Update()
    {
        if(camera.orthographic)
        {
            camera.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * zoom;
        }
        else
        {
            camera.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * zoom;
        }
    }
}
