using UnityEngine;

public class ChangeMusicZoneOnDeath : MonoBehaviour
{
    [SerializeField]
    private GameObject _bossbattleMusicZone;

    [SerializeField]
    private GameObject _musicZone;

    private void Start()
    {
        GetComponent<Health>().OnDeath += ChangeMusicZone;
        _musicZone.SetActive(false);
        _bossbattleMusicZone.GetComponent<AudioSourcePlayer>().Play();
    }

    private void ChangeMusicZone()
    {
        _bossbattleMusicZone.GetComponent<AudioSourcePlayer>().Stop();
        _musicZone.SetActive(true);
    }
}
