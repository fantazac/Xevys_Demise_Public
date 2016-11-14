using UnityEngine;
using System.Collections;

public class FallDamage: MonoBehaviour
{
    private Health _playerHealth;
    private PlayerMovement _playerMovement;

    private float _fallingCount = 0;

    private void Start()
    {
        _playerHealth = GetComponent<Health>();
        _playerMovement = GetComponent<PlayerGroundMovement>();

        _playerMovement.OnFalling += OnFalling;
        _playerMovement.OnLanding += OnLanding;
    }

    private void OnFalling()
    {
        _fallingCount += Time.deltaTime;
    }

    private void OnLanding()
    {
        if (_fallingCount > 1 && !PlayerState.IsInvincible && _playerHealth.HealthPoint > 0)
        {
            //qu'est-ce qui se passe ici? alex
            _playerHealth.Hit((int)Mathf.Clamp(_fallingCount * 50, _fallingCount * 50, _playerHealth.HealthPoint), 
                new Vector2(transform.position.x, transform.position.y - 1));
        }

        _fallingCount = 0;
    }
}
