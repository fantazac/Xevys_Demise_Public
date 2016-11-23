using UnityEngine;
using System.Collections;

public class BatPlayerDetector : MonoBehaviour
{
    private BoxCollider2D _hitbox;

    public delegate void OnDetectedPlayerHandler();
    public event OnDetectedPlayerHandler OnDetectedPlayer;

    private void Start()
    {
        _hitbox = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == StaticObjects.GetUnityTags().Player)
        {
            DisableHitbox();
            OnDetectedPlayer();
        }
    }

    public void EnableHitbox()
    {
        _hitbox.enabled = true;
        _hitbox.isTrigger = true;
    }

    private void DisableHitbox()
    {
        _hitbox.isTrigger = false;
        _hitbox.enabled = false;
    }
}
