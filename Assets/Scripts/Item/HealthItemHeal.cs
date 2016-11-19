using UnityEngine;
using System.Collections;

public class HealthItemHeal : MonoBehaviour
{
    [SerializeField]
    private int _healPoints = 200;

    private Health _playerHealth;

    private void Start()
    {
        _playerHealth = StaticObjects.GetPlayer().GetComponent<Health>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" && _playerHealth.CanHeal())
        {
            _playerHealth.Heal(_healPoints);
            Destroy(gameObject);
        }
    } 
}
