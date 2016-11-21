using UnityEngine;
using System.Collections;

public class KnockbackOnDamageTaken : MonoBehaviour
{
    private const float KNOCKBACK_SPEED = 5;
    private const float TIME_DAMAGE_ANIMATION_PLAYS = 0.25f;

    private WaitForSeconds _damageAnimDelay;

    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _damageAnimDelay = new WaitForSeconds(TIME_DAMAGE_ANIMATION_PLAYS);

        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void KnockbackPlayer(Vector2 positionEnemy)
    {
        PlayerState.SetKnockedBack(true);

        if (transform.position.x < positionEnemy.x)
        {
            _rigidbody.velocity = new Vector2(-KNOCKBACK_SPEED, _rigidbody.velocity.y);
        }
        else if (transform.position.x > positionEnemy.x)
        {
            _rigidbody.velocity = new Vector2(KNOCKBACK_SPEED, _rigidbody.velocity.y);
        }
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, KNOCKBACK_SPEED);

        StartCoroutine(StopDamageAnimation());
    }

    private IEnumerator StopDamageAnimation()
    {
        yield return _damageAnimDelay;

        PlayerState.SetKnockedBack(false);
        PlayerState.SetImmobile();
    }
}
