﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

public class ActorThrowAttack : MonoBehaviour
{

    [SerializeField]
    private GameObject _knife;

    [SerializeField]
    private GameObject _axeFacingLeft;

    [SerializeField]
    private GameObject _axeFacingRight;

    [SerializeField]
    private float WEAPON_SPAWN_DISTANCE_FROM_PLAYER = 0f;

    [SerializeField]
    private float AXE_THROWING_HEIGHT = 1f;

    [SerializeField]
    private float KNIFE_SPEED = 15f;

    [SerializeField]
    private float AXE_SPEED = 6f;

    [SerializeField]
    private float AXE_THROWING_ANGLE = 14.5f;

    [SerializeField]
    private float AXE_INITIAL_ANGLE = 90f;

    [SerializeField]
    private const int ATTACK_COOLDOWN = 50;

    [SerializeField]
    private const int WEAPON_Z_POSITION = 0;

    private int _knifeThrowCDCount;
    private int _axeThrowCDCount;

    private InputManager _inputManager;
    private AudioSource[] _audioSources;

    public enum Projectile { Knives, Axes };
    private List<Projectile> _throwableWeapons;

    private ShowEquippedWeapons _showEquippedWeapons;

    private void Start()
    {
        _inputManager = GetComponent<InputManager>();
        _throwableWeapons = new List<Projectile> { Projectile.Knives, Projectile.Axes };
        _inputManager.OnThrowAttack += OnKnifeAttack;
        _inputManager.OnThrowAttackChanged += OnThrowableWeaponChange;

        _audioSources = GetComponents<AudioSource>();
        _showEquippedWeapons = GameObject.Find("SelectedWeaponCanvas").GetComponent<ShowEquippedWeapons>();

        _knifeThrowCDCount = ATTACK_COOLDOWN;
        _axeThrowCDCount = ATTACK_COOLDOWN;
    }

    private void Update()
    {
        if (_knifeThrowCDCount < ATTACK_COOLDOWN)
        {
            _knifeThrowCDCount++;
        }
        if (_axeThrowCDCount < ATTACK_COOLDOWN)
        {
            _axeThrowCDCount++;
        }
    }

    private void OnKnifeAttack()
    {
        if (_knifeThrowCDCount >= ATTACK_COOLDOWN && GetComponent<PlayerThrowingWeaponsMunitions>().KnifeMunition > 0)
        {
            _audioSources[1].Play();
            GameObject newKnife;

            if (GetComponent<PlayerMovement>().FacingRight)
            {
                newKnife = (GameObject)Instantiate(_knife, new Vector3(transform.position.x + WEAPON_SPAWN_DISTANCE_FROM_PLAYER, transform.position.y, WEAPON_Z_POSITION), transform.rotation);
                newKnife.GetComponent<Rigidbody2D>().velocity = new Vector2(KNIFE_SPEED, 0);
                _knifeThrowCDCount = 0;
                GetComponent<PlayerThrowingWeaponsMunitions>().KnifeMunition--;
            }
            else
            {
                newKnife = (GameObject)Instantiate(_knife, new Vector3(transform.position.x - WEAPON_SPAWN_DISTANCE_FROM_PLAYER, transform.position.y, WEAPON_Z_POSITION), transform.rotation);
                newKnife.GetComponent<SpriteRenderer>().flipX = true;
                newKnife.GetComponent<Rigidbody2D>().velocity = new Vector2(-KNIFE_SPEED, 0);
                _knifeThrowCDCount = 0;
                GetComponent<PlayerThrowingWeaponsMunitions>().KnifeMunition--;
            }
            _showEquippedWeapons.OnKnifeAmmoChanged(GetComponent<PlayerThrowingWeaponsMunitions>().KnifeMunition);
        }
    }

    private void OnAxeAttack()
    {
        if (_axeThrowCDCount >= ATTACK_COOLDOWN && GetComponent<PlayerThrowingWeaponsMunitions>().AxeMunition > 0)
        {
            _audioSources[2].Play();
            GameObject newAxe;

            if (GetComponent<PlayerMovement>().FacingRight)
            {
                newAxe = (GameObject)Instantiate(_axeFacingRight, new Vector3(transform.position.x, transform.position.y + AXE_THROWING_HEIGHT, WEAPON_Z_POSITION), transform.rotation);
                newAxe.GetComponent<Rigidbody2D>().rotation = AXE_INITIAL_ANGLE;
                newAxe.GetComponent<Rigidbody2D>().velocity = new Vector2(AXE_SPEED, AXE_THROWING_ANGLE);
                _axeThrowCDCount = 0;
                GetComponent<PlayerThrowingWeaponsMunitions>().AxeMunition--;
            }
            else
            {
                newAxe = (GameObject)Instantiate(_axeFacingLeft, new Vector3(transform.position.x, transform.position.y + AXE_THROWING_HEIGHT, WEAPON_Z_POSITION), transform.rotation);
                newAxe.GetComponent<Rigidbody2D>().rotation = AXE_INITIAL_ANGLE;
                newAxe.GetComponent<SpriteRenderer>().flipY = true;
                newAxe.GetComponent<Rigidbody2D>().velocity = new Vector2(-AXE_SPEED, AXE_THROWING_ANGLE);
                _axeThrowCDCount = 0;
                GetComponent<PlayerThrowingWeaponsMunitions>().AxeMunition--;
            }
            _showEquippedWeapons.OnAxeAmmoChanged(GetComponent<PlayerThrowingWeaponsMunitions>().AxeMunition);
        }
    }

    private void OnThrowableWeaponChange()
    {
        switch (_throwableWeapons[0])
        {
            case Projectile.Knives:
            default:
                _inputManager.OnThrowAttack += OnAxeAttack;
                _inputManager.OnThrowAttack -= OnKnifeAttack;
                _showEquippedWeapons.OnAxeSelected();
                break;
            case Projectile.Axes:
                _inputManager.OnThrowAttack -= OnAxeAttack;
                _inputManager.OnThrowAttack += OnKnifeAttack;
                _showEquippedWeapons.OnKnifeSelected();
                break;
        }

        Projectile tmp = _throwableWeapons[0];
        _throwableWeapons[0] = _throwableWeapons[1];
        _throwableWeapons[1] = tmp;
    }
}
