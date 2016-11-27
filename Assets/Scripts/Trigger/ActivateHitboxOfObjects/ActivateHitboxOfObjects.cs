using UnityEngine;
using System.Collections;

public class ActivateHitboxOfObjects : MonoBehaviour
{
    [SerializeField]
    protected GameObject[] _hitboxesToActivate;

    [SerializeField]
    protected bool _deactivateHitboxes = false;

    protected void Activate()
    {
        foreach (GameObject hitbox in _hitboxesToActivate)
        {
            hitbox.GetComponent<BoxCollider2D>().enabled = !_deactivateHitboxes;
        }
    }
}
