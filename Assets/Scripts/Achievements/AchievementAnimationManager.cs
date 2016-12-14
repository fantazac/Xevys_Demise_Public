using System.Collections;
using UnityEngine;

public class AchievementAnimationManager : MonoBehaviour
{
    public delegate void OnUnlockAchievementInGameHandler(int index);
    public event OnUnlockAchievementInGameHandler OnUnlockAchievementInGame;

    private AccountHasAchievementDataHandler _accountHasAchievement;

    private void Start()
    {
        _accountHasAchievement = DontDestroyOnLoadStaticObjects.GetDatabase().GetComponent<AccountHasAchievementDataHandler>();
        _accountHasAchievement.OnAchievementUnlocked += TriggerAchievement;
    }

    private void TriggerAchievement(int index)
    {
        transform.GetChild(index).gameObject.SetActive(true);
        StartCoroutine(CallUnlockedAchievementContainer(index));
    }

    private IEnumerator CallUnlockedAchievementContainer(int index)
    {
        yield return new WaitForSeconds(1f);

        OnUnlockAchievementInGame(index);
        StartCoroutine(FadeOutContainer(index));
    }

    private IEnumerator FadeOutContainer(int index)
    {
        yield return new WaitForSeconds(3f);

        transform.GetChild(index).GetComponent<Animator>().SetTrigger(StaticObjects.GetAnimationTags().FadeOut);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
        _accountHasAchievement.ClearOnAchievementUnlocked();
    }
}
