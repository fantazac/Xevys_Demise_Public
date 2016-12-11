using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BackTooltipManager : MonoBehaviour
{
    private GameObject _gamepad;
    private GameObject _keyboard;
    private bool _canChange;

    private AchievementInputsManager _achievementInputsManager;

    private void Start()
    {
        _keyboard = transform.GetChild(0).gameObject;
        _gamepad = transform.GetChild(1).gameObject;

        _achievementInputsManager = GameObject.Find("InputsManager").GetComponent<AchievementInputsManager>();

        _achievementInputsManager.OnInputsSchemeChanged += OnInputSchemeChanged;

        _canChange = true;
    }

    private void OnInputSchemeChanged()
    {
        if (_canChange)
        {
            _gamepad.GetComponent<Image>().enabled = !_gamepad.GetComponent<Image>().enabled;
            _keyboard.GetComponent<Image>().enabled = !_keyboard.GetComponent<Image>().enabled;
            _canChange = false;
            StartCoroutine(Countdown());
        }

    }

    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(0.3f);

        _canChange = true;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
