using UnityEngine;
using System.Collections;

public class FollowAttackHitbox : MonoBehaviour
{
    private BoxCollider2D _hitbox;

    void Start()
    {
        _hitbox = GetComponentInParent<BoxCollider2D>();
    }

	void Update ()
    {
	    transform.position = new Vector3(_hitbox.transform.position.x + _hitbox.offset.x, _hitbox.transform.position.y + _hitbox.offset.y, _hitbox.transform.position.z);
	}
}
