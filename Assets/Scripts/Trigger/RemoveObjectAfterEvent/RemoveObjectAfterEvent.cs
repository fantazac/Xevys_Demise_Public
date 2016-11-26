using UnityEngine;
using System.Collections;

public class RemoveObjectAfterEvent : MonoBehaviour
{
    [SerializeField]
    protected bool _destroyParent = false;

    protected virtual void RemoveObject()
    {
        Destroy(gameObject);
        if (_destroyParent)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
