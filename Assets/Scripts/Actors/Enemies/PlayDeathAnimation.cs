using UnityEngine;
using System.Collections;

public class PlayDeathAnimation : MonoBehaviour
{

    private Health _health;
    private Animator _animator;

    private void Start()
    {
        _health = GetComponent<Health>();
        _health.OnDeath += OnDeath;

        _animator = GetComponent<Animator>();
    }

    private void OnDeath()
    {
        _animator.SetBool("IsDying", true);
    }

}
