using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockStateController : MonoBehaviour
{

    public delegate void OnUnlockAchievementHandler(int index);
    public event OnUnlockAchievementHandler OnUnlockAchievement;

    private bool[] _achievementsArray;

    private const int ACHIEVEMENT_NUM = 12;

    private void Start()
    {
        GameObject database = DontDestroyOnLoadStaticObjects.GetDatabase();
        List <int> obtainedAchievements = database.GetComponent<AccountHasAchievementDataHandler>().GetAllObtainedAchievements(database.GetComponent<DatabaseController>().AccountId);
        _achievementsArray = new bool[ACHIEVEMENT_NUM];
        
        for (int i = 0; i < ACHIEVEMENT_NUM; i++)
        {
            _achievementsArray[i] = false;
        }
        foreach (int achievement in obtainedAchievements)
        {
            _achievementsArray[achievement] = true;
        }

        StartCoroutine(CallUnlockedAchievementContainer());
    }

    private IEnumerator CallUnlockedAchievementContainer()
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < ACHIEVEMENT_NUM; i++)
        {
            if (_achievementsArray[i])
            {
                OnUnlockAchievement(i);
            }
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
