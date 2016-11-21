using UnityEngine;
using System.Collections;

public class PlaySoundOnWeaponThrown : MonoBehaviour
{

    [SerializeField]
    private int _knifeSoundIndex = 1;

    [SerializeField]
    private int _axeSoundIndex = 2;

    private ThrowKnife _throwKnifeAttack;
    private ThrowAxe _throwAxeAttack;

    private AudioSourcePlayer _audioSourcePlayer;

    private void Start()
    {
        _throwKnifeAttack = GetComponent<ThrowKnife>();
        _throwAxeAttack = GetComponent<ThrowAxe>();

        _throwAxeAttack.OnAxeAmmoUsed += PlayAxeThrownSound;
        _throwKnifeAttack.OnKnifeAmmoUsed += PlayKnifeThrownSound;

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
