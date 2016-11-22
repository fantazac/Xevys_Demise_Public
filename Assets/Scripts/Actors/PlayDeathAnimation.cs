using UnityEngine;
using System.Collections;

public class PlayDeathAnimation : MonoBehaviour
{
    public delegate void OnDyingAnimationFinishedHandler();
    public event OnDyingAnimationFinishedHandler OnDyingAnimationFinished;

    private Health _health;
    private Animator _animator;

    private void Start()
    {
        _health = GetComponent<Health>();
        _health.OnDeath += OnDeath;

        _animator = GetComponentInChildren<Animator>();
    }

    private void OnDeath()
    {
        _animator.SetBool(StaticObjects.GetAnimationTags().IsDying, true);
        if (OnDyingAnimationFinished != null)
        {
            OnDyingAnimationFinished();
        }
    }
}
