using UnityEngine;
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
    private float _weaponSpawnDistanceFromPlayer = 0f;

    [SerializeField]
    private float _axeThrowingHeight = 1f;

    [SerializeField]
    private float _knifeSpeed = 15f;

    [SerializeField]
    private float _axeHorinzontalSpeed = 6f;

    [SerializeField]
    private float _axeVerticalSpeed = 14.5f;

    [SerializeField]
    private float _axeInitialRotation = 90f;

    [SerializeField]
    private float _attackCooldown = 1f;

    private WeaponType _selectedWeapon;
    private bool _canUseThrowAttack = true;

    private InputManager _inputManager;
    private InventoryManager _inventoryManager;
    private AudioSourcePlayer _soundPlayer;
    private PlayerThrowingWeaponsMunitions _munitions;


    private delegate void OnSelectedThrowAttackHandler();
    private event OnSelectedThrowAttackHandler OnSelectedThrowAttack;

    public delegate void OnThrowAttackUsedHandler(WeaponType weaponType);
    public event OnThrowAttackUsedHandler OnThrowAttackUsed;
    //private ShowItems _showItems;

    private void Start()
    {
        /*_inputManager = GetComponentInChildren<InputManager>();
        _inputManager.OnThrowAttackChangeButtonPressed += OnChangeWeaponType;

        _inventoryManager = GetComponent<InventoryManager>();
        _inventoryManager.OnEnableWeapon += OnThrowableWeaponChange;*/

        _inputManager.OnThrowAttack += OnThrowAttack;

        _soundPlayer = GetComponent<AudioSourcePlayer>();
        //_showItems = GameObject.Find("ItemCanvas").GetComponent<ShowItems>();
        _munitions = GetComponent<PlayerThrowingWeaponsMunitions>();
        _selectedWeapon = WeaponType.None;
    }

    private void OnThrowAttack()
    {
        if (CanUseThrowAttack())
        {
            _canUseThrowAttack = false;
            OnSelectedThrowAttack();
            Invoke("EnableThrowAttack", _attackCooldown);
            OnThrowAttackUsed(_selectedWeapon);
        }
    }

    private bool CanUseThrowAttack()
    {
        return _canUseThrowAttack && _selectedWeapon != WeaponType.None;
    }

    private void EnableThrowAttack()
    {
        _canUseThrowAttack = true;
    }

    private void OnKnifeAttack()
    {
        if (GetComponent<PlayerThrowingWeaponsMunitions>().KnifeMunition > 0)
        {
            _soundPlayer.Play(1);
            GameObject newKnife;

            newKnife = (GameObject)Instantiate(_knife, new Vector2(transform.position.x + _weaponSpawnDistanceFromPlayer, transform.position.y), transform.rotation);
            newKnife.GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<FlipPlayer>().IsFacingRight ? _knifeSpeed : -_knifeSpeed, 0);
            newKnife.GetComponent<SpriteRenderer>().flipX = !GetComponent<FlipPlayer>().IsFacingRight;
            _munitions.KnifeMunition--;
            if(_inventoryManager.HasInfiniteKnives && _munitions.KnifeMunition == 0)
            {
                _munitions.KnifeMunition += 1;
            }

            //_showItems.KnifeAmmoChange(_munitions.KnifeMunition);
        }
    }

    private void OnAxeAttack()
    {
        if (GetComponent<PlayerThrowingWeaponsMunitions>().AxeMunition > 0)
        {
            _soundPlayer.Play(2);
            GameObject newAxe;

            newAxe = (GameObject)Instantiate(_axe, new Vector2(transform.position.x, transform.position.y + _axeThrowingHeight), transform.rotation);
            newAxe.transform.eulerAngles = new Vector3(0, 0, _axeInitialRotation);
            newAxe.GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<FlipPlayer>().IsFacingRight ? _axeHorinzontalSpeed : -_axeHorinzontalSpeed, _axeVerticalSpeed);
            newAxe.transform.localScale = new Vector2(newAxe.transform.localScale.x, GetComponent<FlipPlayer>().IsFacingRight ? newAxe.transform.localScale.y : -newAxe.transform.localScale.y);
            _munitions.AxeMunition--;
            if (_inventoryManager.HasInfiniteAxes && _munitions.AxeMunition == 0)
            {
                _munitions.AxeMunition += 1;
            }

            //_showItems.AxeAmmoChange(_munitions.AxeMunition);
        }
    }

    private void OnThrowableWeaponChange(WeaponType weaponTypes)
    {
        switch (weaponTypes)
        {
            case WeaponType.Axe:
                {
                    OnSelectedThrowAttack += OnAxeAttack;
                    OnSelectedThrowAttack -= OnKnifeAttack;
                    break;
                }
            case WeaponType.Knife:
                {
                    OnSelectedThrowAttack -= OnAxeAttack;
                    OnSelectedThrowAttack += OnKnifeAttack;
                    break;
                }
        }
    }

    private void OnChangeWeaponType()
    {
        /*if (_inventoryManager.KnifeActive && GetComponent<PlayerThrowingWeaponsMunitions>().AxeMunition > 0)
        {
            OnThrowableWeaponChange(WeaponType.Axe);
        }
        else if (_inventoryManager.AxeActive && GetComponent<PlayerThrowingWeaponsMunitions>().KnifeMunition > 0)
        {
            OnThrowableWeaponChange(WeaponType.Knife);
        }*/
    }
}
