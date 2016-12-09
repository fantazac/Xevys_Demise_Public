public class AccountSettingsEntity
{
    public int AccountId { get; set; }
    public bool IsMusicPlaying { get; set; }
    public float MusicVolume { get; set; }
    public float SoundEffectsVolume { get; set; }
    public int KeyboardControlSchemeId { get; set; }
    public int GamepadControlSchemeId { get; set; }

    public AccountSettingsEntity()
    {
        IsMusicPlaying = true;
        MusicVolume = 1;
        SoundEffectsVolume = 1;
        KeyboardControlSchemeId = 1;
        GamepadControlSchemeId = 1;
    }
}
