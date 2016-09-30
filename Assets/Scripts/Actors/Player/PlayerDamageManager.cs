using UnityEngine;
using System.Collections;

public class PlayerDamageManager : MonoBehaviour
{
    public delegate void OnDamageHandler(int damage);
    public event OnDamageHandler OnDamage;
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
            OnDamage(100);
    }
}
