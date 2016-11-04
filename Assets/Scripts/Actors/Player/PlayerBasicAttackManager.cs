using UnityEngine;
using System.Collections;
using System.Linq;

public class PlayerBasicAttackManager : MonoBehaviour
{
    [SerializeField]
    private int _baseDamage = 100;

    /* BEN_CORRECTION
     * 
     * Non utilisé.
     */
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
        /* BEN_CORRECTION
         * 
         * Pourquoi faire "collider is PolygonCollider2D" ? C'est quoi le but ? En quoi cela affecte
         * la logique de votre jeu ?
         * 
         * Aussi, pourquoi faire la distinction entre les ennemis et les boss si ce n'est que
         * pour, de toute façon, appeler la même méthode avec les mêmes paramètres.
         */
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
