using UnityEngine;
using System.Collections;

public class SpawnBossOnPlayerEnterRoom : MonoBehaviour
{
    [SerializeField]
    private GameObject _boss;

    private void Start ()
    {
        _boss.SetActive(false);
	}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            _boss.SetActive(true);
            Destroy(gameObject);
        }
    }
}
