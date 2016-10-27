using UnityEngine;
using System.Collections;
using System.Linq;

public class PlayerBasicAttackManager : MonoBehaviour
{
    [SerializeField]
    private int _baseDamage = 100;

    private Animator _anim;

    private string[] _enemiesTags;
    private string[] _bossesTags;

    private void Start()
    {
        _enemiesTags = new string[] { "Scarab", "Bat", "Skeltal" };
        _bossesTags = new string[] { "Behemoth", "Phoenix", "Neptune" };
        _anim = GameObject.Find("CharacterSprite").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (_bossesTags.Contains(collider.gameObject.tag) && collider is PolygonCollider2D)
        {
            collider.GetComponent<Health>().Hit(_baseDamage);
        }
        else if (_enemiesTags.Contains(collider.gameObject.tag))
        {
            collider.GetComponent<Health>().Hit(_baseDamage);
        }
    }
}
