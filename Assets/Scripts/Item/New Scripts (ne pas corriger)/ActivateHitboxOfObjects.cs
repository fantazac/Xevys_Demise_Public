using UnityEngine;
using System.Collections;

public class ActivateHitboxOfObjects : MonoBehaviour
{

    [SerializeField]
    private GameObject[] _hitboxesToActivate;

    public void Activate()
    {
        foreach (GameObject hitbox in _hitboxesToActivate)
        {
            hitbox.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
