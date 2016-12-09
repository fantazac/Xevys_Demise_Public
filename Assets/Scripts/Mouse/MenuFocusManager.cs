using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuFocusManager : MonoBehaviour
{
    private PauseMenuAnimationManager _pauseMenuAnimationManager;

    private GameObject _lastSelectedGameObject;

    private bool _checking;

    private void Start()
    {
        _pauseMenuAnimationManager = StaticObjects.GetPauseMenuPanel().GetComponent<PauseMenuAnimationManager>();
        _pauseMenuAnimationManager.OnPauseMenuStateChanged += CheckLostOfFocusWhenInPauseMenu;

        _lastSelectedGameObject = EventSystem.current.firstSelectedGameObject;

        _checking = false;
    }

    private void CheckLostOfFocusWhenInPauseMenu(bool isActive, bool isDead)
    {
        if ((isActive || isDead) && !_checking)
        {
            _checking = true;
            StartCoroutine(CheckLostOfFocus());
        }
        else
        {
            _checking = false;
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

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
