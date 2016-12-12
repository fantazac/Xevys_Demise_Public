using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{
    private AudioSource _music;

    private void Start()
    {
        _music = GetComponent<AudioSource>();
        MainMenuAudioSettingsManager.OnVolumeChanged += ChangeMusicVolume;
    }

    private void OnDestroy()
    {
        MainMenuAudioSettingsManager.OnVolumeChanged -= ChangeMusicVolume;
    }

    private void ChangeMusicVolume(bool isMusic, float volume)
    {
        if (isMusic)
        {
            _music.volume = volume;
        }
    }
}
