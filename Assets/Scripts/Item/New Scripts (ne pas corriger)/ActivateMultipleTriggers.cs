using UnityEngine;
using System.Collections.Generic;

public class ActivateMultipleTriggers : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _triggersToActivate;

    private int _amountOfTriggersToActivate;

    private void Start()
    {
        _amountOfTriggersToActivate = _triggersToActivate.Length;

        foreach (GameObject triggerToActivate in _triggersToActivate)
        {
            triggerToActivate.GetComponent<ActivateTrigger>().OnTrigger += HitTrigger;
        }
    }

    private void HitTrigger()
    {
        _amountOfTriggersToActivate--;
        if (_amountOfTriggersToActivate == 0)
        {
            GetComponent<ActivateTrigger>().MultipleTriggersActivated();
        }
    }
}
