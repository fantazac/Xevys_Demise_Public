using System;
using UnityEngine;

public class AchievementKeyboardInputs : MonoBehaviour
{

    public delegate void KeyboardOnEscapeButtonPressedHandler();
    public event KeyboardOnEscapeButtonPressedHandler KeyboardOnEscapeButtonPressed;

    private void Update()
    {
        UpdateStartButton();
    }

    private void UpdateStartButton()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            KeyboardOnEscapeButtonPressed();
        }
    }
}
