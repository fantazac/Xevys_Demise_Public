using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerThrowingWeaponsMunitions : MonoBehaviour
{
    private int _axeMunition = 0;
    public int AxeMunition { get { return _axeMunition; } set { _axeMunition = value; } }

    private int _knifeMunition = 0;
    public int KnifeMunition { get { return _knifeMunition; } set { _knifeMunition = value; } }
}