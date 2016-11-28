using UnityEngine;
using System.Collections;

public class ActivateArtefactOnBossDefeated : MonoBehaviour {

    [SerializeField]
    private GameObject _artefact;

    [SerializeField]
    private bool _spawnOnDeathSpot = true;

    private GameObject _parent;

    private void Start()
    {
        _parent = _artefact.transform.parent.gameObject;
        _parent.SetActive(false);
        GetComponent<Health>().OnDeath += OnBossDefeated;
    }

    private void OnBossDefeated()
    {
        if (_spawnOnDeathSpot)
        {
            _parent.transform.position = transform.position;
        }
        
        _parent.SetActive(true);
    }
}
