using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class DatabaseController : MonoBehaviour
{
    [SerializeField]
    private bool _loadDB;

    private AccountDataHandler _accountDataHandler;
    private AccountStatsDataHandler _accountStatsDataHandler;
    private AccountSettingsDataHandler _accountSettingsDataHandler;
    private AccountRoomStateDataHandler _accountRoomStateDataHandler;
    public int AccountId { get; set; }
    public bool IsGuest { get; set; }

    private void Start()
    {
        if(!_loadDB || !File.Exists(Path.Combine(Application.persistentDataPath, "Database.db")))
        {
            File.Copy(Path.Combine(Application.streamingAssetsPath, "Database.db"), Path.Combine(Application.persistentDataPath, "Database.db"), true);
            GetComponent<AchievementDataHandler>().CreateAllAchievements();
            GetComponent<FunFactDataHandler>().CreateAllFunFacts();
        }
        _accountDataHandler = GetComponent<AccountDataHandler>();
        _accountStatsDataHandler = GetComponent<AccountStatsDataHandler>();
        _accountSettingsDataHandler = GetComponent<AccountSettingsDataHandler>();
        _accountRoomStateDataHandler = GetComponent<AccountRoomStateDataHandler>();

        MainMenuInputs.OnMainMenuLoaded += ConnectToNicknameEnteredEvent;
    }

    private void ConnectToNicknameEnteredEvent()
    {
        GameObject.Find(MainMenuStaticObjects.GetFindTags().InputField).GetComponent<NicknameAddRequestToDropdown>().OnNicknameEntered += CreateNewAccountEntries;
    }

    private void CreateNewAccountEntries(string username)
    {
        _accountDataHandler.CreateNewEntry(username);
        _accountDataHandler.ChangeEntity(username);
        _accountStatsDataHandler.CreateNewEntry(AccountId);
        _accountSettingsDataHandler.CreateNewEntry(AccountId);
        _accountRoomStateDataHandler.CreateNewEntry(AccountId);
    }

    public void ChangeAccount()
    {
        Dropdown dropdown = GameObject.Find(MainMenuStaticObjects.GetFindTags().Dropdown).GetComponent<Dropdown>();
        _accountDataHandler.ChangeEntity(dropdown.options[dropdown.value].text);
        _accountStatsDataHandler.ChangeEntity(AccountId);
        _accountSettingsDataHandler.ChangeEntity(AccountId);
    }

    public void ChangeAccountRoomState()
    {
        _accountRoomStateDataHandler.ChangeEntity(AccountId);
    }

    public void SaveAccount()
    {
        if (!IsGuest)
        {
            _accountStatsDataHandler.UpdateRepository();
            _accountSettingsDataHandler.UpdateRepository();
            _accountRoomStateDataHandler.UpdateRepository();
        }
    }
}
