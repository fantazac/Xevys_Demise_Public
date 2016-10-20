using UnityEngine;
using System.Collections;

public class FallDamage: MonoBehaviour
{
    private Health _playerHealth;
    private PlayerMovement _playerMovement;
    private KnockbackOnDamageTaken _knockback;
    private InvincibilityAfterBeingHit _invincibility;

    private int _fallingCount = 0;

    private void Start()
    {
        _playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        _playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        _knockback = GameObject.FindGameObjectWithTag("Player").GetComponent<KnockbackOnDamageTaken>();
        _invincibility = GameObject.FindGameObjectWithTag("Player").GetComponent<InvincibilityAfterBeingHit>();

        _playerMovement.OnFalling += OnFalling;
        _playerMovement.OnLanding += OnLanding;
    }

    private void OnFalling()
    {
        _fallingCount++;
    }

    private void OnLanding()
    {
        if (_fallingCount > 50 && !_invincibility.IsFlickering && _playerHealth && _playerHealth.HealthPoint > 0)
        {
            _playerHealth.Hit(Mathf.Clamp(_fallingCount * 5, _fallingCount * 5, (int)_playerHealth.HealthPoint));
            _knockback.KnockbackPlayer(new Vector2(GameObject.FindGameObjectWithTag("Player").transform.position.x, 
                GameObject.FindGameObjectWithTag("Player").transform.position.y - 1));
            _invincibility.StartFlicker();
        }

        _fallingCount = 0;
    }
}
