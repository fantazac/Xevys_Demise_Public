using UnityEngine;
using System.Collections;

public class ActivateHitboxOfObjectsOnBreakableItemDestroyed : MonoBehaviour
{

    [SerializeField]
    private GameObject[] _hitboxesToActivate;

    [SerializeField]
    private bool _deactivateHitboxes = false;

    private void Start()
    {
        GetComponent<Health>().OnDeath += Activate;
    }

    private void Activate()
    {
        foreach (GameObject hitbox in _hitboxesToActivate)
        {
            hitbox.GetComponent<BoxCollider2D>().enabled = !_deactivateHitboxes;
        }
    }
}
