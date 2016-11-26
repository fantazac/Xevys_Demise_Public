using UnityEngine;
using System.Collections.Generic;

public class ActivateMultipleTriggers : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _triggersToActivate;

    private ActivateTrigger _trigger;

    private int _amountOfTriggersToActivate;

    private void Start()
    {
        _trigger = GetComponent<ActivateTrigger>();

        _amountOfTriggersToActivate = _triggersToActivate.Length;

        foreach (GameObject triggerToActivate in _triggersToActivate)
        {
            triggerToActivate.GetComponent<ActivateTrigger>().OnTrigger += HitTrigger;
        }
    }

    private void HitTrigger()
    {
        if (--_amountOfTriggersToActivate == 0)
        {
            _trigger.MultipleTriggersActivated();
        }
    }
}
