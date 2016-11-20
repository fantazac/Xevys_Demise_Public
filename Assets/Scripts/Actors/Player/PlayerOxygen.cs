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
    private InventoryManager _inventoryManager;

    private void Start()
    {
        _playerHealth = GetComponent<Health>();
        _playerWaterMovement = GetComponent<PlayerWaterMovement>();
        _inventoryManager = GetComponent<InventoryManager>();

        _playerFloating = GetComponentInChildren<PlayerFloatingInteraction>();
        _playerFloating.OnPlayerUnderWater += OnPlayerUnderWater;
        _playerFloating.OnPlayerOutOfWater += OnPlayerOutOfWater;
    }

    private void OnPlayerUnderWater()
    {
        if (!_inventoryManager.BubbleEnabled)
        {
            StartCoroutine(DamageIfMissingOxygen());
        }        
    }

    private void OnPlayerOutOfWater()
    {
        StopCoroutine(DamageIfMissingOxygen());
    }

    private IEnumerator DamageIfMissingOxygen()
    {
        yield return new WaitForSeconds(_timeBeforeOxygenMissing);

        while (_playerWaterMovement.enabled && !_playerWaterMovement.IsFloating)
        {
            _playerHealth.Hit(_damageOnHit, Vector2.zero);

            yield return new WaitForSeconds(_intervalBetweenHits);
        }
    }
}
