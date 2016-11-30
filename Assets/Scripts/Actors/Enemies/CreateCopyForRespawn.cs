using UnityEngine;
using System.Collections;

public class CreateCopyForRespawn : MonoBehaviour
{
    [SerializeField]
    private float _respawnTime = 10f;

    [SerializeField]
    private bool _isAParent = false;

    private WaitForSeconds _delayRespawn;

    private GameObject _copy;

    private void Start()
    {
        _delayRespawn = new WaitForSeconds(_respawnTime);

        if (_isAParent)
        {
            GetComponentInChildren<Health>().OnDeath += SpawnCopy;
        }
        else
        {
            GetComponent<Health>().OnDeath += SpawnCopy;
        }

        _copy = (GameObject)Instantiate(gameObject, gameObject.transform.position, gameObject.transform.rotation);
        _copy.transform.parent = gameObject.transform.parent;
        _copy.name = gameObject.name;
        _copy.SetActive(false);
    }

    private void SpawnCopy()
    {
        StaticObjects.GetRespawnEnemy().GetComponent<RespawnEnemy>().SpawnEnemy(_copy, _delayRespawn);
    }
}
