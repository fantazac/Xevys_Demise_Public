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

    private PlayerWeaponAmmo _ammo;
    private Health _health;

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
            GameObject item = (GameObject)Instantiate(_knife, transform.position, new Quaternion());
            item.GetComponent<OnItemDrop>().Initialise(3, 0, GetComponent<Collider2D>());
            item.GetComponent<PickUpWeaponAmmo>().SetAmmoOnDrop((int)((_ammo.KnifeAmmo - MAXIMUM_AMMO_AFTER_DEATH) * 0.5f));
            _ammo.AddKnifeAmmo(-_ammo.KnifeAmmo + MAXIMUM_AMMO_AFTER_DEATH);
            
        }
        if (_ammo.AxeAmmo > MAXIMUM_AMMO_AFTER_DEATH)
        {
            GameObject item2 = (GameObject)Instantiate(_axe, transform.position, new Quaternion());
            item2.GetComponent<OnItemDrop>().Initialise(3, 1, GetComponent<Collider2D>());
            item2.GetComponent<PickUpWeaponAmmo>().SetAmmoOnDrop((int)((_ammo.AxeAmmo - MAXIMUM_AMMO_AFTER_DEATH) * 0.5f));
            _ammo.AddAxeAmmo(-_ammo.AxeAmmo + MAXIMUM_AMMO_AFTER_DEATH);
        }
        GameObject item3 = (GameObject)Instantiate(_heart, transform.position, new Quaternion());
        item3.GetComponent<HealthItemHeal>().SetHealPoints((int)(_health.MaxHealth * 0.5f));
    }

}
