using UnityEngine;
using System.Collections;

public class ActivateArtefactOnBossDefeated : MonoBehaviour {

    [SerializeField]
    private GameObject _boss;

    public GameObject Boss { get { return _boss; } }

    private Health _artefactGuardianHealth;

    private void Start()
    {
        if (_boss != null)
        {
            gameObject.transform.parent.gameObject.SetActive(false);
            _artefactGuardianHealth = _boss.GetComponent<Health>();
            _artefactGuardianHealth.OnDeath += OnGuardianDefeated;
        }
    }

    private void Destroy()
    {
        if (_boss != null && _artefactGuardianHealth != null)
        {
            _artefactGuardianHealth.OnDeath -= OnGuardianDefeated;
        }
    }

    private void OnGuardianDefeated()
    {
        gameObject.transform.parent.gameObject.SetActive(true);
    }
}
