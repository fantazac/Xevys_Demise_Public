using UnityEngine;
using System.Collections;

public class ThrowingWeaponHitEnemy : MonoBehaviour
{
    [SerializeField]
    private int _baseDamage = 100;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Scarab" ||
            collider.gameObject.tag == "Bat" ||
            collider.gameObject.tag == "Skeltal")
        {
            collider.GetComponent<Health>().Hit(_baseDamage);
            GetComponent<DestroyWeapon>().DestroyNow = true;
        }
    }
}
