using UnityEngine;
using System.Collections;

public class ActivateArtefactOnBossDefeated : MonoBehaviour {

    [SerializeField]
    private GameObject _artefactGuardian;

    private Health _artefactGuardianHealth;

    private void Start()
    {
        if (_artefactGuardian != null)
        {
            gameObject.transform.parent.gameObject.SetActive(false);
            _artefactGuardianHealth = _artefactGuardian.GetComponent<Health>();
            _artefactGuardianHealth.OnDeath += OnGuardianDefeated;
        }
    }

    private void Destroy()
    {
        if (_artefactGuardian != null && _artefactGuardianHealth != null)
        {
            _artefactGuardianHealth.OnDeath -= OnGuardianDefeated;
        }
    }

    private void OnGuardianDefeated()
    {
        gameObject.transform.parent.gameObject.SetActive(true);
    }
}
