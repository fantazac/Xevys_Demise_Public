using UnityEngine;

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
