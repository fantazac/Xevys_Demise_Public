using UnityEngine;
using System.Collections;

public class UnityTags : MonoBehaviour
{

    public string Player { get; private set; }
    public string BasicAttackHitbox { get; private set; }
    public string Knife { get; private set; }
    public string Axe { get; private set; }

    private void Start()
    {
        Player = "Player";
        BasicAttackHitbox = "BasicAttackHitbox";
        Knife = "Knife";
        Axe = "Axe";
    }
}
