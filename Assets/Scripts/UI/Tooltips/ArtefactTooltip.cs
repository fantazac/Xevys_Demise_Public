using UnityEngine;
using System.Collections;

/* 
 * SPAG ALERT : Ne pas corriger
 * Ce sont des features de dernières minutes volontairement faites à la vite
 * sans trop réfléchir 
*/

public class ArtefactTooltip : MonoBehaviour
{
    [SerializeField]
    private int _index;

    private Animator _animator;
    private bool[] _artefactsObtained = {false, false, false};

    private void Start ()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            _artefactsObtained[0] = StaticObjects.GetPlayer().GetComponent<InventoryManager>().EarthArtefactEnabled;
            _artefactsObtained[1] = StaticObjects.GetPlayer().GetComponent<InventoryManager>().AirArtefactEnabled;
            _artefactsObtained[2] = StaticObjects.GetPlayer().GetComponent<InventoryManager>().WaterArtefactEnabled;

            if (!_artefactsObtained[_index])
            {
                _animator.SetTrigger("FadeIn");
            }
            else
            {
                GetComponent<BoxCollider2D>().enabled = false;
            }       
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            _animator.SetTrigger("FadeOut");
        }
    }
}
