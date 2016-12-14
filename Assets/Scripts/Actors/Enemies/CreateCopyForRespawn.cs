using UnityEngine;
using System.Collections;

public class CreateCopyForRespawn : MonoBehaviour
{
    private GameObject _copy;
    private PauseMenuAudioSettingListener _copyPauseMenuAudio;

    private void Start()
    {
        _copy = (GameObject)Instantiate(gameObject, gameObject.transform.position, gameObject.transform.rotation);
        _copy.transform.parent = gameObject.transform.parent;
        _copy.name = gameObject.name;
        _copyPauseMenuAudio = _copy.GetComponent<PauseMenuAudioSettingListener>();
        if(_copyPauseMenuAudio == null)
        {
            _copyPauseMenuAudio = _copy.GetComponentInChildren<PauseMenuAudioSettingListener>();
            _copyPauseMenuAudio.InitialiseVolume(GetComponentInChildren<PauseMenuAudioSettingListener>().GetAudioListeners());
        }
        else
        {
            _copyPauseMenuAudio.InitialiseVolume(GetComponent<PauseMenuAudioSettingListener>().GetAudioListeners());
        }
        
        _copy.SetActive(false);
    }
}
