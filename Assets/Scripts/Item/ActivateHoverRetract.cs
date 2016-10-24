using UnityEngine;
using System.Collections;

/* BEN_REVIEW
 * 
 * HoverRetract ?
 */
public class ActivateHoverRetract : MonoBehaviour
{

    [SerializeField]
    private GameObject _hoverPad;

    public void ActivateRetract()
    {
        if(_hoverPad != null)
        {
            _hoverPad.GetComponent<RetractHoverPad>().Retract = true;
        }
    }
}
