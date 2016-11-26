using UnityEngine;
using System.Collections;

public class ActivateObjectsOnTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _objectsToActivate;

    private void Start()
    {
        GetComponent<ActivateTrigger>().OnTrigger += Activate;
    }

    private void Activate()
    {
        foreach (GameObject objectToActivate in _objectsToActivate)
        {
            objectToActivate.SetActive(true);
        }
    }
}
