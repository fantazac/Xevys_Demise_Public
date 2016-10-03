using UnityEngine;
using System.Collections;

public class PlayerDamageManager : MonoBehaviour
{

    [SerializeField]
    private int _baseDamage = 100;

    private int _damageTimer = 200;

    private void OnTriggerStay2D(Collider2D collider)
    {
        _damageTimer--;
        if (collider.gameObject.tag == "Player" && _damageTimer <= 0)
        {
            if (collider.GetComponent<Health>().HealthPoint >= 100)
            {
                collider.GetComponent<Health>().HealthPoint -= _baseDamage;
            }
            else if (collider.GetComponent<Health>().HealthPoint < 100)
            {
                collider.GetComponent<Health>().HealthPoint -= collider.GetComponent<Health>().HealthPoint;
            }

            _damageTimer = 200;
        }
    }
}
