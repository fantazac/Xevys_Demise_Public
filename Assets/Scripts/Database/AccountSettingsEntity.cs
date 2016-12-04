using UnityEngine;

public class AccountSettingsEntity : MonoBehaviour
{
    public int AccountId { get; set; }
    public bool IsMusicPlaying { get; set; }
    public float MusicVolume { get; set; }
    public float SoundEffectsVolume { get; set; }
    public int KeyboardControlSchemeId { get; set; }
    public int GamepadControlSchemeId { get; set; }
}
