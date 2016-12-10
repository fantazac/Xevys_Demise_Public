using UnityEngine;
using UnityEngine.UI;

public class BackTooltipManager : MonoBehaviour
{
    private GameObject _gamepad;
    private GameObject _keyboard;

    private AchievementInputsManager _achievementInputsManager;

    private void Start()
    {
        _keyboard = transform.GetChild(0).gameObject;
        _gamepad = transform.GetChild(1).gameObject;

        _achievementInputsManager = GameObject.Find("InputsManager").GetComponent<AchievementInputsManager>();

        _achievementInputsManager.OnInputsSchemeChanged += OnInputSchemeChanged;
    }

    private void OnInputSchemeChanged()
    {
        _gamepad.GetComponent<Image>().enabled = !_gamepad.GetComponent<Image>().enabled;
        _keyboard.GetComponent<Image>().enabled = !_keyboard.GetComponent<Image>().enabled;
    }
}
