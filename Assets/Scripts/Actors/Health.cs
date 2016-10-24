using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float _health = 1000f;

    /* BEN_REVIEW
     * 
     * Ce n'est pas la responsabilité de ce composant de faire jouer du son lorsque
     * l'on reçois un coup. Faites un autre composant pour cela qui s'abonne sur le "OnHealthChanged".
     */
    [SerializeField]
    private int _hitSoundIndex = -1;
    private AudioSource[] _audioSources;

    public float HealthPoint { get { return _health; } set { _health = value; } }

    public delegate void HealthChangedHandler(int hitPoint);
    public event HealthChangedHandler OnHealthChanged;

    /* BEN_REVIEW
     * 
     * Toutes les méthodes évènementielles Unity doivent être private.
     * 
     * En fait, toute méthode doit avoir son modificateur de visibilité (public, private, protected, internal, etc...).
     */
    void Start()
    {
        _audioSources = GetComponents<AudioSource>();
    }

    public void Heal(int healPoints)
    {
        HealthPoint += healPoints;
    }

    public void Hit(int hitPoints)
    {
        if (OnHealthChanged != null)
        {
            OnHealthChanged(hitPoints);
        }
        HealthPoint -= hitPoints;

        if(_hitSoundIndex > -1)
        {
            _audioSources[_hitSoundIndex].Play();
        }

        /* BEN_REVIEW
         * 
         * "if" vide à supprimer (ou créer l'évènement "OnDeath") qui va avec cette condition.
         */
        if(HealthPoint <= 0)
        {

        }
    }
}
