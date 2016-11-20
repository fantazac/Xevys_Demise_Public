using UnityEngine;
using System.Collections;

public class PlaySoundOnWeaponThrown : MonoBehaviour
{

    [SerializeField]
    private int _knifeSoundIndex = 1;

    [SerializeField]
    private int _axeSoundIndex = 2;

    private PlayerThrowAttack _throwAttack;

    private AudioSourcePlayer _audioSourcePlayer;

    private void Start()
    {
        _throwAttack = GetComponent<PlayerThrowAttack>();
        _throwAttack.OnAxeAmmoUsed += PlayAxeThrownSound;
        _throwAttack.OnKnifeAmmoUsed += PlayKnifeThrownSound;

        _audioSourcePlayer = GetComponent<AudioSourcePlayer>();
    }

    private void PlayKnifeThrownSound(int ammoUsedOnThrow)
    {
        _audioSourcePlayer.Play(_knifeSoundIndex);
    }

    private void PlayAxeThrownSound(int ammoUsedOnThrow)
    {
        _audioSourcePlayer.Play(_axeSoundIndex);
    }

}
