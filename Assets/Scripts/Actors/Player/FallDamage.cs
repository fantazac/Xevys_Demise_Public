using UnityEngine;
using System.Collections;

public class FallDamage: MonoBehaviour
{
    private Health _playerHealth;
    private PlayerMovement _playerMovement;
    private KnockbackOnDamageTaken _knockback;

    private float _fallingCount = 0;

    private void Start()
    {
        _playerHealth = StaticObjects.GetPlayer().GetComponent<Health>();
        _playerMovement = StaticObjects.GetPlayer().GetComponent<PlayerGroundMovement>();
        _knockback = StaticObjects.GetPlayer().GetComponent<KnockbackOnDamageTaken>();

        _playerMovement.OnFalling += OnFalling;
        _playerMovement.OnLanding += OnLanding;
    }

    private void OnFalling()
    {
        _fallingCount += Time.deltaTime;
    }

    private void OnLanding()
    {
        if (_fallingCount > 1 && !PlayerState.IsInvincible && _playerHealth && _playerHealth.HealthPoint > 0)
        {
            _playerHealth.Hit((int)Mathf.Clamp(_fallingCount * 50, _fallingCount * 50, _playerHealth.HealthPoint));
            _knockback.KnockbackPlayer(new Vector2(StaticObjects.GetPlayer().transform.position.x,
                StaticObjects.GetPlayer().transform.position.y - 1));
        }

        _fallingCount = 0;
    }
}
