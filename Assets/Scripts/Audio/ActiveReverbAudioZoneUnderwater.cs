using UnityEngine;
using System.Collections;

public class ActiveReverbAudioZoneUnderwater : MonoBehaviour
{
    private void Start()
    {
        GameObject player = StaticObjects.GetPlayer();
        PlayerFloatingInteraction playerFloatingInteraction = player.GetComponentInChildren<PlayerFloatingInteraction>();
        playerFloatingInteraction.OnPlayerUnderWater += ActivateReverbAudioZone;
        playerFloatingInteraction.OnPlayerOutOfWater += DisableReverbAudioZone;
    }

    private void ActivateReverbAudioZone()
    {
        GetComponent<AudioReverbZone>().enabled = true;
    }

    private void DisableReverbAudioZone()
    {
        GetComponent<AudioReverbZone>().enabled = false;
    }
}
