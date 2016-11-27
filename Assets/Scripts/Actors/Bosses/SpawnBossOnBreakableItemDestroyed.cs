using UnityEngine;
using System.Collections;

public class SpawnBossOnBreakableItemDestroyed : MonoBehaviour
{
    [SerializeField]
    private GameObject _boss;

    private Health _health;
    private Health _playerHealth;
    private Health _bossHealth;

    private void Start ()
    {
        _health = GetComponent<Health>();
        _health.OnDeath += EnableBossFight;

        _bossHealth = _boss.GetComponent<Health>();
        if(_bossHealth == null)
        {
            _bossHealth = _boss.GetComponentInChildren<Health>();
        }
        _bossHealth.OnDeath += DestroyBreakableItem;

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
        _boss.GetComponent<AudioSourcePlayer>().Stop();
        Destroy(gameObject);
    }
}
