using UnityEngine;
using System.Collections;

/* 
 * SPAG ALERT : Ne pas corriger
 * Ce sont des features de dernières minutes volontairement faites à la vite
 * sans trop réfléchir 
*/

public class AttackHubTooltip : MonoBehaviour
{
    private WaitForSeconds _delayBeforeFadeIn;
    private WaitForSeconds _delayBeforeFadeOut;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _delayBeforeFadeIn = new WaitForSeconds(5);
        _delayBeforeFadeOut = new WaitForSeconds(4);
        StartCoroutine(FadeInFadeOutCoroutine());

    }

    private IEnumerator FadeInFadeOutCoroutine()
    {
        yield return _delayBeforeFadeIn;
        _animator.SetTrigger("FadeIn");
        yield return _delayBeforeFadeOut;
        _animator.SetTrigger("FadeOut");
    }
}
