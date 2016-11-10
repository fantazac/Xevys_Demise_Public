using UnityEngine;
using System.Collections;

public class ActivateArtefactOnBossDefeated : MonoBehaviour {

    [SerializeField]
    private GameObject _artefactGuardian;

    private OnBossDefeated _onBossDefeated;

    private void Start()
    {
        if (_artefactGuardian != null)
        {
            gameObject.transform.parent.gameObject.SetActive(false);
            _onBossDefeated = _artefactGuardian.GetComponent<OnBossDefeated>();
            _onBossDefeated.OnDefeated += OnGuardianDefeated;
        }
    }

    private void Destroy()
    {
        if (_artefactGuardian != null && _onBossDefeated != null)
        {
            _onBossDefeated.OnDefeated -= OnGuardianDefeated;
        }
    }

    private void OnGuardianDefeated()
    {
        gameObject.transform.parent.gameObject.SetActive(true);
    }
}
