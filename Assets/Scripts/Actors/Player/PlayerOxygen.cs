using UnityEngine;
using System.Collections;

public class PlayerOxygen : MonoBehaviour
{
    [SerializeField]
    private float _timeBeforeOxygenMissing = 5f;

    [SerializeField]
    private float _intervalBetweenHits = 1.5f;

    [SerializeField]
    private int _damageOnHit = 50;

    public delegate void OnOxygenCountHandler(int index);
    public event OnOxygenCountHandler OnOxygenCount;

    private WaitForSeconds _delayBetweenHits;

    private PlayerFloatingInteraction _playerFloating;
    private PlayerWaterMovement _playerWaterMovement;
    private Health _playerHealth;
    private InventoryManager _inventoryManager;
    private PlayerState _playerState;

    private void Start()
    {
        _playerState = StaticObjects.GetPlayerState();

        _playerHealth = GetComponent<Health>();

        _playerWaterMovement = GetComponent<PlayerWaterMovement>();
        _inventoryManager = GetComponent<InventoryManager>();

        _delayBetweenHits = new WaitForSeconds(_intervalBetweenHits);

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
        StopAllCoroutines();
    }

    private IEnumerator DamageIfMissingOxygen()
    {
        // J'ai du utiliser un compteur manuel au lieu d'un Time.deltatime
        // car il faut pouvoir connaître la valeur du compteur à un moment donné

        float counter = 0;
        int nbOfSecondsPassed = 0;
        while (nbOfSecondsPassed <= _timeBeforeOxygenMissing)
        {           
            counter += Time.deltaTime;
            if (counter >= 1)
            {               
                nbOfSecondsPassed++;
                counter = 0;
                OnOxygenCount(nbOfSecondsPassed);
            }
            yield return null;
        }
        
        while (_playerWaterMovement.enabled && !_playerState.IsFloating)
        {
            _playerHealth.Hit(_damageOnHit, Vector2.zero);

            yield return _delayBetweenHits;
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
