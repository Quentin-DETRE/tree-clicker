using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorldManager : BaseManager
{
    public static WorldManager Instance;

    public GameObject EnvironmentPrefab;
    public GameObject Environment { get; private set; }

    protected GameObject _buildingPrefab;
    protected GameObject _toBuild;
    protected Transform _parent;
    public Transform[] _buildWaypointsList;
    public GameObject[] _prefabUpgrade;

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
        { 
            if (Input.GetMouseButtonDown(1))
            {
                Destroy(_toBuild);
                _toBuild = null;
                _buildingPrefab = null;
                return;
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
            Environment = Instantiate(EnvironmentPrefab);
        }

        Transform[] childTransform = Environment.GetComponentsInChildren<Transform>();

        for (int i = 0; i < childTransform.Length; i++)
        {
            if (childTransform[i].CompareTag("Build"))
            {
                _parent = childTransform[i];
                break;
            }
        }
        _buildWaypointsList = _parent.GetComponentsInChildren<Transform>();
    }

    public void RandomPlacementButtonClick(GameObject prefab)
    {
        SetBuildingPrefab(prefab);

        if (_buildingPrefab != null)
        {
            int random = Random.Range(0, _buildWaypointsList.Length);
            // Instancier l'objet et le placer
            _toBuild = Instantiate(_buildingPrefab, _buildWaypointsList[random].position, _buildWaypointsList[random].rotation, _parent);
            _toBuild.SetActive(true);


            BuildingManager m = _toBuild.GetComponent<BuildingManager>();
            m.SetPlacementMode(PlacementMode.Fixed);

            _buildingPrefab = null;
            _toBuild = null;
        }
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