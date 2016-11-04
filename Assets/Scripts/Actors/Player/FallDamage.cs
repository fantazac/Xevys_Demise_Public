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
        _playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerGroundMovement>();
        _knockback = GameObject.FindGameObjectWithTag("Player").GetComponent<KnockbackOnDamageTaken>();
        _invincibility = GameObject.FindGameObjectWithTag("Player").GetComponent<InvincibilityAfterBeingHit>();

        _playerMovement.OnFalling += OnFalling;
        _playerMovement.OnLanding += OnLanding;
    }

    /* BEN_CORRECTION
     * 
     * Basé sur le nombre de frames, ce qui veut dire que si le framerate est très bas, le joueur
     * peut tomber de plus haut sans que cela lui fasse mal.
     * 
     * Je vous conseille plutôt de regarder la vélocité du joueur à l'atérissage (rigidbody2d.velocity)
     * pour déterminer si du "FallingDamage" doit être appliqué. Vérifiez qu'il va bien vers le bas (produit scalaire),
     * que la vitesse est assez haute (sqrMagnitude).
     */
    private void OnFalling()
    {
        _fallingCount++;
    }

    /* BEN_CORRECTION
     * 
     * Voir commentaire plus haut.
     */
    private void OnLanding()
    {
        if (_fallingCount > 50 && !_invincibility.IsFlickering && _playerHealth && _playerHealth.HealthPoint > 0)
        {
            /* BEN_CORRECTION
             * 
             * Comme je disais ailleurs, mieux vaut que KnockbackOnDamageTaken et InvincibilityAfterBeingHit soit
             * notifiés que le "Player" s'est fait mal via un évènement à partir du composant "Health". J'ai vu un 
             * code similaire ailleurs.
             */
            _playerHealth.Hit(Mathf.Clamp(_fallingCount * 4, _fallingCount * 4, (int)_playerHealth.HealthPoint));
            _knockback.KnockbackPlayer(new Vector2(GameObject.FindGameObjectWithTag("Player").transform.position.x, 
                GameObject.FindGameObjectWithTag("Player").transform.position.y - 1));
            _invincibility.StartFlicker();
        }

        _fallingCount = 0;
    }
}
