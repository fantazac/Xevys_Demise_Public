using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

public class PlayerThrowAttack : MonoBehaviour
{

    [SerializeField]
    protected float _attackCooldown = 0.8f;

    [SerializeField]
    protected int _ammoUsedPerThrow = 1;

    protected static bool _canUseThrowAttack = true;

    private InputManager _inputManager;
    protected PlayerWeaponAmmo _munitions;

    private WaitForSeconds _enableAttackDelay;

    protected virtual void Start()
    {
        _inputManager = GetComponentInChildren<InputManager>();
        _inputManager.OnThrowAttack += OnThrowAttack;

        _munitions = GetComponent<PlayerWeaponAmmo>();

        _enableAttackDelay = new WaitForSeconds(_attackCooldown);
    }

    protected void OnThrowAttack()
    {
        if (_canUseThrowAttack && enabled)
        {
            _canUseThrowAttack = false;
            Throw();
            StartCoroutine(EnableThrowAttack());
        }
    }

    private IEnumerator EnableThrowAttack()
    {
        yield return _enableAttackDelay;

        _canUseThrowAttack = true;
    }

    protected virtual bool HasAmmo()
    {
        return false;
    }

    protected virtual void Throw() { }
}
