﻿using UnityEngine;

public class MainMenuGameObjectTags : MonoBehaviour
{
    public string MainMenuPanel { get; private set; }
    public string MainMenuControlsOptionsButtons { get; private set; }
    public string MainMenuButtons { get; private set; }
    public string MainMenuAudioOptionsButtons { get; private set; }
    public string GamepadInputs { get; private set; }
    public string Dropdown { get; private set; }
    public string NextBtn { get; private set; }
    public string InputField { get; set; }
    public string DontDestroyOnLoadTags { get; set; }

    private void Start()
    {
        MainMenuPanel = "MainMenuPanel";
        MainMenuControlsOptionsButtons = "MainMenuControlsOptionsButtons";
        MainMenuButtons = "MainMenuButtons";
        MainMenuAudioOptionsButtons = "MainMenuAudioOptionsButtons";
        GamepadInputs = "GamepadInputs";
        Dropdown = "Dropdown";
        NextBtn = "NextBtn";
        InputField = "InputField";
        DontDestroyOnLoadTags = "DontDestroyOnLoadTags";
    }
}