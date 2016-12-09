using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownController : MonoBehaviour {

    private Dropdown _dropdown;
    private NicknameAddRequestToDropdown _nicknameAddRequest;
    private List<string> _nicknamesList;

    private void Start()
    {
        _dropdown = GetComponent<Dropdown>();
        _nicknamesList = new List<string>();
        _nicknameAddRequest = transform.parent.GetComponentInChildren<NicknameAddRequestToDropdown>();
        _nicknameAddRequest.OnNicknameEntered += AddNicknameToDropdown;

        PopulateDropdown();
    }

    private void PopulateDropdown()
    {
        _nicknamesList.Insert(0, "Guest");
        List<String> usernames = DontDestroyOnLoadStaticObjects.GetDatabase().GetComponent<AccountDataHandler>().GetAllUsernames();
        foreach (string username in usernames)
        {
            _nicknamesList.Add(username);
        }
        
        _dropdown.AddOptions(_nicknamesList);
        _dropdown.value = DontDestroyOnLoadStaticObjects.GetDatabase().GetComponent<DatabaseController>().AccountId;
    }

    private void AddNicknameToDropdown(string nickname)
    {
        _nicknamesList.Insert(0, nickname);

        _dropdown.ClearOptions();
        _dropdown.AddOptions(_nicknamesList);
    }
}
