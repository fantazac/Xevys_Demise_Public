using UnityEngine;

public class AchievementDataHandler : MonoBehaviour
{
    AchievementEntity _entity;
    AchievementRepository _repository;

    private void Start()
    {
        _repository = new AchievementRepository();
    }

    public void CreateAllAchievements()
    {
        CreateAchievement("Eye of the Behemoth", "Kill Behemoth in World 1");
        CreateAchievement("The Phoenix", "Kill Phoenix in World 2");
        CreateAchievement("Smoke on the water", "Kill Neptune in World 3");
        CreateAchievement("Highway to Hell", "Kill Vulcan in World 4");
        CreateAchievement("Bimon and the Beast", "Finally kill Xevy");
        CreateAchievement("Skeltals in the closet", "Kill 15 skeltals");
        CreateAchievement("Bat country", "Kill 30 bats");
        CreateAchievement("Kill 'Em All", "Kill 15 scarabs");
        CreateAchievement("In too deep", "Get the boots");
        CreateAchievement("I believe I can fly", "Get the feather");
        CreateAchievement("Bubble Pop!", "Get the bubble");
        CreateAchievement("Through the fire and flames", "Get the fire armor");
    }


    private void CreateAchievement(string name, string description)
    {
        AchievementEntity entity = new AchievementEntity();
        entity.Name = name;
        entity.Description = description;
        _repository.Add(entity);
    }
}
