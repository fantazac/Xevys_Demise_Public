using UnityEngine;
using System.Collections;

public class PlayerOxygen : MonoBehaviour
{
    [SerializeField]
    private float _timeBeforeOxygenMissing = 5;

    [SerializeField]
    private float _intervalBetweenHits = 1.5f;

    [SerializeField]
    private int _damageOnHit = 50;

    private PlayerFloatingInteraction _playerFloating;
    private PlayerWaterMovement _playerWaterMovement;
    private Health _playerHealth;

    private void Start ()
    {
        _playerFloating = GetComponentInChildren<PlayerFloatingInteraction>();
        _playerHealth = GetComponent<Health>();
        _playerFloating.OnPlayerUnderWater += OnPlayerUnderWater;
        _playerWaterMovement = GetComponent<PlayerWaterMovement>();
    }

    private void OnPlayerUnderWater()
    {
        StartCoroutine("OxygenManagerCoroutine");
    }

    private IEnumerator OxygenManagerCoroutine()
    {
        yield return new WaitForSeconds(_timeBeforeOxygenMissing);
        while (_playerWaterMovement.enabled && !_playerWaterMovement.IsFloating)
        {
            _playerHealth.Hit(_damageOnHit, Vector2.zero);
            Debug.Log(time);
            time = 0;
            yield return new WaitForSeconds(_intervalBetweenHits);
        }
    }
    float time = 0;
    private void Update()
    {
        time += Time.deltaTime;
    }
}
