using UnityEngine;
using System.Collections;
using System.Security.Cryptography.X509Certificates;

public class MainGameObjects : MonoBehaviour
{
    public string NeptuneHead { get; private set; }
    public string Vulcan { get; private set; }
    public string CharacterBasicAttackBox { get; private set; }
    public string CharacterCroutchedHitbox { get; private set; }
    public string CharacterFloatingHitbox { get; private set; }
    public string Boundary { get; private set; }
    public string MainCamera { get; private set; }
    public string ResumeBtn { get; private set; }
    public string Character { get; private set; }
    public string PanelUI { get; private set; }
    public string PauseMenuPanel { get; private set; }
    public string HealthBar { get; private set; }
    public string ItemCanvas { get; private set; }
    public string PauseMenuControlsOptionsButtons { get; private set; }
    public string PauseMenuButtons { get; private set; }
    public string PauseMenuAudioOptionsButtons { get; private set; }
    public string CharacterTouchesGround { get; private set; }
    public string CharacterTouchesRoof { get; private set; }
    public string RespawnEnemy { get; private set; }
    public string Cinematic { get; private set; }
    public string Pause { get; private set; }
    public string RoomsStateCollector { get; private set; }
    public string WaterCamera { get; private set; }
    public string Behemoth { get; set; }
    public string Phoenix { get; set; }
    public string Xevy { get; set; }

    private void Start()
    {
        NeptuneHead = "Neptune Head";
        Vulcan = "Vulcan";
        CharacterBasicAttackBox = "CharacterBasicAttackBox";
        CharacterCroutchedHitbox = "CharacterCroutchedHitbox";
        CharacterFloatingHitbox = "CharacterFloatingHitbox";
        Boundary = "Boundary";
        MainCamera = "Main Camera";
        ResumeBtn = "ResumeBtn";
        Character = "Character";
        PanelUI = "PanelUI";
        PauseMenuPanel = "PauseMenuPanel";
        HealthBar = "HealthBar";
        ItemCanvas = "ItemCanvas";
        PauseMenuControlsOptionsButtons = "PauseMenuControlsOptionsButtons";
        PauseMenuButtons = "PauseMenuButtons";
        PauseMenuAudioOptionsButtons = "PauseMenuAudioOptionsButtons";
        CharacterTouchesGround = "CharacterTouchesGround";
        CharacterTouchesRoof = "CharacterTouchesRoof";
        RespawnEnemy = "RespawnEnemy";
        Cinematic = "Cinematic";
        Pause = "Pause";
        RoomsStateCollector = "RoomsStateCollector";
        WaterCamera = "Water Camera";
        Behemoth = "Behemoth";
        Phoenix = "Phoenix";
        Xevy = "Xevy";
    }
}
