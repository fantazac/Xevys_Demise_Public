using UnityEngine;
using System.Collections;

public class DestroyOnDeath : MonoBehaviour
{
    [SerializeField]
    private int _deathSoundIndex = -1;
    private AudioSourcePlayer _audioSourcePlayer;

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
            if(_deathSoundIndex > -1)
            {
                _audioSourcePlayer.StopAll();
                _audioSourcePlayer.Play(_deathSoundIndex);
            }
        }
    }

    protected void Destroy()
    {
        GetComponent<DropItems>().Drop();
        Destroy(gameObject);
    }
}
