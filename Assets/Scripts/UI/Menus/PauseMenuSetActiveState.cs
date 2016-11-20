using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PauseMenuSetActiveState : MonoBehaviour
{
    private GameObject _firstButtonGameObject;

    private EventSystem _eventSystem;

    private void Start()
    {
        _firstButtonGameObject = transform.GetChild(0).gameObject;
        _eventSystem = EventSystem.current;
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
