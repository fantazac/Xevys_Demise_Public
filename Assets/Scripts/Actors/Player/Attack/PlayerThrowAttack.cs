﻿using UnityEngine;
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
    private InventoryManager _inventoryManager;
    protected PlayerWeaponAmmo _munitions;
    protected PlayerOrientation _playerOrientation;

    private WaitForSeconds _enableAttackDelay;

    protected void Start()
    {
        _inputManager = GetComponentInChildren<InputManager>();

        _inventoryManager = GetComponent<InventoryManager>();

        _playerOrientation = GetComponent<PlayerOrientation>();

        _inputManager.OnThrowAttack += OnThrowAttack;

        _munitions = GetComponent<PlayerWeaponAmmo>();

        _enableAttackDelay = new WaitForSeconds(_attackCooldown);
    }

    protected void OnThrowAttack()
    {
        if (CanUseThrowAttack())
        {
            _canUseThrowAttack = false;
            Throw();
            StartCoroutine(EnableThrowAttack());
        }
    }

    private bool CanUseThrowAttack()
    {
        return _canUseThrowAttack && enabled;
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

    protected GameObject InstantiateThrowWeapon(GameObject weapon, Vector2 initialPosition, Vector3 initialRotation, Vector2 initialVelocity, Vector2 initialDirection)
    {
        GameObject newWeapon;

        newWeapon = (GameObject)Instantiate(weapon, initialPosition, transform.rotation);
        newWeapon.transform.eulerAngles = initialRotation;
        newWeapon.GetComponent<Rigidbody2D>().velocity = initialVelocity;
        newWeapon.transform.localScale = initialDirection;

        return newWeapon;
    }

    protected virtual void Throw() { }
}
