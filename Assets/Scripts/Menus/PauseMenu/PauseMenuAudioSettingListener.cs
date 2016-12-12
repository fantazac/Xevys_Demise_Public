using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct SingleAudioListener
{
    public int audioSourceID;
    public AudioSource audioSource;
    public float maxVolume;
    public bool isMusic;
}

public class PauseMenuAudioSettingListener : MonoBehaviour
{
    private List<SingleAudioListener> _audioListeners;
    private AudioSourcePlayer _audioSourcePlayer;

    private void Start()
    {
        InitialiseVolume(null);
        MainMenuAudioSettingsManager.OnVolumeChanged += SetVolume;
        SetVolume(false, PauseMenuAudioSettingsManager._sfxVolume);
    }

    public void InitialiseVolume(List<SingleAudioListener> singleAudioListeners)
    {
        if(_audioListeners == null)
        {
            _audioListeners = new List<SingleAudioListener>();
            _audioSourcePlayer = GetComponent<AudioSourcePlayer>();
            for (int i = 0; i < _audioSourcePlayer.GetAudioSourcesLength(); i++)
            {
                SingleAudioListener audioListener = new SingleAudioListener();
                AudioSource audioSource = _audioSourcePlayer.GetAudioSource(i);
                audioListener.audioSourceID = i;
                audioListener.audioSource = audioSource;
                audioListener.maxVolume = singleAudioListeners == null ? 
                    audioSource.volume : singleAudioListeners[i].maxVolume;
                if (gameObject.tag == StaticObjects.GetObjectTags().MusicZone)
                {
                    audioListener.isMusic = true;
                }
                _audioListeners.Add(audioListener);
            }
        }
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

    public List<SingleAudioListener> GetAudioListeners()
    {
        InitialiseVolume(_audioListeners);
        return _audioListeners;
    }

    private void OnDestroy()
    {
        MainMenuAudioSettingsManager.OnVolumeChanged -= SetVolume;
    }
}
