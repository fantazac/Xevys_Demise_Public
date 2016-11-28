using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EnterPortal : MonoBehaviour
{
    [SerializeField]
    private GameObject _otherPortal;

    private GameObject _player;
    private bool _playerIsOnPortal = false;

    private void Start()
    {
        _player = StaticObjects.GetPlayer();
        _player.GetComponentInChildren<InputManager>().OnEnterPortal += GoToOtherDimension;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == StaticObjects.GetUnityTags().Player)
        {
            _playerIsOnPortal = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == StaticObjects.GetUnityTags().Player)
        {
            _playerIsOnPortal = false;
        }
    }

    private void GoToOtherDimension()
    {
        if (_playerIsOnPortal)
        {
            _player.transform.position = _otherPortal.transform.position + (Vector3.forward * _player.transform.position.z);
        }
    }
}
