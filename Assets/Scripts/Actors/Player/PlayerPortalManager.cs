using UnityEngine;
using System.Collections;

public class PlayerPortalManager : MonoBehaviour
{
    protected InputManager _inputManager;

    private bool _isInPortal = false;
    private GameObject _portal;

    public bool IsInPortal { get { return _isInPortal; } set { _isInPortal = value; } }
    public GameObject Portal { get { return _portal; } set { _portal = value; } }

    private void Start()
    {
        _inputManager = GetComponent<InputManager>();
        _inputManager.OnEnterPortal += OnEnterPortal;
    }

    private void OnEnterPortal()
    {
        if(_portal != null)
        {
            transform.position = new Vector3(_portal.transform.position.x, _portal.transform.position.y, transform.position.z);
        }
    }
}
