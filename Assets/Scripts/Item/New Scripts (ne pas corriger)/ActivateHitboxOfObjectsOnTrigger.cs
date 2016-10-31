using UnityEngine;
using System.Collections;

public class ActivateHitboxOfObjectsOnTrigger : MonoBehaviour
{

    [SerializeField]
    private GameObject[] _hitboxesToActivate;

    private ActivateTrigger _trigger;

    private void Start()
    {
        _trigger = GetComponent<ActivateTrigger>();
        _trigger.OnTrigger += Activate;
    }

    private void Activate()
    {
        foreach (GameObject hitbox in _hitboxesToActivate)
        {
            hitbox.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
