using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Camera camera;

    public GameObject target;
    float speed = 5.0f;

    Vector3[] camera_transform;

    float minZoom = 10f;
    float maxZoom = 100f;
    float sensitivity = 15f;

    float angleMarge = 1f;

    bool canRotateX = true;
    bool canRotateY = true;

    protected Ray _ray;
    protected RaycastHit _hit;
    public LayerMask earthLayerMask;

    public void Start()
    {
        camera_transform = new Vector3[] { transform.position, transform.eulerAngles };
    }


    private void Update()
    {
        Controls();
        if (WorldManager.Instance._TestBuildIsNull() && (GameManager.Instance.State != GameState.Pause))
        {
            if (Input.GetMouseButton(0))
            {
                _ray = camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(_ray, out _hit, 1000f, earthLayerMask))
                {
                    if (canRotateX)
                        transform.RotateAround(target.transform.position, transform.up, Input.GetAxis("Mouse X") * speed);
                    if (canRotateY)
                        transform.RotateAround(target.transform.position, transform.right, Input.GetAxis("Mouse Y") * -speed);
                }
            }            
            else
            {
                StartCoroutine(WaitOneSecond());
                if (transform.eulerAngles.z > angleMarge && transform.eulerAngles.z < 180)
                    transform.RotateAround(target.transform.position, transform.forward, -(speed / 100));
                else if (transform.eulerAngles.z > 180 && transform.eulerAngles.z < (360 - angleMarge))
                    transform.RotateAround(target.transform.position, transform.forward, (speed / 100));
            }
            float zoom = camera.fieldOfView;
            zoom += Input.GetAxis("Mouse ScrollWheel") * -sensitivity;
            zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
            camera.fieldOfView = zoom;
        }        
    }

    public void Controls()
    {
        /// Touche "A" pour activer
        /// Retourne � la position initial
        if (Input.GetKeyUp(KeyCode.Q))
        {
            camera.fieldOfView = Mathf.Clamp(20f, minZoom, maxZoom);
            transform.position = camera_transform[0];
            transform.eulerAngles = camera_transform[1];
        }

        /// Touche "Q" pour activer
        /// Active/D�sactive la rotation X
        if (Input.GetKeyUp(KeyCode.A))
            canRotateX = !canRotateX;

        /// Touche "W" pour activer
        /// Active/D�sactive la rotation Y
        if (Input.GetKeyUp(KeyCode.Z))
            canRotateY = !canRotateY;
    }

    public IEnumerator WaitOneSecond()
    {
        yield return new WaitForSeconds(1.0f);
    }
}