using UnityEngine;
using System.Collections;

public class ThrowAxe : PlayerThrowAttack
{

    [SerializeField]
    protected GameObject _axe;

    private float _playerZPosition;

    public delegate void OnAxeAmmoUsedHandler(int ammoUsedOnThrow);
    public event OnAxeAmmoUsedHandler OnAxeAmmoUsed;

    public delegate void OnAxeThrownHandler(GameObject axe);
    public event OnAxeThrownHandler OnAxeThrown;

    protected override void Start()
    {
        base.Start();
        _playerZPosition = transform.position.z;
    }

    protected override void Throw()
    {
        if (HasAmmo())
        {
            GameObject thrownWeapon = (GameObject)Instantiate(_axe, transform.position + (Vector3.back * _playerZPosition), transform.rotation);
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
