using UnityEngine;
using System.Collections;

public class ThrowKnife : PlayerThrowAttack
{

    [SerializeField]
    protected GameObject _knife;

    public delegate void OnKnifeAmmoUsedHandler(int ammoUsedOnThrow);
    public event OnKnifeAmmoUsedHandler OnKnifeAmmoUsed;

    public delegate void OnKnifeThrownHandler(GameObject knife);
    public event OnKnifeThrownHandler OnKnifeThrown;

    protected override void Throw()
    {
        if (HasAmmo())
        {
            GameObject thrownWeapon = (GameObject)Instantiate(_knife, transform.position, transform.rotation);
            OnKnifeAmmoUsed(_ammoUsedPerThrow);
            if (OnKnifeThrown != null)
            {
                OnKnifeThrown(thrownWeapon);
            }
        }
    }

    protected override bool HasAmmo()
    {
        return _munitions.KnifeAmmo > 0;
    }

}
