using UnityEngine;
using System.Collections;

public class ThrowingWeaponHitEnemy : MonoBehaviour
{
    [SerializeField]
    private int _baseDamage = 100;

    private UnityTags _unityTags;

    private void Start()
    {
        _unityTags = StaticObjects.GetUnityTags();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == _unityTags.Scarab ||
            collider.gameObject.tag == _unityTags.Bat ||
            collider.gameObject.tag == _unityTags.Skeltal)
        {
            collider.GetComponent<Health>().Hit(_baseDamage, Vector2.zero);
            GetComponent<DestroyPlayerProjectile>().DestroyNow = true;
        }
    }
}
