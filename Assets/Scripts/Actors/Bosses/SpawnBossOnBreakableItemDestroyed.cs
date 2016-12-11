using UnityEngine;
using System.Collections;

public class SpawnBossOnBreakableItemDestroyed : MonoBehaviour
{
    [SerializeField]
    private GameObject _boss;

    [SerializeField]
    private bool _destroyOnBossDefeated = true;

    private GameObject _bossInstance;

    private Health _health;
    private Health _playerHealth;
    private Health _bossHealth;

    public delegate void OnBossFightEnabledHandler();
    public event OnBossFightEnabledHandler OnBossFightEnabled;

    public delegate void OnBossSpawnHandler(GameObject bossInstance);
    public event OnBossSpawnHandler OnBossSpawn;

    public delegate void OnBossFightDisabledHandler();
    public event OnBossFightDisabledHandler OnBossFightDisabled;

    public delegate void OnBossFightFinishedHandler();
    public event OnBossFightFinishedHandler OnBossFightFinished;

    private void Start()
    {
        _health = GetComponent<Health>();
        _health.OnDeath += EnableBossFight;

        _playerHealth = StaticObjects.GetPlayer().GetComponent<Health>();
        _playerHealth.OnDeath += ResetBossRoom;

        _boss.SetActive(false);
    }

    private void EnableBossFight()
    {
        _bossInstance = (GameObject)Instantiate(_boss, _boss.transform.position, new Quaternion());

        _bossHealth = _bossInstance.GetComponent<Health>();
        if (_bossHealth == null)
        {
            _bossHealth = _bossInstance.GetComponentInChildren<Health>();
        }
        _bossHealth.OnDeath += BossFightFinished;

        _bossInstance.SetActive(true);

        if(OnBossSpawn != null)
        {
            OnBossSpawn(_bossInstance);
        }
        
        OnBossFightEnabled();
    }

    private void ResetBossRoom()
    {
        if (_bossInstance != null)
        {
            Destroy(_bossInstance); 
        }
        _health.HealthPoint = _health.MaxHealth;
        gameObject.SetActive(true);

        OnBossFightDisabled();
    }

    private void BossFightFinished()
    {
        OnBossFightFinished();

        if (_destroyOnBossDefeated)
        {
            _playerHealth.OnDeath -= ResetBossRoom;
            Destroy(_boss);
            Destroy(gameObject);
        }
    }
}
