using UnityEngine;
using System.Collections;

public class AttackHubTooltip : MonoBehaviour {

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
