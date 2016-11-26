using UnityEngine;
using System.Collections;

public class ActiveReverbAudioZoneUnderwater : MonoBehaviour
{
    private AudioReverbZone _audioReverbZone;
    private PlayerFloatingInteraction playerFloatingInteraction;

    private void Start()
    {
        _audioReverbZone = GetComponent<AudioReverbZone>();
        playerFloatingInteraction = StaticObjects.GetPlayer().GetComponentInChildren<PlayerFloatingInteraction>();
        playerFloatingInteraction.OnPlayerUnderWater += ActivateReverbAudioZone;
        playerFloatingInteraction.OnPlayerOutOfWater += DisableReverbAudioZone;
    }

    private void ActivateReverbAudioZone()
    {
        _audioReverbZone.enabled = true;
    }

    private void DisableReverbAudioZone()
    {
        _audioReverbZone.enabled = false;
    }
}
