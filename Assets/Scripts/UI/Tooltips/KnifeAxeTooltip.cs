using UnityEngine;
using System.Collections;

/* 
 * SPAG ALERT : Ne pas corriger
 * Ce sont des features de dernières minutes volontairement faites à la vite
 * sans trop réfléchir 
*/

public class KnifeAxeTooltip : MonoBehaviour
{
    [SerializeField]
    private bool _isKeyboardScheme;
    [SerializeField]
    private GameObject _xboxChangeWeapon;
    [SerializeField]
    private GameObject _keyboardChangeWeapon;

    private WaitForSeconds _delayBeforeFadeOut;
    private Animator _animator;
    private InputManager _inputManager;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _inputManager = GameObject.Find("InputsManager").GetComponent<InputManager>();
        _delayBeforeFadeOut = new WaitForSeconds(2);

        _inputManager.OnInputSchemeChanged += OnInputSchemeChanged;
    }

    private void OnInputSchemeChanged()
    {
        GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;

        // Juste pour que ça rentre une fois...
        // Ce script est un mess lol
        if (_isKeyboardScheme)
        {
            _xboxChangeWeapon.GetComponent<SpriteRenderer>().enabled = !_xboxChangeWeapon.GetComponent<SpriteRenderer>().enabled;
            _keyboardChangeWeapon.GetComponent<SpriteRenderer>().enabled = !_keyboardChangeWeapon.GetComponent<SpriteRenderer>().enabled;
        }    
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

        if (StaticObjects.GetPlayer().GetComponent<InventoryManager>().KnifeEnabled &&
            StaticObjects.GetPlayer().GetComponent<InventoryManager>().AxeEnabled)
        {
            _xboxChangeWeapon.GetComponent<Animator>().SetBool("FadeIn", true);
            _keyboardChangeWeapon.GetComponent<Animator>().SetBool("FadeIn", true);
            yield return _delayBeforeFadeOut;
            _xboxChangeWeapon.GetComponent<Animator>().SetBool("FadeOut", true);
            _keyboardChangeWeapon.GetComponent<Animator>().SetBool("FadeOut", true);
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}