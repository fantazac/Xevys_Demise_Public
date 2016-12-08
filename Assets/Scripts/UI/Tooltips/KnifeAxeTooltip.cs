using UnityEngine;
using System.Collections;

/* 
 * SPAG ALERT : Ne pas corriger
 * Ce sont des features de dernières minutes volontairement faites à la vite
 * sans trop réfléchir 
*/

public class KnifeAxeTooltip : MonoBehaviour
{
    private WaitForSeconds _delayBeforeFadeOut;
    private Animator _animator;
    private Animator _changeWeaponAnimator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _delayBeforeFadeOut = new WaitForSeconds(2);
        _changeWeaponAnimator = transform.parent.FindChild("ChangeWeaponTooltip").GetComponent<Animator>();
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
            _changeWeaponAnimator.SetBool("FadeIn", true);
            yield return _delayBeforeFadeOut;
            _changeWeaponAnimator.SetBool("FadeOut", true);
        }
    }
}