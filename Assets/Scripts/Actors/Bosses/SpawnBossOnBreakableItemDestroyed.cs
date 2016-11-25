using UnityEngine;
using System.Collections;

public class SpawnBossOnBreakableItemDestroyed : MonoBehaviour
{
    [SerializeField]
    private GameObject _boss;

    private Health _health;

    private void Start ()
    {
        _health = GetComponent<Health>();
        _health.OnDeath += EnableBossFight;

        _boss.GetComponent<Health>().OnDeath += DestroyBreakableItem;

        StaticObjects.GetPlayer().GetComponent<Health>().OnDeath += ResetBossRoom;

        _boss.SetActive(false);
	}

    private void EnableBossFight()
    {
        _boss.SetActive(true);
        _health.HealthPoint = _health.MaxHealth;
        gameObject.SetActive(false);
    }

    private void ResetBossRoom()
    {
        _boss.SetActive(false);
        gameObject.SetActive(true);
    }

    private void DestroyBreakableItem()
    {
        Destroy(gameObject);
    }
}
