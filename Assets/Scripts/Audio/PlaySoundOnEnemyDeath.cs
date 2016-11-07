using UnityEngine;
using System.Collections;

public class PlaySoundOnEnemyDeath : MonoBehaviour
{
    [SerializeField]
    private int _deathSoundIndex = -1;

    private Health _health;

    private AudioSourcePlayer _audioSourcePlayer;

    public delegate void OnDeathSoundFinishedHandler();
    public event OnDeathSoundFinishedHandler OnDeathSoundFinished;

    private float _soundDuration = 0;

    private void Start()
    {
        _health = GetComponent<Health>();
        _health.OnDeath += PlayDeathSound;

        _audioSourcePlayer = GetComponent<AudioSourcePlayer>();
    }

    private void PlayDeathSound()
    {
        if (_deathSoundIndex > -1)
        {
            _audioSourcePlayer.StopAll();
            _audioSourcePlayer.Play(_deathSoundIndex);
            _soundDuration = _audioSourcePlayer.GetAudioSource(_deathSoundIndex).clip.length;
        }
        Invoke("DeathSoundFinished", _soundDuration);
    }

    private void DeathSoundFinished()
    {
        OnDeathSoundFinished();
    }
}
