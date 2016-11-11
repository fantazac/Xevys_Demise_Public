using UnityEngine;
using System.Collections;

public class PlayerOxygen : MonoBehaviour
{
    [SerializeField]
    private float TIME_BEFORE_OXYGEN_MISSING = 5;
    [SerializeField]
    private float INTERVAL_BETWEEN_HITS = 2;

    private PlayerFloatingInteraction _playerFloating;
    private Health _playerHealth;
    private float _oxygenCounter = 0;
    private bool _coroutineStarted = false;
    private bool _playerGotOutOfWater = false;

    private void Start ()
    {
        _playerFloating = StaticObjects.GetPlayer().GetComponentInChildren<PlayerFloatingInteraction>();
        _playerHealth = StaticObjects.GetPlayer().GetComponent<Health>();
        _playerFloating.OnPlayerUnderWater += OnPlayerUnderWater;
        _playerFloating.OnPlayerOutOfWater += OnPlayerOutOfWater;
    }

    private void OnPlayerUnderWater()
    {
        if (!_coroutineStarted)
        {
            _playerGotOutOfWater = false;
            StartCoroutine("OxygenManagerCoroutine");
        }     
    }

    private void OnPlayerOutOfWater()
    {
        _playerGotOutOfWater = true;
    }

    private IEnumerator OxygenManagerCoroutine()
    {
        _coroutineStarted = true;
        while (_oxygenCounter < TIME_BEFORE_OXYGEN_MISSING)
        {
            if (_playerGotOutOfWater)
            {
                _coroutineStarted = false;
                break;
            }
            _oxygenCounter += Time.deltaTime;
            yield return null;
        }

        _oxygenCounter = 0;
        while (_playerHealth.HealthPoint >= 0)
        {
            if (_playerGotOutOfWater)
            {
                _coroutineStarted = false;
                break;
            }
            _oxygenCounter += Time.deltaTime;
            if (_oxygenCounter >= INTERVAL_BETWEEN_HITS)
            {
                _oxygenCounter = 0;
                _playerHealth.Hit(100, Vector2.zero);            
            }
            yield return null;
        }
        _coroutineStarted = false;
    }
}
