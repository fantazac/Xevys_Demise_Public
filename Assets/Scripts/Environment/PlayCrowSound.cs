using UnityEngine;
using System.Collections;

public class PlayCrowSound : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        GetComponent<AnimateCrowOnPlayerHit>().OnCrowAnimationStart += PlaySound;
    }

    private void PlaySound()
    {
        _audioSource.Play();
    }
}
