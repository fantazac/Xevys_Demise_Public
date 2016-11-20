using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PauseMenuSetActiveState : MonoBehaviour
{
    private GameObject _thisGameObject;
    private GameObject _firstButtonGameObject;

    private EventSystem _eventSystem;

    private PauseMenuAnimationManager _pauseMenuAnimationManager;
    private PauseMenuGroupButtonsFadeListener _pauseMenuGroupButtonsFadeListener;

    private void Start()
    {
        _thisGameObject = gameObject;
        _firstButtonGameObject = _thisGameObject.transform.GetChild(0).gameObject;
        _eventSystem = EventSystem.current;
        _pauseMenuAnimationManager = StaticObjects.GetPauseMenuPanel().GetComponent<PauseMenuAnimationManager>();
        _pauseMenuGroupButtonsFadeListener = GetComponent<PauseMenuGroupButtonsFadeListener>();

        _pauseMenuAnimationManager.OnPauseMenuStateChanged += EnableMainButtonsOnPauseMenuOpened;
        _pauseMenuAnimationManager.OnPauseMenuIsOutOfScreen += DisableAllGroupButtonOnMenuIsOutOfScreen;
        _pauseMenuGroupButtonsFadeListener.OnGroupButtonFadeOutEnded += DisableGroupButton;

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

    private void DisableGroupButton(bool enable)
    {
        _thisGameObject.SetActive(enable);
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

    private void DisableAllGroupButtonOnMenuIsOutOfScreen()
    {
        _thisGameObject.SetActive(false);
    }
}
