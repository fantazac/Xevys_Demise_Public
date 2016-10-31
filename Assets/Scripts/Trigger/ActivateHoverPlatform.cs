using UnityEngine;
using System.Collections;

public class ActivateHoverPlatform : MonoBehaviour
{
    [SerializeField]
    private GameObject _hoverPad;

    public void ActivateRetract()
    {
        if(_hoverPad != null)
        {
            _hoverPad.GetComponent<RetractHoverPlatform>().IsActivated = true;
        }
    }
}
