using UnityEngine;

public class AchievementBackToMainMenu : MonoBehaviour
{
    private AchievementInputsManager _achievementInputsManager;
    private ShowMainMenu _showMainMenu;
    
    private void Start()
    {
        _achievementInputsManager = GameObject.Find("InputsManager").GetComponent<AchievementInputsManager>();
        _achievementInputsManager.OnBackButtonPressed += ToMainMenu;

        _showMainMenu = GetComponent<ShowMainMenu>();
    }

    private void ToMainMenu()
    {
        _showMainMenu.ShowMenu();
    }
}
