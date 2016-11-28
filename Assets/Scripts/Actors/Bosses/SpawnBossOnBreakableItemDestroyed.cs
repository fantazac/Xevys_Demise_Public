using UnityEngine;
using System.Collections;

public class SpawnBossOnBreakableItemDestroyed : MonoBehaviour
{
    [SerializeField]
    private GameObject _boss;

    [SerializeField]
    private bool _instanciateBoss = false;

    private GameObject _bossInstance;

    private Health _health;
    private Health _playerHealth;
    private Health _bossHealth;

    private void Start ()
    {
        _health = GetComponent<Health>();
        _health.OnDeath += EnableBossFight;

        if (!_instanciateBoss)
        {
            _bossHealth = _boss.GetComponent<Health>();
            if (_bossHealth == null)
            {
                _bossHealth = _boss.GetComponentInChildren<Health>();
            }
            _bossHealth.OnDeath += DestroyBreakableItem;
        }

        _playerHealth =  StaticObjects.GetPlayer().GetComponent<Health>();
        _playerHealth.OnDeath += ResetBossRoom;

        _boss.SetActive(false);
	}

    private void EnableBossFight()
    {
        if (_instanciateBoss)
        {
            _bossInstance = (GameObject)Instantiate(_boss, _boss.transform.position, new Quaternion());
            _bossHealth = _bossInstance.GetComponent<Health>();
            if (_bossHealth == null)
            {
                _bossHealth = _boss.GetComponentInChildren<Health>();
            }
            _bossHealth.OnDeath += DestroyBreakableItem;
            _bossInstance.SetActive(true);
        }
        else
        {
            _boss.SetActive(true);
        }
        _health.HealthPoint = _health.MaxHealth;
        gameObject.SetActive(false);
    }

    private void ResetBossRoom()
    {
        if (_instanciateBoss)
        {
            Destroy(_bossInstance);
        }
        else
        {
            _boss.SetActive(false);
        }
        
        gameObject.SetActive(true);
    }

    private void DestroyBreakableItem()
    {
        _playerHealth.OnDeath -= ResetBossRoom;
        _boss.GetComponent<AudioSourcePlayer>().Stop();
        Destroy(gameObject);
    }
}
