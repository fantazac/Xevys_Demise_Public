using UnityEngine;
using System.Collections;

public class DestroyOnDeath : MonoBehaviour
{
    [SerializeField]
    private int _deathSoundIndex = -1;
    private AudioSource[] _audioSources;

    private Health _health;
    private Animator _animator;

    private void Start()
    {
        _health = GetComponent<Health>();
        _animator = GetComponent<Animator>();
        _audioSources = GetComponents<AudioSource>();
    }

    private void Update()
    {
        if (_health.HealthPoint <= 0)
        {
            _animator.SetBool("IsDying", true);
            if(_deathSoundIndex > -1)
            {
                foreach(AudioSource audioSource in _audioSources)
                {
                    audioSource.Stop();
                }
                _audioSources[_deathSoundIndex].Play();
            }
        }
    }

    protected void Destroy()
    {
        GetComponent<DropItems>().Drop();
        Destroy(gameObject);
    }
}
