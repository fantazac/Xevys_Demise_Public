using UnityEngine;
using System.Collections;

public class ActivateHitboxOfObjectsOnTrigger : MonoBehaviour
{

    [SerializeField]
    private GameObject[] _hitboxesToActivate;

    private void Start()
    {
        GetComponent<ActivateTrigger>().OnTrigger += Activate;
    }

    private void Activate()
    {
        foreach (GameObject hitbox in _hitboxesToActivate)
        {
            hitbox.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
