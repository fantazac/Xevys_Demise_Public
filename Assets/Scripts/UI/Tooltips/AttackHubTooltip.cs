using UnityEngine;
using System.Collections;

/* 
 * SPAG ALERT : Ne pas corriger
 * Ce sont des features de dernières minutes volontairement faites à la vite
 * sans trop réfléchir 
*/

public class AttackHubTooltip : MonoBehaviour
{
    [SerializeField]
    private bool _isKeyboardScheme;

    private WaitForSeconds _delayBeforeFadeIn;
    private WaitForSeconds _delayBeforeFadeOut;
    private Animator _animator;
    private InputManager _inputManager;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _inputManager = GameObject.Find("InputsManager").GetComponent<InputManager>();
        _delayBeforeFadeIn = new WaitForSeconds(5);
        _delayBeforeFadeOut = new WaitForSeconds(4);
        _inputManager.OnInputSchemeChanged += OnInputSchemeChanged;

        StartCoroutine(FadeInFadeOutCoroutine());
    }

    private IEnumerator FadeInFadeOutCoroutine()
    {
        yield return _delayBeforeFadeIn;
        _animator.SetTrigger("FadeIn");
        yield return _delayBeforeFadeOut;
        _animator.SetTrigger("FadeOut");
    }

    private void OnInputSchemeChanged()
    {
        GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
