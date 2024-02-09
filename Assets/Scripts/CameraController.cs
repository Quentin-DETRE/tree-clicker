using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    protected Camera camera;

    public GameObject target;
    protected float speed = 5.0f;

    protected Vector3[] camera_transform;

    protected float minZoom = 10f;
    protected float maxZoom = 100f;
    protected float sensitivity = 15f;

    protected float angleMarge = 1f;

    protected bool canRotateX = true;
    protected bool canRotateY = true;

    protected Ray _ray;
    protected RaycastHit _hit;
    public LayerMask earthLayerMask;

    private void Start()
    {
        camera = Camera.main;
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
                    Debug.DrawLine(_ray.direction, _hit.point, Color.red, 1f);
                    if (canRotateX)
                        transform.RotateAround(target.transform.position, transform.up, Input.GetAxis("Mouse X") * speed);
                    if (canRotateY)
                        transform.RotateAround(target.transform.position, transform.right, Input.GetAxis("Mouse Y") * -speed);
                }
            }            
            else
            {
                StartCoroutine(WaitSecond(1f));
                if (transform.eulerAngles.z > angleMarge && transform.eulerAngles.z < 180)
                    transform.RotateAround(target.transform.position, transform.forward, -(speed / 100));
                else if (transform.eulerAngles.z > 180 && transform.eulerAngles.z < (360 - angleMarge))
                    transform.RotateAround(target.transform.position, transform.forward, (speed / 100));
            }
            if (Input.GetMouseButton(1))
            {
                float zoom = camera.fieldOfView;
                zoom += Input.GetAxis("Mouse ScrollWheel") * -sensitivity;
                zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
                camera.fieldOfView = zoom;
            }
        }        
    }

    private void Controls()
    {
        /// Touche "A" pour activer
        /// Retourne à la position initial
        if (Input.GetKeyUp(KeyCode.Q))
        {
            camera.fieldOfView = Mathf.Clamp(20f, minZoom, maxZoom);
            transform.position = camera_transform[0];
            transform.eulerAngles = camera_transform[1];
        }

        /// Touche "Q" pour activer
        /// Active/Désactive la rotation X
        if (Input.GetKeyUp(KeyCode.A))
            canRotateX = !canRotateX;

        /// Touche "W" pour activer
        /// Active/Désactive la rotation Y
        if (Input.GetKeyUp(KeyCode.Z))
            canRotateY = !canRotateY;
    }

    private IEnumerator WaitSecond(float sec)
    {
        yield return new WaitForSeconds(sec);
    }
}