using UnityEngine;
using System.Collections;

public class HealthItemHeal : MonoBehaviour
{
    [SerializeField]
    private int _healPoints = 200;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player" && 
            GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().HealthPoint < GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().MaxHealth)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().Heal(_healPoints);
            Destroy(gameObject);
        }
    } 
}
