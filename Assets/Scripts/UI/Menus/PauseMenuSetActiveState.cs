using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PauseMenuSetActiveState : MonoBehaviour
{
    private GameObject _thisGameObject;
    private GameObject _firstButtonGameObject;

    private EventSystem _eventSystem;

    private PauseMenuAnimationManager _pauseMenuAnimationManager;

    private void Start()
    {
        _thisGameObject = gameObject;
        _firstButtonGameObject = _thisGameObject.transform.GetChild(0).gameObject;
        _eventSystem = EventSystem.current;
        _pauseMenuAnimationManager = StaticObjects.GetPauseMenuPanel().GetComponent<PauseMenuAnimationManager>();
        _pauseMenuAnimationManager.OnPauseMenuStateChanged += EnableMainButtonsOnPauseMenuOpened;

        if (gameObject.name != "PauseMenuMainButtons")
        {
            _thisGameObject.SetActive(false);
        }
    }

    public void EnableGroupButtonOnBackBtnClicked()
    {
        _thisGameObject.SetActive(true);
        SetSelectedButton();
    }

    public void EnableGroupButtonOnFocus()
    {
        _thisGameObject.SetActive(true);
        SetSelectedButton();
    }

    private void SetSelectedButton()
    {
        _eventSystem.SetSelectedGameObject(_firstButtonGameObject);
    }

    private void EnableMainButtonsOnPauseMenuOpened(bool isActive)
    {
        if (gameObject.name == "PauseMenuMainButtons" && isActive)
        {
            _thisGameObject.SetActive(true);
        }
    }
}
