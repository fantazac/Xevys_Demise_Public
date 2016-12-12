using UnityEngine;
using System.Collections;

public class CreateCopyForRespawn : MonoBehaviour
{
    private GameObject _copy;

    private void Start()
    {
        _copy = (GameObject)Instantiate(gameObject, gameObject.transform.position, gameObject.transform.rotation);
        _copy.transform.parent = gameObject.transform.parent;
        _copy.name = gameObject.name;
        _copy.GetComponent<PauseMenuAudioSettingListener>().InitialiseVolume(GetComponent<PauseMenuAudioSettingListener>().AudioListeners);
        _copy.SetActive(false);
    }
}
