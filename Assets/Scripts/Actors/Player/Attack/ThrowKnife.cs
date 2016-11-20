using UnityEngine;
using System.Collections;

public class ThrowKnife : PlayerThrowAttack
{

    [SerializeField]
    protected GameObject _knife;

    [SerializeField]
    private float _knifeSpeed = 15f;

    public delegate void OnKnifeAmmoUsedHandler(int ammoUsedOnThrow);
    public event OnKnifeAmmoUsedHandler OnKnifeAmmoUsed;

    public delegate void OnKnifeThrownHandler(GameObject knife);
    public event OnKnifeThrownHandler OnKnifeThrown;

    protected override void Throw()
    {
        if (HasAmmo())
        {
            Debug.Log(2);
            GameObject thrownWeapon = InstantiateThrowWeapon(_knife,
                new Vector2(transform.position.x, transform.position.y),
                Vector3.zero,
                new Vector2(_playerOrientation.IsFacingRight ? _knifeSpeed : -_knifeSpeed, 0),
                new Vector2(_playerOrientation.IsFacingRight ? _knife.transform.localScale.x : -_knife.transform.localScale.x, _knife.transform.localScale.y));
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
