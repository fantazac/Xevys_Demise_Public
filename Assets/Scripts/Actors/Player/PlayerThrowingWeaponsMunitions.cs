using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerThrowingWeaponsMunitions : MonoBehaviour
{

    private IList<PickThrowingWeaponsMunitions> _munitionsPicker;

    private int _axeMunition = 0;
    public int AxeMunition { get { return _axeMunition; } set { _axeMunition = value; } }

    private int _knifeMunition = 0;
    public int KnifeMunition { get { return _knifeMunition; } set { _knifeMunition = value; } }

    private void Start()
    {
        _munitionsPicker.Add(GameObject.Find("BaseAxeItem").GetComponent<PickThrowingWeaponsMunitions>());
        _munitionsPicker.Add(GameObject.Find("BaseKnifeItem").GetComponent<PickThrowingWeaponsMunitions>());
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("KnifePickableItem"))
        {
            
        }

        foreach (PickThrowingWeaponsMunitions munitionsItem in _munitionsPicker)
        {
            
        }
    }

    private void OnAxePicked(int axeAmount)
    {
        _axeMunition += axeAmount;
        _munitionsPicker.OnAxePicked -= OnAxePicked;
        Destroy(GameObject.Find("BaseAxeItem"));
    }

    private void OnKnifePicked(int axeAmount)
    {
        _axeMunition += axeAmount;
        _munitionsPicker.OnAxePicked -= OnAxePicked;
        Destroy(GameObject.Find("BaseKnifeItem"));
    }
}