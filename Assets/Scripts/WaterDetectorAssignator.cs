using UnityEngine;
using Assets.Scripts;

/// <summary>
/// Cette classe assigne les boîtes de collisions à tous les segments verticaux de notre surface d'eau.
/// Ce script est executé après le "delta time".
/// </summary>
public class WaterDetectorAssignator : MonoBehaviour
{

    private void Start()
    {
        foreach (GameObject collider in GetComponentInChildren<Water>().Colliders)
        {
            collider.AddComponent<WaterDetector>();
        }
    }
}
