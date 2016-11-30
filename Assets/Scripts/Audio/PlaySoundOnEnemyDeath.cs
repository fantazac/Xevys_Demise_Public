using UnityEngine;
using System.Collections;

public class PlaySoundOnEnemyDeath : MonoBehaviour
{
    [SerializeField]
    private int _deathSoundIndex = -1;

    private AudioSourcePlayer _audioSourcePlayer;

    public delegate void OnDeathSoundFinishedHandler();
    public event OnDeathSoundFinishedHandler OnDeathSoundFinished;

    private float _soundDuration = 0;

    private WaitForSeconds _finishedSoundDelay;

    private void Start()
    {
        GetComponent<Health>().OnDeath += PlayDeathSound;

        _audioSourcePlayer = GetComponent<AudioSourcePlayer>();

        _soundDuration = _audioSourcePlayer.GetAudioSource(_deathSoundIndex).clip.length;
        _finishedSoundDelay = new WaitForSeconds(_soundDuration);
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
