using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorldManager : BaseManager
{
    public static WorldManager Instance;

    public GameObject EnvironmentPrefab;

    public GameObject Environment { get; private set; }

    public LayerMask groundLayerMask;
    public Camera _mainCamera;
    public Transform _parent;

    protected GameObject _buildingPrefab;
    protected GameObject _toBuild;

    protected Ray _ray;
    protected RaycastHit _hit;

    private void Awake()
    {
        if (!CheckSingletonInstance(this, ref Instance))
        {
            return; // Instance already exists, so the new one is destroyed
        }
        _buildingPrefab = null;
    }

    private void Start()
    {
        GameManager.OnStateChanged += HandleStateChange;
    }

    private void OnDestroy()
    {
        GameManager.OnStateChanged -= HandleStateChange;
    }

    private void Update()
    {
        if (_buildingPrefab != null)
        { // if in build mode

            // right-click: cancel build mode
            if (Input.GetMouseButtonDown(1))
            {
                Destroy(_toBuild);
                _toBuild = null;
                _buildingPrefab = null;
                return;
            }

            // hide preview when hovering UI
            if (EventSystem.current.IsPointerOverGameObject())
            {
                if (_toBuild.activeSelf) _toBuild.SetActive(false);
                return;
            }
            else if (!_toBuild.activeSelf) _toBuild.SetActive(true);

            _ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_ray, out _hit, 1000f, groundLayerMask))
            {
                if (!_toBuild.activeSelf) _toBuild.SetActive(true);
                _toBuild.transform.position = _hit.point;
                _toBuild.transform.rotation = Quaternion.FromToRotation(Vector3.up, _hit.normal) ;

                if (Input.GetMouseButtonDown(0))
                { // if left-click
                    BuildingManager m = _toBuild.GetComponent<BuildingManager>();
                    if (m.hasValidPlacement)
                    {
                        m.SetPlacementMode(PlacementMode.Fixed);

                        _buildingPrefab = null;
                        _toBuild = null;
                    }
                }

            }
            else if (_toBuild.activeSelf) _toBuild.SetActive(false);
        }
    }

    private void HandleStateChange(GameState state)
    {
        // start switch
        switch (state)
        {
            case GameState.Playing:
                InitializeEnvironment();
                break;
            case GameState.Start:
                if (Environment != null)
                    Destroy(Environment);
                break;
            default:
                break;
        }        
    }

    public void InitializeEnvironment()
    {
        if (Environment == null)
        {
            Environment = Instantiate(EnvironmentPrefab, _parent);
        }
        _mainCamera = Environment.transform.Find("Camera").GetComponent<Camera>();
        _parent = Environment.transform.Find("Planets/RotatePlanet").transform;

    }


    public void SetBuildingPrefab(GameObject prefab)
    {
        _buildingPrefab = prefab;
        _PrepareBuilding();
        EventSystem.current.SetSelectedGameObject(null); // cancel keyboard UI nav
    }

    protected virtual void _PrepareBuilding()
    {
        if (_toBuild) Destroy(_toBuild);

        _toBuild = Instantiate(_buildingPrefab, _parent);
        _toBuild.SetActive(false);

        BuildingManager m = _toBuild.GetComponent<BuildingManager>();
        m.isFixed = false;
        m.SetPlacementMode(PlacementMode.Valid);
    }

    public bool _TestBuildIsNull()
    {
        if (_buildingPrefab != null) return false;
        else return true;
    }

}