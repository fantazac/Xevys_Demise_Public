using UnityEngine;
using System.Collections;

public class PlayDeathAnimation : MonoBehaviour
{
    public delegate void OnDyingAnimationFinishedHandler();
    public event OnDyingAnimationFinishedHandler OnDyingAnimationFinished;

    private Animator _animator;

    private void Start()
    {   
        GetComponent<Health>().OnDeath += OnDeath;

        _animator = GetComponentInChildren<Animator>();
    }

    private void OnDeath()
    {
        _animator.SetBool(StaticObjects.GetAnimationTags().IsDying, true);

        StartCoroutine(FinishAnimation());
    }

    private IEnumerator FinishAnimation()
    {
        yield return null;

        if (OnDyingAnimationFinished != null)
        {
            OnDyingAnimationFinished();
        }
    }
}
