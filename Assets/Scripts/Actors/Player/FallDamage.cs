using UnityEngine;
using System.Collections;

public class FallDamage: MonoBehaviour
{
    [SerializeField]
    private float _timeBeforeHit = 0.75f;
    [SerializeField]
    private float _damageMultiplier = 100f;

    private Health _playerHealth;
    private PlayerMovement _playerMovement;

    private float _fallingCount = 0;

    private void Start()
    {
        _playerHealth = GetComponent<Health>();
        _playerMovement = GetComponent<PlayerGroundMovement>();

        _playerMovement.OnFalling += OnFalling;
        _playerMovement.OnLanding += OnLanding;
    }

    private void OnFalling()
    {
        _fallingCount += Time.deltaTime;
    }

    private void OnLanding()
    {
        if (_fallingCount > _timeBeforeHit && !StaticObjects.GetPlayerState().IsInvincible && _playerHealth.HealthPoint > 0)
        {
            _playerHealth.Hit((int)Mathf.Clamp(_fallingCount * _damageMultiplier, 
                _fallingCount * _damageMultiplier, _playerHealth.HealthPoint), 
                new Vector2(transform.position.x, transform.position.y - 1));
        }

        _fallingCount = 0;
    }
}
