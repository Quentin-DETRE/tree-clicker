using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorRandomPointsGenerator : MonoBehaviour
{
    public RandomPointsGenerator pointsGenerator;
    public GameObject parent;
    public GameObject pointPrefab;

    //public void Start()
    //{
    //    pointsGenerator.GenerateRandomSurfacePositions(parent.transform, pointPrefab);
    //}

    public void GenerateRandomPoints()
    {
        pointsGenerator.GenerateRandomSurfacePositions(parent.transform, pointPrefab);
    }
}

