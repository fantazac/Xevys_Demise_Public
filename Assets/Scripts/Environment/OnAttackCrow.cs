using UnityEngine;
using System.Collections;

public class OnAttackCrow : MonoBehaviour
{
    private Animator _animator;
    private WaitForSeconds _hitAnimationDelay;
    private AudioSource _audioSource;

    private void Start ()
	{
        _hitAnimationDelay = new WaitForSeconds(0.2f);
        _animator = GetComponent<Animator>();
	    _audioSource = GetComponent<AudioSource>();
	}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "BasicAttackHitbox" || collider.tag == "Knife" || collider.tag == "AxeBlade")
        {
            _animator.SetBool("IsHit", true);
            _audioSource.Play();
            StartCoroutine(WaitForAnimationCoroutine());
        }
    }

    private IEnumerator WaitForAnimationCoroutine()
    {
        yield return _hitAnimationDelay;
        _animator.SetBool("IsHit", false);
    }

}
