using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float _health = 1000f;
    [SerializeField]
    private int _hitSoundIndex = -1;
    private AudioSourcePlayer _soundPlayer;

    public float HealthPoint { get { return _health; } set { _health = value; } }

    public delegate void HealthChangedHandler(int hitPoint);
    public event HealthChangedHandler OnHealthChanged;

    private void Start()
    {
        _soundPlayer = GetComponent<AudioSourcePlayer>();
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
            _soundPlayer.Play(_hitSoundIndex);
        }
    }
}
