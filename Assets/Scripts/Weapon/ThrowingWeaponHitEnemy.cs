using UnityEngine;
using System.Collections;

public class ThrowingWeaponHitEnemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Scarab" ||
            collider.gameObject.tag == "Bat" ||
            collider.gameObject.tag == "Skeltal")
        {
            GetComponent<DestroyWeapon>().DestroyNow = true;
        }
    }
}
