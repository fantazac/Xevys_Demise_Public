using UnityEngine;
using System.Collections;

public class DestroyOnDeath : MonoBehaviour
{
    [SerializeField]
    private int _deathSoundIndex = -1;

    [SerializeField]
    private bool _destroyParent = false;

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

    protected void Destroy()
    {
        GetComponent<DropItems>().Drop();
        Destroy(gameObject);
        if (_destroyParent)
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
