using UnityEngine;
using UnityEngine.UI;

public class NicknameAddRequestToDropdown : MonoBehaviour
{
    public delegate void OnNicknameEnteredHandler(string nickname);
    public event OnNicknameEnteredHandler OnNicknameEntered;

    public delegate void ChangeCurrentControlOnNicknameEnteredHandler();
    public event ChangeCurrentControlOnNicknameEnteredHandler ChangeCurrentControlOnNicknameEntered;

    public void NicknameEntered(string nickname)
    {
        GetComponent<InputField>().text = "";

        if (nickname.Length > 0)
        {
            OnNicknameEntered(nickname);
            ChangeCurrentControlOnNicknameEntered();
        }
    }
}
