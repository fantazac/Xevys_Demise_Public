using UnityEngine;
using System.Collections.Generic;

public class DropAmmoAndHeartOnDeath : MonoBehaviour
{
    [SerializeField]
    private GameObject _knife;

    [SerializeField]
    private GameObject _axe;

    [SerializeField]
    private GameObject _heart;

    private const int MAXIMUM_AMMO_AFTER_DEATH = 10;
    private const int NB_OF_ITEMS_DROPPED = 3;

    private PlayerWeaponAmmo _ammo;
    private Health _health;
    private int _ammoToGiveBack = 0;

    private void Start()
    {
        _health = GetComponent<Health>();
        _health.OnDeath += DropAmmoAndHeart;
        _ammo = GetComponent<PlayerWeaponAmmo>();
    }

    private void DropAmmoAndHeart()
    {
        if(_ammo.KnifeAmmo > MAXIMUM_AMMO_AFTER_DEATH)
        {
            DropWeaponAmmo(_ammo.KnifeAmmo, _knife, 0);
            _ammo.AddKnifeAmmo(-_ammo.KnifeAmmo + MAXIMUM_AMMO_AFTER_DEATH);
        }
        if (_ammo.AxeAmmo > MAXIMUM_AMMO_AFTER_DEATH)
        {
            DropWeaponAmmo(_ammo.AxeAmmo, _axe, 2);
            _ammo.AddAxeAmmo(-_ammo.AxeAmmo + MAXIMUM_AMMO_AFTER_DEATH);
        }
        GameObject healthItem = (GameObject)Instantiate(_heart, transform.position, new Quaternion());
        healthItem.GetComponent<HealthItemHeal>().SetHealPoints((int)(_health.MaxHealth * 0.5f));
    }

    private void DropWeaponAmmo(int initialAmmo, GameObject item, int itemId)
    {
        GameObject itemToDrop = (GameObject)Instantiate(item, transform.position, new Quaternion());
        _ammoToGiveBack = (int)((initialAmmo - MAXIMUM_AMMO_AFTER_DEATH) * 0.5f);
        if (_ammoToGiveBack > 0)
        {
            itemToDrop.GetComponent<OnItemDrop>().Initialise(NB_OF_ITEMS_DROPPED, itemId, GetComponent<Collider2D>());
            itemToDrop.GetComponent<PickUpWeaponAmmo>().SetAmmoOnDrop(_ammoToGiveBack);
        }
        else
        {
            Destroy(itemToDrop);
        }
    }
}
