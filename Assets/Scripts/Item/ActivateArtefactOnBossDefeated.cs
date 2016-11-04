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
            /* BEN_CORRECTION
             * 
             * Pourquoi ne pas aller plutôt chercher le composant "Health" et s'abonner
             * à un évènemetn "OnDeath". « OnBossDefeated » ne serait peut-être plus nécessaire (ou du
             * moins, dans sa forme actuelle).
             */
            _onBossDefeated = _artefactGuardian.GetComponent<OnBossDefeated>();
            _onBossDefeated.onDefeated += OnGuardianDefeated;
        }
    }

    private void Destroy()
    {
        if (_artefactGuardian != null && _onBossDefeated != null)
        {
            _onBossDefeated.onDefeated -= OnGuardianDefeated;
        }
    }

    private void OnGuardianDefeated()
    {
        gameObject.transform.parent.gameObject.SetActive(true);
    }
}
