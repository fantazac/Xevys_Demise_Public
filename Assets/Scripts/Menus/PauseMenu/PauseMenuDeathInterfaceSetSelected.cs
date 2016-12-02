using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PauseMenuDeathInterfaceSetSelected : MonoBehaviour
{

    private GameObject _firstButtonGameObject;
    private EventSystem _eventSystem;
    private PauseMenuCurrentInterfaceAnimator _pauseMenuCurrentInterfaceAnimator;

    private void Start()
    {
        _firstButtonGameObject = transform.GetChild(0).gameObject;
        _eventSystem = EventSystem.current;
        _pauseMenuCurrentInterfaceAnimator = GetComponentInParent<PauseMenuCurrentInterfaceAnimator>();

        _pauseMenuCurrentInterfaceAnimator.OnPlayerDeathShowDeathInterface += SetSelectedButton;
    }

    private void SetSelectedButton(bool isDead)
    {
        _eventSystem.SetSelectedGameObject(_firstButtonGameObject);
    }
}
