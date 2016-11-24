using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenuSetActiveState : MonoBehaviour
{
    private GameObject _firstButtonGameObject;
    private EventSystem _eventSystem;
    private PauseMenuCurrentInterfaceAnimator _pauseMenuCurrentInterfaceAnimator;
    private PauseMenuInputs _pauseMenuInputs;

    private void Start()
    {
        _firstButtonGameObject = transform.GetChild(0).gameObject;
        _eventSystem = EventSystem.current;
        _pauseMenuCurrentInterfaceAnimator = GetComponentInParent<PauseMenuCurrentInterfaceAnimator>();
        _pauseMenuInputs = StaticObjects.GetPauseMenuPanel().GetComponentInChildren<PauseMenuInputs>();

        switch (gameObject.name)
        {
            case "PauseMenuMainButtons":
                _pauseMenuCurrentInterfaceAnimator.OnMainInterfaceIsCurrent += EnableGroupButtonOnFocus;
                _pauseMenuInputs.OnMainInterfaceOpended += EnableGroupButtonOnFocus;
                break;
            case "PauseMenuOptionsButtons":
                _pauseMenuCurrentInterfaceAnimator.OnOptionsInterfaceIsCurrent += EnableGroupButtonOnFocus;
                break;
        }
    }

    public void EnableGroupButtonOnBackBtnClicked()
    {
        SetSelectedButton();
    }

    public void EnableGroupButtonOnFocus()
    {
        SetSelectedButton();
    }

    private void SetSelectedButton()
    {
        _eventSystem.SetSelectedGameObject(_firstButtonGameObject);
    }
}
