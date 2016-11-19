using UnityEngine;
using System.Collections;

public class RemoveObjectAfterTrigger : MonoBehaviour
{

    private void Start()
    {
        GetComponent<ActivateTrigger>().OnTrigger += RemoveObject;
    }

    private void RemoveObject()
    {
        Destroy(gameObject);
    }
}
