using UnityEngine;

public class MainMenuInputs : MonoBehaviour
{
    public delegate void OnMainMenuLoadedHandler();
    public static event OnMainMenuLoadedHandler OnMainMenuLoaded;

    public delegate void OnAccountInterfaceIsCurrentHandler(string current);
    public event OnAccountInterfaceIsCurrentHandler OnAccountInterfaceIsCurrent;

    public delegate void OnMainInterfaceIsCurrentHandler(string current);
    public event OnMainInterfaceIsCurrentHandler OnMainInterfaceIsCurrent;

    public delegate void OnOptionsInterfaceIsCurrentHandler(string current);
    public event OnOptionsInterfaceIsCurrentHandler OnOptionsInterfaceIsCurrent;

    public delegate void OnControlsInterfaceIsCurrentHandler(string current);
    public event OnControlsInterfaceIsCurrentHandler OnControlsInterfaceIsCurrent;

    public delegate void OnAudioInterfaceIsCurrentHandler(string current);
    public event OnAudioInterfaceIsCurrentHandler OnAudioInterfaceIsCurrent;

    public delegate void OnGamepadBackButtonPressedHandler();
    public event OnGamepadBackButtonPressedHandler OnGamepadBackButtonPressed;

    public delegate void OnStartButtonPressedHandler();
    public event OnStartButtonPressedHandler OnStartButtonPressed;

    public delegate void OnAchievementsButtonPressedHandler();
    public event OnAchievementsButtonPressedHandler OnAchievementsButtonPressed;

    private MainMenuGamepadInputs _mainMenuGamepadInputs;

    private void Start()
    {
        _mainMenuGamepadInputs = GameObject.Find(MainMenuStaticObjects.GetFindTags().GamepadInputs).GetComponent<MainMenuGamepadInputs>();
        _mainMenuGamepadInputs.OnBackButtonPressedInMenu += GamepadBackBtnPressed;
        OnMainMenuLoaded();
    }

    private void GamepadBackBtnPressed()
    {
        OnGamepadBackButtonPressed();
    }

    public void AcheivementsBtnOnClick()
    {
        OnAchievementsButtonPressed();
    }

    public void StartBtnOnClick()
    {
        OnStartButtonPressed();
    }

    public void NextBtnOnClick()
    {
        OnMainInterfaceIsCurrent("Main");
    }

    public void OptionsBtnOnClick()
    {
        OnOptionsInterfaceIsCurrent("Options");
    }

    public void QuitBtnOnClick()
    {
        Application.Quit();
    }

    public void MainBackBtnOnClick()
    {
        OnAccountInterfaceIsCurrent("Account");
    }

    public void ControlsBtnOnClick()
    {
        OnControlsInterfaceIsCurrent("Controls");
    }

    public void AudioBtnOnClick()
    {
        OnAudioInterfaceIsCurrent("Audio");
    }

    public void OptionsBackBtnOnClick()
    {
        OnMainInterfaceIsCurrent("Main");
    }

    public void ControlsBackBtnOnClick()
    {
        OnOptionsInterfaceIsCurrent("Options");
    }

    public void AudioBackBtnOnClick()
    {
        OnOptionsInterfaceIsCurrent("Options");
    }
}
