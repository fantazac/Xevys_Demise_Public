using UnityEngine;
using System.Collections;

public class ThrowingWeaponHitEnemy : MonoBehaviour
{
    [SerializeField]
    private int _baseDamage = 100;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        /*
         * BEN_REVIEW
         * 
         * OK.
         */
        if (collider.gameObject.tag == "Scarab" ||
            collider.gameObject.tag == "Bat" ||
            collider.gameObject.tag == "Skeltal")
        {
            collider.GetComponent<Health>().Hit(_baseDamage, Vector2.zero);
            GetComponent<DestroyPlayerProjectile>().DestroyNow = true;
        }
    }
}
