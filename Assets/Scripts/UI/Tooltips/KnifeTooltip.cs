using UnityEngine;
using System.Collections;

public class KnifeTooltip : MonoBehaviour
{
    private WaitForSeconds _delayBeforeFadeOut;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _delayBeforeFadeOut = new WaitForSeconds(4);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            _animator.SetTrigger("FadeIn");
            StartCoroutine(FadeOutCoroutine());
        }    
    }

    private IEnumerator FadeOutCoroutine()
    {
        yield return _delayBeforeFadeOut;
        _animator.SetTrigger("FadeOut");
    }
}