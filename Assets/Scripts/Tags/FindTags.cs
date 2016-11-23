using UnityEngine;
using System.Collections;

public class FindTags : MonoBehaviour
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
    public string Database { get; private set; }
    public string PauseMenuControlsOptionsButtons { get; private set; }

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
        Database = "Database";
        PauseMenuControlsOptionsButtons = "PauseMenuControlsOptionsButtons";
    }
}
