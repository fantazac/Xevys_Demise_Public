using UnityEngine;
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
    private float AXE_X_SPEED = 6f;

    [SerializeField]
    private float AXE_Y_SPEED = 14.5f;

    [SerializeField]
    private float AXE_INITIAL_ROTATION = 90f;

    [SerializeField]
    private const int ATTACK_COOLDOWN = 50;

    [SerializeField]
    private const int WEAPON_Z_POSITION = 0;

    private int _knifeThrowCDCount;
    private int _axeThrowCDCount;

    private InputManager _inputManager;
    private InventoryManager _inventoryManager;
    private AudioSourcePlayer _soundPlayer;
    private PlayerThrowingWeaponsMunitions _munitions;

    private ShowItems _showItems;

    private void Start()
    {
        _inputManager = GetComponentInChildren<InputManager>();
        _inputManager.OnThrowAttackChangeButtonPressed += OnThrowableWeaponChangeButtonPressed;

        _inventoryManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryManager>();
        _inventoryManager.OnThrowableWeaponChange += OnThrowableWeaponChange;

        _soundPlayer = GetComponent<AudioSourcePlayer>();
        _showItems = GameObject.Find("ItemCanvas").GetComponent<ShowItems>();

        _munitions = GetComponent<PlayerThrowingWeaponsMunitions>();

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
            _soundPlayer.Play(1);
            GameObject newKnife;

            newKnife = (GameObject)Instantiate(_knife, new Vector3(transform.position.x + WEAPON_SPAWN_DISTANCE_FROM_PLAYER, transform.position.y, WEAPON_Z_POSITION), transform.rotation);
            newKnife.GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<FlipPlayer>().IsFacingRight ? KNIFE_SPEED : -KNIFE_SPEED, 0);
            newKnife.GetComponent<SpriteRenderer>().flipX = !GetComponent<FlipPlayer>().IsFacingRight;
            _knifeThrowCDCount = 0;
            _munitions.KnifeMunition--;
            if(_inventoryManager.HasInfiniteKnives && _munitions.KnifeMunition == 0)
            {
                _munitions.KnifeMunition += 1;
            }

            _showItems.KnifeAmmoChange(_munitions.KnifeMunition);
        }
    }

    private void OnAxeAttack()
    {
        if (_axeThrowCDCount >= ATTACK_COOLDOWN && GetComponent<PlayerThrowingWeaponsMunitions>().AxeMunition > 0)
        {
            _soundPlayer.Play(2);
            GameObject newAxe;

            newAxe = (GameObject)Instantiate(_axeFacingRight, new Vector3(transform.position.x, transform.position.y + AXE_THROWING_HEIGHT, WEAPON_Z_POSITION), transform.rotation);
            newAxe.GetComponent<Rigidbody2D>().rotation = AXE_INITIAL_ROTATION;
            newAxe.GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<FlipPlayer>().IsFacingRight ? AXE_X_SPEED : -AXE_X_SPEED, AXE_Y_SPEED);
            newAxe.GetComponent<SpriteRenderer>().flipY = !GetComponent<FlipPlayer>().IsFacingRight;
            _axeThrowCDCount = 0;
            _munitions.AxeMunition--;
            if (_inventoryManager.HasInfiniteAxes && _munitions.AxeMunition == 0)
            {
                _munitions.AxeMunition += 1;
            }

            _showItems.AxeAmmoChange(_munitions.AxeMunition);
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

                    _showItems.AxeSelect();
                    break;
                }
            case InventoryManager.WeaponTypes.Knife:
                {
                    _inputManager.OnThrowAttack -= OnAxeAttack;
                    _inputManager.OnThrowAttack += OnKnifeAttack;

                    _inventoryManager.AxeActive = false;
                    _inventoryManager.KnifeActive = true;

                    _showItems.KnifeSelect();
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
