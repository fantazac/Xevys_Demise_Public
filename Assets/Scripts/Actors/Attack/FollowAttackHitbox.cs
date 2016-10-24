using UnityEngine;
using System.Collections;

/* BEN_REVIEW
 * 
 * C'est pour qu'un objet soit à la même position que la Hitbox ? Pas clair comme nom si c'est le cas. Aussi, pas à la bonne
 * place. Enfin, il est possible de le généraliser.
 */
public class FollowAttackHitbox : MonoBehaviour
{
    private BoxCollider2D _hitbox;

    void Start()
    {
        _hitbox = GetComponentInParent<BoxCollider2D>();
    }

    void Update()
    {
        transform.position = new Vector3(_hitbox.transform.position.x + _hitbox.offset.x, _hitbox.transform.position.y + _hitbox.offset.y, _hitbox.transform.position.z);
    }
}
