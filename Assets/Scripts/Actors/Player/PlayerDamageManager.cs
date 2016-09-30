using UnityEngine;
using System.Collections;

public class PlayerDamageManager : MonoBehaviour
{

    [SerializeField]
    private const int BASE_DAMAGE = 100;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
            if(collider.GetComponent<Health>().HealthPoint >= 100)
                collider.GetComponent<Health>().HealthPoint -= BASE_DAMAGE;
            else if (collider.GetComponent<Health>().HealthPoint < 100)
                collider.GetComponent<Health>().HealthPoint -= collider.GetComponent<Health>().HealthPoint;
    }
}
