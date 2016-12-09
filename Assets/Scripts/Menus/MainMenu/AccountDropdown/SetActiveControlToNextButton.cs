using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SetActiveControlToNextButton : MonoBehaviour
{
    private NicknameAddRequestToDropdown _nicknameAddRequest;
    private GameObject _nextButton;

    private void Start()
    {
        _nextButton = GameObject.Find(MainMenuStaticObjects.GetFindTags().NextBtn);
        _nicknameAddRequest = GetComponent<NicknameAddRequestToDropdown>();
        _nicknameAddRequest.ChangeCurrentControlOnNicknameEntered += SetCurrentButtonToNext;
    }

    public void SetCurrentButtonToNext()
    {
        if (!EventSystem.current.alreadySelecting)
        {
            EventSystem.current.SetSelectedGameObject(_nextButton);
        }
    }
}
