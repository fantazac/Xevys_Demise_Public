using UnityEngine;
using System.Collections;

public class FallDamage: MonoBehaviour
{
    private Health _playerHealth;
    private PlayerMovement _playerMovement;

    private float _fallingCount = 0;

    private const float DAMAGE_MULTIPLIER = 50f;

    private void Start()
    {
        _playerHealth = GetComponent<Health>();
        _playerMovement = GetComponent<PlayerGroundMovement>();

        _playerMovement.OnFalling += OnFalling;
        _playerMovement.OnLanding += OnLanding;
    }

    private void OnFalling()
    {
        /*
         * BEN_REVIEW
         * 
         * Je pense l'avoir déjà mentionné, mais pourquoi ne pas utiliser la vélocité du joueur pour déterminer le "fall dammage" ?
         * 
         * Aussi simple que : 
         * 
         *  if (Vector2.Dot(rigidbody2D.velocity, Vector2.down) > 0.5f)
            {
                if (rigidbody2D.velocity.sqrMagnitude > 2)
                {
                    _playerHealth.Hit();
                }
            }

         * Me voir si pas clair. En passant, le produit scalaire (Dot Product) de deux vecteurs perpendiculaires est toujours 0, 
         * le produit scalaire de deux vecteurs opposés est toujours négatif et le produit scalaire de deux vecteurs dans la même 
         * direction est toujours positif.
         */
        _fallingCount += Time.deltaTime;
    }

    private void OnLanding()
    {
        if (_fallingCount > 1 && !PlayerState.IsInvincible && _playerHealth.HealthPoint > 0)
        {
            _playerHealth.Hit((int)Mathf.Clamp(_fallingCount * DAMAGE_MULTIPLIER, 
                _fallingCount * DAMAGE_MULTIPLIER, _playerHealth.HealthPoint), 
                new Vector2(transform.position.x, transform.position.y - 1));
        }

        _fallingCount = 0;
    }
}
