﻿using UnityEngine;
using System.Collections;

public class FallDamage: MonoBehaviour
{
    [SerializeField]
    private float _timeBeforeHit = 0.75f;
    [SerializeField]
    private float _damageMultiplier = 100f;

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
        if (_fallingCount > _timeBeforeHit && !StaticObjects.GetPlayerState().IsInvincible)
        {
            _playerHealth.Hit((int)Mathf.Clamp(_fallingCount * _damageMultiplier, 
                _fallingCount * _damageMultiplier, _playerHealth.HealthPoint), 
                transform.position + Vector3.down);
        }

        _fallingCount = 0;
    }
}
