using UnityEngine;
using System.Collections;

public class ActivateHitboxOfObjectsOnPlayerDeath : MonoBehaviour
{

    [SerializeField]
    private GameObject[] _hitboxesToActivate;

    [SerializeField]
    private bool _deactivateHitboxes = false;

    private Health _playerHealth;

    private void Start()
    {
        _playerHealth = StaticObjects.GetPlayer().GetComponent<Health>();
        _playerHealth.OnDeath += Activate;
    }

    private void Activate()
    {
        foreach (GameObject hitbox in _hitboxesToActivate)
        {
            hitbox.GetComponent<BoxCollider2D>().enabled = !_deactivateHitboxes;
        }
    }

}
