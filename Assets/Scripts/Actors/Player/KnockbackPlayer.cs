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
        SetupKnockback(attackerPosition);
        StartCoroutine(StopKnockback());
    }

    private void SetupKnockback(Vector2 attackerPosition)
    {
        _playerState.SetKnockedBack(true);
        if (attackerPosition != Vector2.zero)
        {
            if (transform.position.x != attackerPosition.x)
            {
                _rigidbody.velocity = _knockbackSpeed * (transform.position.x < attackerPosition.x ?
                    Vector2.left : Vector2.right);
            }

            _rigidbody.velocity += Vector2.up * _knockbackSpeed;
        }
        StopAllCoroutines();
    }

    private void KnockbackOnBehemothHit(Vector2 behemothPosition)
    {
        SetupKnockback(behemothPosition);
        _rigidbody.velocity *= BEHEMOTH_KNOCKBACK_INCREASE_MODIFIER;
        StartCoroutine(StopBehemothKnockback());
    }

    private void SetPlayerImmobile()
    {
        _playerState.SetKnockedBack(false);
        _playerState.SetImmobile();
    }

    private IEnumerator StopKnockback()
    {
        yield return _knockbackDelay;

        SetPlayerImmobile();
    }

    private IEnumerator StopBehemothKnockback()
    {
        yield return _knockbackBehemothDelay;

        SetPlayerImmobile();
    }

    public void EnableBehemothKnockback(GameObject behemoth)
    {
        behemoth.GetComponent<OnBehemothAttackHit>().OnKnockback += KnockbackOnBehemothHit;
    }
}
