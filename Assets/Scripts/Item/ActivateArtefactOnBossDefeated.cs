using UnityEngine;
using System.Collections;

public class ActivateArtefactOnBossDefeated : MonoBehaviour {

    [SerializeField]
    private GameObject _boss;

    public GameObject Boss { get { return _boss; } }

    private Health _bossHealth;

    private void Start()
    {
        gameObject.transform.parent.gameObject.SetActive(false);
        _boss.GetComponent<Health>().OnDeath += OnBossDefeated;
    }

    private void OnBossDefeated()
    {
        gameObject.transform.parent.gameObject.SetActive(true);
    }
}
