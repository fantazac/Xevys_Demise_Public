using UnityEngine;
using System.Collections;

public class KnockbackOnDamageTaken : MonoBehaviour
{
    [SerializeField]
    private float _knockbackSpeed = 5;

    [SerializeField]
    private float _knockbackDuration = 0.25f;

    private WaitForSeconds _knockbackDelay;

    private Rigidbody2D _rigidbody;
    private PlayerState _playerState;

    private void Start()
    {
        _knockbackDelay = new WaitForSeconds(_knockbackDuration);
        GetComponent<Health>().OnDamageTakenByEnemy += KnockbackPlayer;

        _rigidbody = GetComponent<Rigidbody2D>();

        _playerState = StaticObjects.GetPlayerState();
    }

    private void KnockbackPlayer(Vector2 attackerPosition)
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

    private IEnumerator StopKnockback()
    {
        yield return _knockbackDelay;

        _playerState.SetKnockedBack(false);
        _playerState.SetImmobile();
    }
}
