using UnityEngine;
using System.Collections;

/* 
 * SPAG ALERT : Ne pas corriger
 * Ce sont des features de dernières minutes volontairement faites à la vite
 * sans trop réfléchir 
*/

public class GenericOnTriggerEnterTooltip : MonoBehaviour
{
    [SerializeField]
    private bool _isKeyboardScheme;

    private WaitForSeconds _delayBeforeFadeOut;
    private Animator _animator;
    private InputManager _inputManager;

    private void Start ()
    {
        _animator = GetComponent<Animator>();
        _inputManager = GameObject.Find("InputsManager").GetComponent<InputManager>();
        _delayBeforeFadeOut = new WaitForSeconds(2);
        _inputManager.OnInputSchemeChanged += OnInputSchemeChanged;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            _animator.SetTrigger("FadeIn");
            GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(FadeOutCoroutine());
        }
    }

    private IEnumerator FadeOutCoroutine()
    {
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
