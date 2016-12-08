using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenuEndInterfaceSetSelected : MonoBehaviour
{

    private GameObject _firstButtonGameObject;
    private EventSystem _eventSystem;
    private PauseMenuCurrentInterfaceAnimator _pauseMenuCurrentInterfaceAnimator;

    private void Start()
    {
        _firstButtonGameObject = transform.GetChild(0).gameObject;
        _eventSystem = EventSystem.current;
        _pauseMenuCurrentInterfaceAnimator = GetComponentInParent<PauseMenuCurrentInterfaceAnimator>();

        _pauseMenuCurrentInterfaceAnimator.OnEndShowEndInterface += SetSelectedButton;
    }

    private void SetSelectedButton(bool isFinished)
    {
        _eventSystem.SetSelectedGameObject(_firstButtonGameObject);
    }
}