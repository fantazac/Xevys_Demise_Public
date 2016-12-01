using UnityEngine;
using System.Collections;

public class MainMenuFindTags : MonoBehaviour
{
    public string MainMenuPanel { get; private set; }
    public string Database { get; private set; }
    public string MainMenuControlsOptionsButtons { get; private set; }
    public string MainMenuButtons { get; private set; }
    public string MainMenuAudioOptionsButtons { get; private set; }
    public string GamepadInputs { get; private set; }

    private void Start()
    {
        MainMenuPanel = "MainMenuPanel";
        Database = "Database";
        MainMenuControlsOptionsButtons = "MainMenuControlsOptionsButtons";
        MainMenuButtons = "MainMenuButtons";
        MainMenuAudioOptionsButtons = "MainMenuAudioOptionsButtons";
        GamepadInputs = "GamepadInputs";
    }
}