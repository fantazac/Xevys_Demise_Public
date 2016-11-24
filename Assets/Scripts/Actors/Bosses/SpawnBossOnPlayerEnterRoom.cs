using UnityEngine;
using System.Collections;

public class SpawnBossOnPlayerEnterRoom : MonoBehaviour
{
    [SerializeField]
    private GameObject _boss;
    [SerializeField]
    private GameObject _lockedDoor;
    [SerializeField]
    private GameObject _artefact;

    private void Start ()
    {
        _boss.SetActive(false);
        _lockedDoor.SetActive(false);
        _artefact.GetComponent<ActivateTrigger>().OnTrigger += DestroyTrigger;
	}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            _boss.SetActive(true);
            _lockedDoor.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            _boss.SetActive(false);
            _lockedDoor.SetActive(false);
        }
    }

    private void DestroyTrigger()
    {
        Destroy(gameObject);
    }
}
