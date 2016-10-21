using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

public class ActorThrowAttack: MonoBehaviour
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
    private InventoryManager _inventoryManager;
    private AudioSource[] _audioSources;

    private ShowItems _showItems;

    private void Start()
    {
        _inputManager = GetComponent<InputManager>();
        _inputManager.OnThowAttackChangeButtonPressed += OnThrowableWeaponChangeButtonPressed;

        _inventoryManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryManager>();
        _inventoryManager.OnThrowableWeaponChange += OnThrowableWeaponChange;

        _audioSources = GetComponents<AudioSource>();
        _showItems = GameObject.Find("SelectedWeaponCanvas").GetComponent<ShowItems>();

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

            if (GetComponent<FlipPlayer>().IsFacingRight)
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
            _showItems.OnKnifeAmmoChanged(GetComponent<PlayerThrowingWeaponsMunitions>().KnifeMunition);
        }
    }

    private void OnAxeAttack()
    {
        if (_axeThrowCDCount >= ATTACK_COOLDOWN && GetComponent<PlayerThrowingWeaponsMunitions>().AxeMunition > 0)
        {
            _audioSources[2].Play();
            GameObject newAxe;

            if (GetComponent<FlipPlayer>().IsFacingRight)
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
            _showItems.OnAxeAmmoChanged(GetComponent<PlayerThrowingWeaponsMunitions>().AxeMunition);
        }
    }

    private void OnThrowableWeaponChange(InventoryManager.WeaponTypes weaponTypes)
    {
        switch (weaponTypes)
        {
            case InventoryManager.WeaponTypes.Axe:
                {
                    _inputManager.OnThrowAttack += OnAxeAttack;
                    _inputManager.OnThrowAttack -= OnKnifeAttack;

                    _inventoryManager.AxeActive = true;
                    _inventoryManager.KnifeActive = false;

                    _showItems.OnAxeSelected();
                    break;
                }
            case InventoryManager.WeaponTypes.Knife:
                {
                    _inputManager.OnThrowAttack -= OnAxeAttack;
                    _inputManager.OnThrowAttack += OnKnifeAttack;

                    _inventoryManager.AxeActive = false;
                    _inventoryManager.KnifeActive = true;

                    _showItems.OnKnifeSelected();
                    break;
                }
        }
    }

    private void OnThrowableWeaponChangeButtonPressed()
    {
        if (_inventoryManager.KnifeActive && GetComponent<PlayerThrowingWeaponsMunitions>().AxeMunition > 0)
        {
            OnThrowableWeaponChange(InventoryManager.WeaponTypes.Axe);
        }
        else if (_inventoryManager.AxeActive && GetComponent<PlayerThrowingWeaponsMunitions>().KnifeMunition > 0)
        {
            OnThrowableWeaponChange(InventoryManager.WeaponTypes.Knife);
        }
    }
}
