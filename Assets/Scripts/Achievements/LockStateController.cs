using System.Collections;
using UnityEngine;

public class LockStateController : MonoBehaviour
{

    public delegate void OnUnlockAchievementHandler(int index);
    public event OnUnlockAchievementHandler OnUnlockAchievement;

    private bool[] _achievementsArray;

    private const int ACHIEVEMENT_NUM = 12;

    private void Start()
    {
        _achievementsArray = new bool[ACHIEVEMENT_NUM];

        ///////////////////Ici build le tableau à partir de la database.
        for (int i = 0; i < ACHIEVEMENT_NUM; i++)
        {
            _achievementsArray[i] = true;
        }
        //////////////////

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
