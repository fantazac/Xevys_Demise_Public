using UnityEngine;

public class MainMenuAnimationTags : MonoBehaviour
{
    public string MainMenuButtonsGroupActiveAnimation { get; private set; }

    private void Start()
    {
        MainMenuButtonsGroupActiveAnimation = "MainMenuButtonsGroupActiveAnimation";
    }
}
