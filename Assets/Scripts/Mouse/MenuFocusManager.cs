using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuFocusManager : MonoBehaviour
{
    private PauseMenuAnimationManager _pauseMenuAnimationManager;

    private GameObject _lastSelectedGameObject;

    private void Start()
    {
        _pauseMenuAnimationManager = StaticObjects.GetPauseMenuPanel().GetComponent<PauseMenuAnimationManager>();
        _pauseMenuAnimationManager.OnPauseMenuStateChanged += CheckLostOfFocusWhenInPauseMenu;

        _lastSelectedGameObject = EventSystem.current.firstSelectedGameObject;
    }

    private void CheckLostOfFocusWhenInPauseMenu(bool isActive, bool isDead)
    {
        if (isActive || isDead)
        {
            StartCoroutine(CheckLostOfFocus());
        }
        else
        {
            StopAllCoroutines();
        }
    }

    private IEnumerator CheckLostOfFocus()
    {
        while (true)
        {
            if (EventSystem.current.currentSelectedGameObject != null)
            {
                _lastSelectedGameObject = EventSystem.current.currentSelectedGameObject;
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(_lastSelectedGameObject);
            }

            yield return null;
        }
    }
}
