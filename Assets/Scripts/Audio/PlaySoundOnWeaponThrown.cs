using UnityEngine;
using System.Collections;

public class PlaySoundOnWeaponThrown : MonoBehaviour
{

    [SerializeField]
    private int _knifeSoundIndex = 1;

    [SerializeField]
    private int _axeSoundIndex = 2;

    private AudioSourcePlayer _audioSourcePlayer;

    private void Start()
    {
        GetComponent<ThrowAxe>().OnAxeAmmoUsed += PlayAxeThrownSound;
        GetComponent<ThrowKnife>().OnKnifeAmmoUsed += PlayKnifeThrownSound;

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
