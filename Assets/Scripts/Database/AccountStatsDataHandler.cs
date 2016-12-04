using UnityEngine;

public class AccountStatsDataHandler : MonoBehaviour
{
    public delegate void OnInventoryReloadedHandler(bool knifeEnabled, bool axeEnabled, bool featherEnabled, bool bootsEnabled, bool bubbleEnabled, bool armorEnabled, bool earthArtefactEnabled, bool airArtefactEnabled, bool waterArtefactEnabled, bool fireArtefactEnabled);
    public static event OnInventoryReloadedHandler OnInventoryReloaded;
    public delegate void OnAmmoReloadedHandler(int knifeAmmo, int axeAmmo);
    public static event OnAmmoReloadedHandler OnAmmoReloaded;
    public delegate void OnHealthReloadedHandler(int health);
    public static event OnHealthReloadedHandler OnHealthReloaded;

    private AccountStatsEntity _entity;
    private AccountStatsRepository _repository;

    private void Start()
    {
        _repository = new AccountStatsRepository();
        _entity = _repository.Get(StaticObjects.AccountId);
        InventoryManager inventory = StaticObjects.GetPlayer().GetComponent<InventoryManager>();
        inventory.OnEnableKnife += EnableKnife;
        inventory.OnEnableAxe += EnableAxe;
        inventory.OnEnableIronBoots += EnableIronBoots;
        inventory.OnEnableFeather += EnableFeather;
        inventory.OnEnableBubble += EnableBubble;
        inventory.OnEnableFireProofArmor += EnableFireProofArmor;
        inventory.OnEnableEarthArtefact += EnableEarthArtefact;
        inventory.OnEnableAirArtefact += EnableAirArtefact;
        inventory.OnEnableWaterArtefact += EnableWaterArtefact;
        inventory.OnEnableFireArtefact += EnableFireArtefact;
        DestroyEnemyOnDeath.OnEnemyDeath += EnemyKilled;
    }

    public void UpdateRepository()
    {
        _repository.UpdateEntity(_entity);
    }

    private void EnemyKilled(string tag)
    {
        if (tag == StaticObjects.GetUnityTags().Scarab)
        {
            _entity.NbScarabsKilled++;
        }
        else if (tag == StaticObjects.GetUnityTags().Bat)
        {
            _entity.NbBatsKilled++;
        }
        else if (tag == StaticObjects.GetUnityTags().Skeltal)
        {
            _entity.NbSkeltalsKilled++;
        }
    }

    private void EnableKnife()
    {
        _entity.KnifeEnabled = true;
    }

    private void EnableAxe()
    {
        _entity.AxeEnabled = true;
    }

    private void EnableIronBoots()
    {
        _entity.BootsEnabled = true;
    }

    private void EnableFeather()
    {
        _entity.FeatherEnabled = true;
    }

    private void EnableBubble()
    {
        _entity.BubbleEnabled = true;
    }

    private void EnableFireProofArmor()
    {
        _entity.ArmorEnabled = true;
    }

    private void EnableEarthArtefact()
    {
        _entity.EarthArtefactEnabled = true;
    }

    private void EnableAirArtefact()
    {
        _entity.AirArtefactEnabled = true;
    }

    private void EnableWaterArtefact()
    {
        _entity.WaterArtefactEnabled = true;
    }

    private void EnableFireArtefact()
    {
        _entity.FireArtefactEnabled = true;
    }
}
