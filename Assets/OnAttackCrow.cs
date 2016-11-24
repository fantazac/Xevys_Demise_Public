using UnityEngine;
using System.Collections;

public class OnAttackCrow : MonoBehaviour
{
    private Animator _animator;
    private WaitForSeconds _hitAnimationDelay;

    private void Start ()
	{
        _hitAnimationDelay = new WaitForSeconds(0.2f);
        _animator = GetComponent<Animator>();
	}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "BasicAttackHitbox" || collider.tag == "Knife" || collider.tag == "Axe")
        {
            _animator.SetBool("IsHit", true);
            StartCoroutine(WaitForAnimationCoroutine());
        }
    }

    private IEnumerator WaitForAnimationCoroutine()
    {
        yield return _hitAnimationDelay;
        _animator.SetBool("IsHit", false);
    }

}
