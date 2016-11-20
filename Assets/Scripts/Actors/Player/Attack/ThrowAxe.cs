using UnityEngine;
using System.Collections;

public class ThrowAxe : PlayerThrowAttack
{

    [SerializeField]
    protected GameObject _axe;

    [SerializeField]
    private float _axeThrowingHeight = 1f;

    [SerializeField]
    private float _axeHorinzontalSpeed = 6f;

    [SerializeField]
    private float _axeVerticalSpeed = 14.5f;

    [SerializeField]
    private float _axeInitialRotation = 90f;

    public delegate void OnAxeAmmoUsedHandler(int ammoUsedOnThrow);
    public event OnAxeAmmoUsedHandler OnAxeAmmoUsed;

    public delegate void OnAxeThrownHandler(GameObject axe);
    public event OnAxeThrownHandler OnAxeThrown;

    protected override void Throw()
    {
        if (HasAmmo())
        {
            GameObject thrownWeapon = InstantiateThrowWeapon(_axe,
                new Vector2(transform.position.x, transform.position.y + _axeThrowingHeight),
                new Vector3(0, 0, _axeInitialRotation),
                new Vector2(_playerOrientation.IsFacingRight ? _axeHorinzontalSpeed : -_axeHorinzontalSpeed, _axeVerticalSpeed),
                new Vector2(_axe.transform.localScale.x, _playerOrientation.IsFacingRight ? _axe.transform.localScale.y : -_axe.transform.localScale.y));
            OnAxeAmmoUsed(_ammoUsedPerThrow);
            if (OnAxeThrown != null)
            {
                OnAxeThrown(thrownWeapon);
            }
        }
    }

    protected override bool HasAmmo()
    {
        return _munitions.AxeAmmo > 0;
    }

}
