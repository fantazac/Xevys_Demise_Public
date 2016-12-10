using UnityEngine;
using System.Collections;

/* 
 * SPAG ALERT : Ne pas corriger
 * Ce sont des features de dernières minutes volontairement faites à la vite
 * sans trop réfléchir 
*/

public class PortalTooltip : MonoBehaviour
{
    private Animator _animator;
    private InventoryManager _inventoryManager;
    private bool _hasArtefact = false;

    private void Start ()
    {
        _animator = GetComponent<Animator>();
        _inventoryManager = StaticObjects.GetPlayer().GetComponent<InventoryManager>();
        _inventoryManager.OnEnableFireArtefact += OnArtefactPickedUp;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player" && _hasArtefact)
        {
            _animator.SetTrigger("FadeIn");
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player" && _hasArtefact)
        {
            _animator.SetTrigger("FadeOut");
        }
    }

    private void OnArtefactPickedUp()
    {
        _hasArtefact = true;
        _animator.SetTrigger("FadeIn");
    }	
}
