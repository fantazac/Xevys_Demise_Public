using UnityEngine;
using System.Collections;

public class SpawnBossOnBreakableItemDestroyed : MonoBehaviour
{
    [SerializeField]
    private GameObject _boss;

    private Health _health;
    private Health _playerHealth;

    private void Start ()
    {
        _health = GetComponent<Health>();
        _health.OnDeath += EnableBossFight;

        _boss.GetComponent<Health>().OnDeath += DestroyBreakableItem;

        _playerHealth =  StaticObjects.GetPlayer().GetComponent<Health>();
        _playerHealth.OnDeath += ResetBossRoom;

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
        _playerHealth.OnDeath -= ResetBossRoom;
        Destroy(gameObject);
    }
}
