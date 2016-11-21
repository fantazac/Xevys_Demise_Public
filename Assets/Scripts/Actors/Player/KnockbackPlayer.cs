using UnityEngine;
using System.Collections;

public class KnockbackPlayer : MonoBehaviour
{
    [SerializeField]
    private float _knockbackSpeed = 5;

    [SerializeField]
    private float _knockbackDuration = 0.25f;

    private const float BEHEMOTH_KNOCKBACK_INCREASE_MODIFIER = 1.8f;

    private WaitForSeconds _knockbackDelay;
    private WaitForSeconds _knockbackBehemothDelay;

    private Rigidbody2D _rigidbody;
    private PlayerState _playerState;

    private void Start()
    {
        _knockbackDelay = new WaitForSeconds(_knockbackDuration);
        _knockbackBehemothDelay = new WaitForSeconds(_knockbackDuration * BEHEMOTH_KNOCKBACK_INCREASE_MODIFIER);

        GetComponent<Health>().OnDamageTakenByEnemy += Knockback;

        _rigidbody = GetComponent<Rigidbody2D>();

        _playerState = StaticObjects.GetPlayerState();
    }

    private void Knockback(Vector2 attackerPosition)
    {
        _playerState.SetKnockedBack(true);

        if (transform.position.x < attackerPosition.x)
        {
            _rigidbody.velocity = new Vector2(-_knockbackSpeed, _rigidbody.velocity.y);
        }
        else if (transform.position.x > attackerPosition.x)
        {
            _rigidbody.velocity = new Vector2(_knockbackSpeed, _rigidbody.velocity.y);
        }
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _knockbackSpeed);

        StartCoroutine(StopKnockback());
    }

    private void KnockbackOnBehemothHit(Vector2 behemothPosition)
    {
        _playerState.SetKnockedBack(true);

        if (transform.position.x < behemothPosition.x)
        {
            _rigidbody.velocity = new Vector2(-_knockbackSpeed * BEHEMOTH_KNOCKBACK_INCREASE_MODIFIER, _rigidbody.velocity.y);
        }
        else if (transform.position.x > behemothPosition.x)
        {
            _rigidbody.velocity = new Vector2(_knockbackSpeed * BEHEMOTH_KNOCKBACK_INCREASE_MODIFIER, _rigidbody.velocity.y);
        }
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _knockbackSpeed * BEHEMOTH_KNOCKBACK_INCREASE_MODIFIER);

        StopAllCoroutines();
        StartCoroutine(StopBehemothKnockback());
    }

    private IEnumerator StopKnockback()
    {
        yield return _knockbackDelay;

        _playerState.SetKnockedBack(false);
        _playerState.SetImmobile();
    }

    private IEnumerator StopBehemothKnockback()
    {
        yield return _knockbackBehemothDelay;

        _playerState.SetKnockedBack(false);
        _playerState.SetImmobile();
    }

    public void EnableBehemothKnockback(GameObject behemoth)
    {
        behemoth.GetComponent<OnBehemothAttackHit>().OnKnockback += KnockbackOnBehemothHit;
    }
}
