using UnityEngine;
using System.Collections;

public class RemoveObjectAfterTrigger : MonoBehaviour
{

    private ActivateTrigger _trigger;

    private void Start()
    {
        _trigger = GetComponent<ActivateTrigger>();
        _trigger.OnTrigger += RemoveObject;
    }

    private void RemoveObject()
    {
        Destroy(gameObject);
    }

}
