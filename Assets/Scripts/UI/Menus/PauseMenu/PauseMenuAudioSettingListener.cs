using UnityEngine;

public class PauseMenuAudioSettingListener : MonoBehaviour
{
    [SerializeField]
    private int _audioSourceID = 0;
    private AudioSource _audioSource;
    private float _maxVolume;
    private bool _isMusic = false;

    private void Start()
    {
        if(tag == "MusicZone")
        {
            _isMusic = true;
        }
        /* BEN_CORRECTION
         * 
         * Comme j'ai dit dans "AccountSettings", ici, vous appèleriez "SoundVolumeManager", obtenu à partir
         * de "StaticObjects".
         */
        AudioSource[] audioSources = GetComponents<AudioSource>();
        _audioSource = audioSources[_audioSourceID];
        _maxVolume = _audioSource.volume;
        PauseMenuAudioSettingsController.OnVolumeChanged += SetVolume;
    }

    private void SetVolume(bool isMusic, float volume)
    {
        if(_isMusic == isMusic)
        {
            _audioSource.volume = volume * _maxVolume;
        }
    }

    private void OnDestroy()
    {
        PauseMenuAudioSettingsController.OnVolumeChanged -= SetVolume;
    }
}
