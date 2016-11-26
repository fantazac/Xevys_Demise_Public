using UnityEngine;
using System.Collections;

public class MoveItemToBossDeathSpot : MonoBehaviour
{
    private GameObject _boss;

    private void Start()
    {
        _boss = GetComponentInChildren<ActivateArtefactOnBossDefeated>().Boss;
        _boss.GetComponent<Health>().OnDeath += MoveArtefact;
    }

    private void MoveArtefact()
    {
        transform.position = _boss.transform.position;
    }
}
