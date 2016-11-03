using UnityEngine;
using System.Collections;

public class PlaySoundOnBatMovement : MonoBehaviour
{

    [SerializeField]
    private int _batMovementSoundIndex = 0;

    private BatMovement _batMovement;

    private AudioSourcePlayer _audioSourcePlayer;

    private void Start()
    {
        _batMovement = GetComponent<BatMovement>();
        _batMovement.OnBatMovement += PlayFlyingSound;
        _batMovement.OnBatReachedTarget += StopFlyingSound;

        _audioSourcePlayer = GetComponent<AudioSourcePlayer>();
    }

    private void PlayFlyingSound()
    {
        _audioSourcePlayer.Play(_batMovementSoundIndex);
    }

    private void StopFlyingSound()
    {
        _audioSourcePlayer.Stop(_batMovementSoundIndex);
    }

}
