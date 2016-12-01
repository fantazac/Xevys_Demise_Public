using UnityEngine;
using System.Collections;

public class PlaySoundOnHealthChanged : MonoBehaviour
{
    [SerializeField]
    private int _hitSoundIndex = 3;

    [SerializeField]
    private int _healSoundIndex = 4;

    private Health _health;

    private AudioSourcePlayer _audioSourcePlayer;

    private WaitForSeconds _delayHitSound;

    public delegate void OnHitSoundFinishedHandler();
    public event OnHitSoundFinishedHandler OnHitSoundFinished;

    private void Start()
    {
        _health = GetComponent<Health>();
        _health.OnDamageTaken += PlayHitSound;
        _health.OnHeal += PlayHealSound;

        _audioSourcePlayer = GetComponent<AudioSourcePlayer>();

        _delayHitSound = new WaitForSeconds(_audioSourcePlayer.GetAudioSource(_hitSoundIndex).clip.length);
    }

    private void PlayHitSound(int hitPoints)
    {
        _audioSourcePlayer.Play(_hitSoundIndex);
        StartCoroutine(FinishHitSound());
    }

    private void PlayHealSound(int hitPoints)
    {
        _audioSourcePlayer.Play(_healSoundIndex);
    }

    private IEnumerator FinishHitSound()
    {
        yield return _delayHitSound;

        if (OnHitSoundFinished != null)
        {
            OnHitSoundFinished();
        }
    }
}
