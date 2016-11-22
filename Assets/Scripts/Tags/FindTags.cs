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

    private void Start()
    {
        GameObject.Find("");

        NeptuneHead = "Neptune Head";
        Vulcan = "Vulcan";
        CharacterBasicAttackBox = "CharacterBasicAttackBox";
        CharacterCroutchedHitbox = "CharacterCroutchedHitbox";
        CharacterFloatingHitbox = "CharacterFloatingHitbox";
        Boundary = "Boundary";
        MainCamera = "MainCamera";
        ResumeBtn = "ResumeBtn";
    }

}
