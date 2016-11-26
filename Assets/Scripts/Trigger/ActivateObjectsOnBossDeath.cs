using UnityEngine;
using System.Collections;

public class ActivateObjectsOnBossDeath : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _objectsToActivate;

    private void Start()
    {
        GetComponent<Health>().OnDeath += Activate;
    }

    private void Activate()
    {
        foreach (GameObject objectToActivate in _objectsToActivate)
        {
            objectToActivate.SetActive(true);
        }
    }
}
