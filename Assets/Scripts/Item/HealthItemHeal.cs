using UnityEngine;
using System.Collections;

public class HealthItemHeal : MonoBehaviour
{
    [SerializeField]
    private int _healPoints = 200;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" && collider.GetComponent<Health>().CanHeal())
        {
            collider.GetComponent<Health>().Heal(_healPoints);
            Destroy(gameObject);
        }
    } 
}
