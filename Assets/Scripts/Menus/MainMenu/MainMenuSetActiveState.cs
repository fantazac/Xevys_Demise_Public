using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuSetActiveState : MonoBehaviour
{
    private GameObject _firstButtonGameObject;
    private GameObject _OptionsButtonGameObject;
    private GameObject _ControlsButtonGameObject;
    private GameObject _AudioButtonGameObject;
    private EventSystem _eventSystem;
    private MainMenuCurrentInterfaceAnimator _mainMenuCurrentInterfaceAnimator;

    private void Start()
    {
        _firstButtonGameObject = transform.GetChild(0).gameObject;
        _OptionsButtonGameObject = transform.GetChild(1).gameObject;
        _ControlsButtonGameObject = transform.GetChild(1).gameObject;
        _AudioButtonGameObject = transform.GetChild(2).gameObject;
        _eventSystem = EventSystem.current;
        _mainMenuCurrentInterfaceAnimator = GetComponentInParent<MainMenuCurrentInterfaceAnimator>();

        switch (gameObject.name)
        {
            case "MainMenuMainButtons":
                _mainMenuCurrentInterfaceAnimator.OnMainInterfaceIsCurrent += SetSelectedButton;
                break;
            case "MainMenuOptionsButtons":
                _mainMenuCurrentInterfaceAnimator.OnOptionsInterfaceIsCurrent += SetSelectedButton;
                break;
        }
    }

    public void EnableGroupButtonOnFocus()
    {
        SetSelectedButton("");
    }

    private void SetSelectedButton(string from)
    {
        switch (from)
        {
            case "Options":
                SetOptionsSelectedButton();
                break;
            case "Controls":
                SetControlsSelectedButton();
                break;
            case "Audio":
                SetAudioSelectedButton();
                break;
            default:
                _eventSystem.SetSelectedGameObject(_firstButtonGameObject);
                break;
        }
    }

    private void SetOptionsSelectedButton()
    {
        _eventSystem.SetSelectedGameObject(_OptionsButtonGameObject);
    }

    private void SetControlsSelectedButton()
    {
        _eventSystem.SetSelectedGameObject(_ControlsButtonGameObject);
    }
    private void SetAudioSelectedButton()
    {
        _eventSystem.SetSelectedGameObject(_AudioButtonGameObject);
    }
}
