using UnityEngine;

public class MainMenuStaticObjects : MonoBehaviour
{
    private static GameObject _mainMenuPanel;
    private static GameObject _tags;
    private static MainMenuGameObjectTags _findTags;
    private static MainMenuAnimationTags _animationTags;

    private void Start()
    {
        //Je dois utiliser le string "Tags" ici car _findTags n'a pas encore de valeur
        _tags = GameObject.Find("Tags");

        _findTags = _tags.GetComponent<MainMenuGameObjectTags>();
        _animationTags = _tags.GetComponent<MainMenuAnimationTags>();

        _mainMenuPanel = GameObject.Find(_findTags.MainMenuPanel);
    }

    public static GameObject GetMainMenuPanel()
    {
        return _mainMenuPanel;
    }

    public static MainMenuGameObjectTags GetFindTags()
    {
        return _findTags;
    }

    public static MainMenuAnimationTags GetAnimationTags()
    {
        return _animationTags;
    }
}
