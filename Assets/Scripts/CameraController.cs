using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    protected Camera _camera;

    public GameObject _target;
    protected float _speed = 5.0f;

    protected Vector3[] camera_transform;

    protected float minZoom = 4f;
    protected float maxZoom = 50f;
    protected float sensitivity = 10f;

    protected float angleMarge = 1f;

    protected bool canRotateX = true;
    protected bool canRotateY = true;

    protected Ray _ray;
    protected RaycastHit _hit;
    public LayerMask earthLayerMask;

    private void Start()
    {
        _camera = gameObject.GetComponent<Camera>();
        camera_transform = new Vector3[] { transform.position, transform.eulerAngles };
    }

    private void Update()
    {
        Controls();
        if (WorldManager.Instance._TestBuildIsNull() && (GameManager.Instance.State != GameState.Pause))
        {
            if (Input.GetMouseButton(0))
            {
                _ray = _camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(_ray, out _hit, 1000f, earthLayerMask))
                {
                    Debug.DrawLine(_ray.direction, _hit.point, Color.red, 1f);
                    if (canRotateX)
                        transform.RotateAround(_target.transform.position, transform.up, Input.GetAxis("Mouse X") * _speed);
                    if (canRotateY)
                        transform.RotateAround(_target.transform.position, transform.right, Input.GetAxis("Mouse Y") * -_speed);
                }
            }            
            else
            {
                StartCoroutine(WaitSecond(1f));
                if (transform.eulerAngles.z > angleMarge && transform.eulerAngles.z < 180)
                    transform.RotateAround(_target.transform.position, transform.forward, -(_speed / 100));
                else if (transform.eulerAngles.z > 180 && transform.eulerAngles.z < (360 - angleMarge))
                    transform.RotateAround(_target.transform.position, transform.forward, (_speed / 100));
            }
            if (Input.GetMouseButton(1))
            {
                float zoom = _camera.fieldOfView;
                zoom += Input.GetAxis("Mouse ScrollWheel") * -sensitivity;
                zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
                _camera.fieldOfView = zoom;
            }
        }        
    }

    private void Controls()
    {
        /// Touche "A" pour activer
        /// Retourne à la position initial
        if (Input.GetKeyUp(KeyCode.Q))
        {
            _camera.fieldOfView = Mathf.Clamp(20f, minZoom, maxZoom);
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