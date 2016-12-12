using System.Collections.Generic;
using System.Linq;
using UnityEngine;

struct SingleAudioListener
{
    public int audioSourceID;
    public AudioSource audioSource;
    public float maxVolume;
    public bool isMusic;
}

public class PauseMenuAudioSettingListener : MonoBehaviour
{
    private List<SingleAudioListener> _audioListeners;

    private void Start()
    {
        _audioListeners = new List<SingleAudioListener>();
        AudioSourcePlayer audioSourcePlayer = GetComponent<AudioSourcePlayer>();
        for(int i = 0; i < audioSourcePlayer.GetAudioSourcesLength(); i++)
        {
            SingleAudioListener audioListener = new SingleAudioListener();
            AudioSource audioSource = audioSourcePlayer.GetAudioSource(i);
            audioListener.audioSourceID = i;
            audioListener.audioSource = audioSource;
            audioListener.maxVolume = audioSource.volume;
            if (gameObject.tag == "MusicZone")
            {
                audioListener.isMusic = true;
            }
            _audioListeners.Add(audioListener);
        }
        MainMenuAudioSettingsManager.OnVolumeChanged += SetVolume;
        SetVolume(false, PauseMenuAudioSettingsManager._sfxVolume);
    }

    public void SetVolume(bool isMusic, float volume)
    {
        if(_audioListeners.First().isMusic == isMusic)
        {
            foreach (SingleAudioListener audioListener in _audioListeners)
            {
                audioListener.audioSource.volume = volume * audioListener.maxVolume;
            }
        }
    }

    private void OnDestroy()
    {
        MainMenuAudioSettingsManager.OnVolumeChanged -= SetVolume;
    }
}
