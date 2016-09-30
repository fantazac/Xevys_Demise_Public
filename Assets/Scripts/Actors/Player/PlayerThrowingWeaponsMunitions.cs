using UnityEngine;
using System.Collections;

public class PlayerThrowingWeaponsMunitions : MonoBehaviour
{

    private PickThrowingWeaponsMunitions _munitionsPicker;

    private int _axeMunition = 0;
    public int AxeMunition { get { return _axeMunition; } set { _axeMunition = value; } }

    private void Start()
    {
        _munitionsPicker = GameObject.Find("BaseAxeItem").GetComponent<PickThrowingWeaponsMunitions>();
        _munitionsPicker.OnAxePicked += OnAxePicked;
    }

    private void OnAxePicked(int axeAmount)
    {
        _axeMunition += axeAmount;
        _munitionsPicker.OnAxePicked -= OnAxePicked;
        Destroy(GameObject.Find("BaseAxeItem"));
    }
}