using UnityEngine;
using System.Collections;
using System.Linq;

public class PlayerBasicAttackManager : MonoBehaviour
{
    [SerializeField]
    private int _baseDamage = 100;

    private string[] _enemiesTags;

    private void Start()
    {
        _enemiesTags = new string[] { "Scarab", "Bat", "Skeltal", "Behemoth", "Phoenix", "Spike" };
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (_enemiesTags.Contains(collider.gameObject.tag))
        {
            collider.GetComponent<Health>().Hit(_baseDamage);
        }
    }
}
