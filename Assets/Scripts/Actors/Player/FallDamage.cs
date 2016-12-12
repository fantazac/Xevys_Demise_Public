using UnityEngine;
using System.Collections;

public class FallDamage: MonoBehaviour
{
    [SerializeField]
    private int _timeBeforeHit = 1;

    [SerializeField]
    private int _damageMultiplier = 100;

    private int _damageToPlayer = 0;

    private Health _playerHealth;
    private PlayerMovement _playerMovement;
    private InputManager _inputManager;

    private float _fallingCount;

    private void Start()
    {
        _playerHealth = GetComponent<Health>();
        _playerMovement = GetComponent<PlayerGroundMovement>();
        _inputManager = GetComponentInChildren<InputManager>();

        _fallingCount = 0;
        _playerMovement.OnFalling += OnFalling;
        _playerMovement.OnLanding += OnLanding;
        _inputManager.OnJump += ResetCounterOnPlayerJump;
    }

    private void ResetCounterOnPlayerJump()
    {
        _fallingCount = 0;
    }

    private void OnFalling()
    {
        _fallingCount += Time.deltaTime;
    }

    private void OnLanding()
    {
        if (_fallingCount >= _timeBeforeHit && !StaticObjects.GetPlayerState().IsInvincible)
        {
            _damageToPlayer = (int)Mathf.Clamp(_fallingCount * _damageMultiplier,
                _fallingCount * _damageMultiplier, _playerHealth.HealthPoint);
            _damageToPlayer -= _damageToPlayer % _damageMultiplier;
            _playerHealth.Hit(_damageToPlayer, transform.position + Vector3.down);
        }
        _damageToPlayer = 0;
        _fallingCount = 0;
    }
}
