using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioSettings : MonoBehaviour
{
    public delegate void OnVolumeChangedHandler(bool isMusic, float volume);
    public static event OnVolumeChangedHandler OnVolumeChanged;

    public void SetMusicVolume(float volume)
    {
        OnVolumeChanged(true, volume);
    }

    public void SetSoundVolume(float volume)
    {
        OnVolumeChanged(false, volume);
    }
}
