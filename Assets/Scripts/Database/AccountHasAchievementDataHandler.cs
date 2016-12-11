using System.Collections.Generic;
using UnityEngine;

public class AccountHasAchievementDataHandler : MonoBehaviour
{
    public delegate void OnAchievementUnlockedHandler(int index);
    public event OnAchievementUnlockedHandler OnAchievementUnlocked;

    private AccountHasAchievementRepository _repository;

    private void Start()
    {
        _repository = new AccountHasAchievementRepository();
        Health.OnHealthStarted += SubscribeToBossesHealth;

    }

    public void SubscribeToMainLevelEvents()
    {
        InventoryManager inventoryManager = StaticObjects.GetPlayer().GetComponent<InventoryManager>();
        inventoryManager.OnEnableIronBoots += IronBootsUnlocked;
        inventoryManager.OnEnableFeather += FeatherUnlocked;
        inventoryManager.OnEnableBubble += BubbleUnlocked;
        inventoryManager.OnEnableFireProofArmor += FireproofArmorsUnlocked;
    }
    
    //Pas la meilleure façon, mais c'est un ajout de rush final.
    private void SubscribeToBossesHealth(Health bossHealth)
    {
        MainGameObjects mainGameObjects = StaticObjects.GetMainObjects();
        if (bossHealth.tag == mainGameObjects.Behemoth)
        {
            bossHealth.OnDeath += BehemothKilled;
        }
        else if (bossHealth.tag == mainGameObjects.Phoenix)
        {
            bossHealth.OnDeath += PhoenixKilled;
        }
        else if (bossHealth.tag == mainGameObjects.NeptuneHead)
        {
            bossHealth.OnDeath += NeptuneKilled;
        }
        else if (bossHealth.tag == mainGameObjects.Vulcan)
        {
            bossHealth.OnDeath += VulcanKilled;
        }
        else if (bossHealth.tag == mainGameObjects.Xevy)
        {
            bossHealth.OnDeath += XevyKilled;
        }
    }

    public List<int> GetAllObtainedAchievements(int accountId)
    {
        return _repository.GetAllAchievementIdsFromAccount(accountId);
    }

    private void UnlockAchievement(int achievementId)
    {
        AccountHasAchievementEntity entity = new AccountHasAchievementEntity();
        entity.AccountId = GetComponent<DatabaseController>().AccountId;
        entity.AchievementId = achievementId;
        _repository.Add(entity);
        OnAchievementUnlocked(achievementId);
    }

    private void BehemothKilled()
    {
        UnlockAchievement(0);
    }

    private void PhoenixKilled()
    {
        UnlockAchievement(1);
    }

    private void NeptuneKilled()
    {
        UnlockAchievement(2);
    }

    private void VulcanKilled()
    {
        UnlockAchievement(3);
    }

    private void XevyKilled()
    {
        UnlockAchievement(4);
    }

    public void EnoughSkeltalsKilled()
    {
        UnlockAchievement(5);
    }

    public void EnoughScarabsKilled()
    {
        UnlockAchievement(6);
    }

    public void EnoughBatsKilled()
    {
        UnlockAchievement(7);
    }

    private void IronBootsUnlocked()
    {
        UnlockAchievement(8);
    }

    private void FeatherUnlocked()
    {
        UnlockAchievement(9);
    }

    private void BubbleUnlocked()
    {
        UnlockAchievement(10);
    }

    private void FireproofArmorsUnlocked()
    {
        UnlockAchievement(11);
    }
}
