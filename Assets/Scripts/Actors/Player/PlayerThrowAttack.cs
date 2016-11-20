using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

public class PlayerThrowAttack : MonoBehaviour
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
    private float _attackCooldown = 0.8f;

    private int _ammoUsedPerThrow = 1;

    private WeaponType _selectedWeapon;
    private bool _canUseThrowAttack = true;

    private InputManager _inputManager;
    private InventoryManager _inventoryManager;
    private PlayerWeaponAmmo _munitions;
    private ShowItems _showItems;
    private PlayerOrientation _playerOrientation;

    private WaitForSeconds _enableAttackDelay;

    private delegate void OnSelectedThrowAttackHandler();
    private event OnSelectedThrowAttackHandler OnSelectedThrowAttack;

    public delegate void OnKnifeAmmoUsedHandler(int ammoUsedOnThrow);
    public event OnKnifeAmmoUsedHandler OnKnifeAmmoUsed;

    public delegate void OnAxeAmmoUsedHandler(int ammoUsedOnThrow);
    public event OnAxeAmmoUsedHandler OnAxeAmmoUsed;

    public delegate void OnKnifeThrownHandler(GameObject knife);
    public event OnKnifeThrownHandler OnKnifeThrown;

    public delegate void OnAxeThrownHandler(GameObject axe);
    public event OnAxeThrownHandler OnAxeThrown;

    private void Start()
    {
        _inputManager = GetComponentInChildren<InputManager>();
        _inputManager.OnThrowAttackChangeButtonPressed += OnSwitchWeapon;

        _inventoryManager = GetComponent<InventoryManager>();
        _inventoryManager.OnEnableAxe += SelectAxe;
        _inventoryManager.OnEnableKnife += SelectKnife;

        _showItems = StaticObjects.GetItemCanvas().GetComponent<ShowItems>();

        _playerOrientation = GetComponent<PlayerOrientation>();

        _inputManager.OnThrowAttack += OnThrowAttack;

        _munitions = GetComponent<PlayerWeaponAmmo>();
        _selectedWeapon = WeaponType.None;

        _enableAttackDelay = new WaitForSeconds(_attackCooldown);
    }

    private void OnThrowAttack()
    {
        if (CanUseThrowAttack())
        {
            _canUseThrowAttack = false;
            OnSelectedThrowAttack();
            StartCoroutine("EnableThrowAttack");
        }
    }

    private bool CanUseThrowAttack()
    {
        return _canUseThrowAttack && _selectedWeapon != WeaponType.None;
    }

    private IEnumerator EnableThrowAttack()
    {
        yield return _enableAttackDelay;

        _canUseThrowAttack = true;
    }

    private bool HasKnifeAmmo()
    {
        return _munitions.KnifeAmmo > 0;
    }

    private bool HasAxeAmmo()
    {
        return _munitions.AxeAmmo > 0;
    }

    private void InstantiateThrowWeapon(GameObject weapon, Vector2 initialPosition, Vector3 initialRotation, Vector2 initialVelocity, Vector2 initialDirection)
    {
        GameObject newWeapon;

        newWeapon = (GameObject)Instantiate(weapon, initialPosition, transform.rotation);
        newWeapon.transform.eulerAngles = initialRotation;
        newWeapon.GetComponent<Rigidbody2D>().velocity = initialVelocity;
        newWeapon.transform.localScale = initialDirection;

        if (newWeapon.tag == "Knife" && OnKnifeThrown != null)
        {
            OnKnifeThrown(newWeapon);
        }
        else if (newWeapon.tag == "Axe" && OnKnifeThrown != null)
        {
            OnAxeThrown(newWeapon);
        }
    }

    private void OnKnifeAttack()
    {
        if (HasKnifeAmmo())
        {
            InstantiateThrowWeapon(_knife,
                new Vector2(transform.position.x + _weaponSpawnDistanceFromPlayer, transform.position.y),
                Vector3.zero,
                new Vector2(_playerOrientation.IsFacingRight ? _knifeSpeed : -_knifeSpeed, 0),
                new Vector2(_playerOrientation.IsFacingRight ? _knife.transform.localScale.x : -_knife.transform.localScale.x, _knife.transform.localScale.y));
            OnKnifeAmmoUsed(_ammoUsedPerThrow);

        }
    }

    private void OnAxeAttack()
    {
        if (HasAxeAmmo())
        {
            InstantiateThrowWeapon(_axe,
                new Vector2(transform.position.x, transform.position.y + _axeThrowingHeight),
                new Vector3(0, 0, _axeInitialRotation),
                new Vector2(_playerOrientation.IsFacingRight ? _axeHorinzontalSpeed : -_axeHorinzontalSpeed, _axeVerticalSpeed),
                new Vector2(_axe.transform.localScale.x, _playerOrientation.IsFacingRight ? _axe.transform.localScale.y : -_axe.transform.localScale.y));
            OnAxeAmmoUsed(_ammoUsedPerThrow);
        }
    }

    private void OnSwitchWeapon()
    {
        switch (_selectedWeapon)
        {
            case WeaponType.Axe:
                {
                    if (_inventoryManager.KnifeEnabled)
                    {
                        SelectKnife();
                    }
                    break;
                }
            case WeaponType.Knife:
                {
                    if (_inventoryManager.AxeEnabled)
                    {
                        SelectAxe();
                    }
                    break;
                }
        }
    }

    private void SelectAxe()
    {
        OnSelectedThrowAttack += OnAxeAttack;
        OnSelectedThrowAttack -= OnKnifeAttack;
        _selectedWeapon = WeaponType.Axe;
        _showItems.SelectAxe();
    }

    private void SelectKnife()
    {
        OnSelectedThrowAttack -= OnAxeAttack;
        OnSelectedThrowAttack += OnKnifeAttack;
        _selectedWeapon = WeaponType.Knife;
        _showItems.SelectKnife();
    }
}
