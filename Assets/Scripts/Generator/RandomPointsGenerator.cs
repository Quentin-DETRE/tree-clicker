using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RandomPointsGenerator", menuName = "Custom/Random Points Generator")]
public class RandomPointsGenerator : ScriptableObject
{
    public int numberOfPositions = 100; // Nombre de positions aléatoires à générer
    public float planetRadius = 10f; // Rayon de la planète
    public float minDistance = 1f; // Marge minimale entre les points

    public void GenerateRandomSurfacePositions(Transform parent, GameObject prefab)
    {
        for (int i = 0; i < numberOfPositions; i++)
        {
            Vector3 randomPoint = Random.onUnitSphere * planetRadius;

            RaycastHit hit;
            if (Physics.Raycast(randomPoint, -randomPoint.normalized, out hit, Mathf.Infinity))
            {
                // Dessiner une ligne pour visualiser le raycast
                Debug.DrawLine(randomPoint, hit.point, Color.red, 1f);

                if (hit.collider.CompareTag("Earth"))
                {
                    bool validPosition = true;
                    Collider[] colliders = Physics.OverlapSphere(hit.point, minDistance);
                    foreach (var collider in colliders)
                    {
                        if (collider.CompareTag("Waypoint"))
                        {
                            validPosition = false;
                            break;
                        }
                    }

                    if (validPosition)
                    {
                        // Calculer la rotation en fonction de la normale de la surface
                        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                        // Instancier l'objet avec la rotation calculée
                        Instantiate(prefab, hit.point, rotation, parent);
                    }
                }
            }
        }
    }
}
