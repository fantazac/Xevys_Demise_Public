using UnityEngine;
using System.Collections;

/* BEN_CORRECTION
 * 
 * Je ne peut pas m'empêcher de constater que cela fait plus que juste "DestroyOnDeath". Cela
 * déclanche l'animation de mort, cela déclanche le son de mort, cela désactive la physique
 * et cela instancie un "Drop".
 * 
 * Donc, quatre composants :
 * 
 *  - PlayDeathAnimationOnDeath
 *  - PlayDeathSoundOnDeath
 *  - DropItemOnDeathAnimationEnd
 *  - DestroyOnDeathAnimationEnd
 */
public class DestroyOnDeath : MonoBehaviour
{
    [SerializeField]
    private int _deathSoundIndex = -1;
    private AudioSourcePlayer _audioSourcePlayer;
    private bool _deathSoundPlayed = false;

    private Health _health;
    private Animator _animator;

    private void Start()
    {
        _health = GetComponent<Health>();
        _animator = GetComponent<Animator>();
        _audioSourcePlayer = GetComponent<AudioSourcePlayer>();
    }

    private void Update()
    {
        if (_health.HealthPoint <= 0)
        {
            _animator.SetBool("IsDying", true);
            if(_deathSoundIndex > -1 && !_deathSoundPlayed)
            {
                _audioSourcePlayer.StopAll();
                _audioSourcePlayer.Play(_deathSoundIndex);
                _deathSoundPlayed = true;
                GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }

    /* BEN_REVIEW
     * 
     * J'ai de la misère à trouver quand c'est appelé. Quelque qu'une peut me dire où ?
     */
    protected void Destroy()
    {
        GetComponent<DropItems>().Drop();
        Destroy(gameObject);
    }
}
