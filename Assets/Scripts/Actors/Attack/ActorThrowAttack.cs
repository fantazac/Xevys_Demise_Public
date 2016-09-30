﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

public class ActorThrowAttack : MonoBehaviour
{

    [SerializeField]
    private GameObject _knife;
    [SerializeField]
    private GameObject _axe;
    [SerializeField]
    private float WEAPON_SPAWN_DISTANCE_FROM_PLAYER = 0.7f;
    [SerializeField]
    private float AXE_THROWING_HEIGHT = 1f;
    [SerializeField]
    private float KNIFE_SPEED = 9f;
    [SerializeField]
    private float AXE_SPEED = 6f;
    [SerializeField]
    private float AXE_THROWING_ANGLE = 14.5f;
    [SerializeField]
    private float AXE_INITIAL_ANGLE = 90f;
    [SerializeField]
    private const int ATTACK_COOLDOWN = 50;

    private InputManager _inputManager;
    private AudioSource[] _audioSources;
    private int _knifeThrowCDCount;
    private int _AxeThrowCDCount;
    public enum Projectile { Knives, Axes };
    private List<Projectile> _throwableWeapons;

    private void Start()
    {
        _inputManager = GetComponent<InputManager>();
        _throwableWeapons = new List<Projectile> {Projectile.Knives, Projectile.Axes};
        _inputManager.OnThrowAttack += OnKnifeAttack;
        _inputManager.OnThrowAttackChanged += OnThrowableWeaponChange;

        _audioSources = GetComponents<AudioSource>();

        _knifeThrowCDCount = ATTACK_COOLDOWN;
        _AxeThrowCDCount = ATTACK_COOLDOWN;
    }

    private void Update()
    {
        if (_knifeThrowCDCount < ATTACK_COOLDOWN)
        {
            _knifeThrowCDCount++;
        }
        if (_AxeThrowCDCount < ATTACK_COOLDOWN)
        {
            _AxeThrowCDCount++;
        }
    }

    private void OnKnifeAttack()
    {
        if (!GameObject.Find("Knife(Clone)") && _knifeThrowCDCount >= ATTACK_COOLDOWN)
        {
            _audioSources[1].Play();
            GameObject newKnife;

            if (GetComponent<PlayerMovement>().FacingRight)
            {
                newKnife = (GameObject)Instantiate(_knife, new Vector3(transform.position.x + WEAPON_SPAWN_DISTANCE_FROM_PLAYER, transform.position.y, transform.position.z), transform.rotation);
                newKnife.GetComponent<Rigidbody2D>().velocity = new Vector2(KNIFE_SPEED, 0);
                _knifeThrowCDCount = 0;
            }
            else
            {
                newKnife = (GameObject)Instantiate(_knife, new Vector3(transform.position.x - WEAPON_SPAWN_DISTANCE_FROM_PLAYER, transform.position.y, transform.position.z), transform.rotation);
                newKnife.GetComponent<SpriteRenderer>().flipX = true;
                newKnife.GetComponent<Rigidbody2D>().velocity = new Vector2(-KNIFE_SPEED, 0);
                _knifeThrowCDCount = 0;
            }
        }
    }

    private void OnAxeAttack()
    {
        if (!GameObject.Find("Axe(Clone)") && _AxeThrowCDCount >= ATTACK_COOLDOWN)
        {
            _audioSources[2].Play();
            GameObject newAxe;

            if (GetComponent<PlayerMovement>().FacingRight)
            {
                newAxe = (GameObject)Instantiate(_axe, new Vector3(transform.position.x, transform.position.y + AXE_THROWING_HEIGHT, transform.position.z), transform.rotation);
                newAxe.GetComponent<Rigidbody2D>().rotation = AXE_INITIAL_ANGLE;
                newAxe.GetComponent<Rigidbody2D>().velocity = new Vector2(AXE_SPEED, AXE_THROWING_ANGLE);
                _AxeThrowCDCount = 0;
            }
            else
            {
                newAxe = (GameObject)Instantiate(_axe, new Vector3(transform.position.x, transform.position.y + AXE_THROWING_HEIGHT, transform.position.z), transform.rotation);
                newAxe.GetComponent<Rigidbody2D>().rotation = AXE_INITIAL_ANGLE;
                newAxe.GetComponent<SpriteRenderer>().flipY = true;
                newAxe.GetComponent<Rigidbody2D>().velocity = new Vector2(-AXE_SPEED, AXE_THROWING_ANGLE);
                _AxeThrowCDCount = 0;
            }
        }
    }

    private void OnThrowableWeaponChange()
    {
        switch (_throwableWeapons[0])
        {
            case Projectile.Knives: default:             
                _inputManager.OnThrowAttack += OnAxeAttack;
                _inputManager.OnThrowAttack -= OnKnifeAttack;
                break;
            case Projectile.Axes:
                _inputManager.OnThrowAttack -= OnAxeAttack;
                _inputManager.OnThrowAttack += OnKnifeAttack;
                break;
        }

        Projectile tmp = _throwableWeapons[0];
        _throwableWeapons[0] = _throwableWeapons[1];
        _throwableWeapons[1] = tmp;
        Debug.Log(_throwableWeapons[0].ToString());
    }
}
