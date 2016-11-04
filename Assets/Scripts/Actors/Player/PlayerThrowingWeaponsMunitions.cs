using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* BEN_CORRECTION
 * 
 * Dès fois, un long nom est meilleur qu'un nom court. Cependant, dans d'autres cas, c'est l'inverse.
 * 
 * Ici, quelque chose comme "PlayerAmmunitions" serait plus simple.
 * 
 * Ah oui! En anglais, c'est pas "Munitions", mais "Ammunitions". Petit détail...
 */
public class PlayerThrowingWeaponsMunitions : MonoBehaviour
{
    private int _axeMunition = 0;
    public int AxeMunition { get { return _axeMunition; } set { _axeMunition = value; } }

    private int _knifeMunition = 0;
    public int KnifeMunition { get { return _knifeMunition; } set { _knifeMunition = value; } }
}