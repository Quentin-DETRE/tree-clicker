using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public GameObject[] _cameras;
    protected int _currentCamera = 0;
    void Start()
    {
        if(_cameras.Length != 0)
        {
            for (int i = 0; i < _cameras.Length; i++)
            {
                _cameras[i].SetActive(false);
            }
            _cameras[_currentCamera].SetActive(true);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && _cameras.Length > 1)
        {
            _cameras[_currentCamera].SetActive(false);
            _currentCamera += 1;
            if(_currentCamera >= _cameras.Length)
            {
                _currentCamera = _currentCamera % _cameras.Length;
            }
            _cameras[_currentCamera].SetActive(true);
        }        
    }
}
