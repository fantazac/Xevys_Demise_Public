using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EnterPortal : MonoBehaviour
{
    [SerializeField]
    private bool _voidPortal;

    private bool _playerIsOnPortal = false;

    private void Start()
    {
        if (_voidPortal)
        {
            StaticObjects.GetPlayer().GetComponentInChildren<InputManager>().OnEnterPortal += EnterVoid;
        }
        else
        {
            StaticObjects.GetPlayer().GetComponentInChildren<InputManager>().OnEnterPortal += EnterMainLevel;
        }
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

    private void EnterVoid()
    {
        if (_playerIsOnPortal)
        {
            SceneManager.LoadScene("Void", LoadSceneMode.Single);
        }
    }

    private void EnterMainLevel()
    {
        if (_playerIsOnPortal)
        {
            SceneManager.LoadScene("MainLevel", LoadSceneMode.Single);
        }
    }
}
