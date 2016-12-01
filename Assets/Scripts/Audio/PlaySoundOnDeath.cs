using UnityEngine;
using System.Collections;

public class PlaySoundOnDeath : MonoBehaviour
{
    [SerializeField]
    private int _deathSoundIndex = -1;

    private AudioSourcePlayer _audioSourcePlayer;

    public delegate void OnDeathSoundFinishedHandler();
    public event OnDeathSoundFinishedHandler OnDeathSoundFinished;

    private float _soundDuration = 0;

    private Health _playerHealth;
    private WaitForSeconds _finishedSoundDelay;

    private void Start()
    {
        if (gameObject.tag == StaticObjects.GetUnityTags().Player)
        {
            _playerHealth = GetComponent<Health>();
            GetComponent<PlaySoundOnHealthChanged>().OnHitSoundFinished += CheckIfPlayerIsDead;
        }
        else
        {
            GetComponent<Health>().OnDeath += PlayDeathSound;
        }

        _audioSourcePlayer = GetComponent<AudioSourcePlayer>();

        _soundDuration = _audioSourcePlayer.GetAudioSource(_deathSoundIndex).clip.length;
        _finishedSoundDelay = new WaitForSeconds(_soundDuration);
    }

    private void CheckIfPlayerIsDead()
    {
        if (_playerHealth.IsDead())
        {
            PlayDeathSound();
        }
    }

    private void PlayDeathSound()
    {
        _audioSourcePlayer.StopAll();
        _audioSourcePlayer.Play(_deathSoundIndex);

        StartCoroutine(DeathSoundFinished());
    }

    private IEnumerator DeathSoundFinished()
    {
        yield return _finishedSoundDelay;

        if (OnDeathSoundFinished != null)
        {
            OnDeathSoundFinished();
        }
    }
}
