using UnityEngine;
using System.Collections;

public class ActivateArtefactOnBossDefeated : MonoBehaviour {

    [SerializeField]
    private GameObject _boss;

    public GameObject Boss { get { return _boss; } }

    private Health _bossHealth;
    private GameObject _parent;

    private void Start()
    {
        _parent = gameObject.transform.parent.gameObject;
        _parent.SetActive(false);
        _boss.GetComponent<Health>().OnDeath += OnBossDefeated;
    }

    private void OnBossDefeated()
    {
        _parent.SetActive(true);
    }
}
