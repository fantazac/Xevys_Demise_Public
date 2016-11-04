using UnityEngine;

public class WaterDetectorAssignator : MonoBehaviour {

	void Start () {
	    foreach (GameObject collider in GetComponentInChildren<Water>().Colliders)
	    {
	        collider.AddComponent<WaterDetector>();
	    }
	}
}
