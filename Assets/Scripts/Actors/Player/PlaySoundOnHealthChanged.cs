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

    private void Start()
    {
        _health = GetComponent<Health>();
        _health.OnDamageTaken += PlayHitSound;
        _health.OnHeal += PlayHealSound;

        _audioSourcePlayer = GetComponent<AudioSourcePlayer>();
    }

    private void PlayHitSound(int hitPoints)
    {
        _audioSourcePlayer.Play(_hitSoundIndex);
    }

    private void PlayHealSound(int hitPoints)
    {
        _audioSourcePlayer.Play(_healSoundIndex);
    }
}
