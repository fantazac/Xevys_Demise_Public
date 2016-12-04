using UnityEngine;

public class AccountStatsDataHandler : MonoBehaviour
{
    public delegate void OnInventoryReloadedHandler(bool knifeEnabled, bool axeEnabled, bool featherEnabled, bool bootsEnabled, bool bubbleEnabled, bool armorEnabled, bool earthArtefactEnabled, bool airArtefactEnabled, bool waterArtefactEnabled, bool fireArtefactEnabled);
    public event OnInventoryReloadedHandler OnInventoryReloaded;
    public delegate void OnAmmoReloadedHandler(int knifeAmmo, int axeAmmo);
    public event OnAmmoReloadedHandler OnAmmoReloaded;
    public delegate void OnHealthReloadedHandler(int health);
    public event OnHealthReloadedHandler OnHealthReloaded;

    private AccountStatsEntity _temporaryEntity;
    private AccountStatsEntity _entity;
    private AccountStatsRepository _repository;

    private void Start()
    {
        _repository = new AccountStatsRepository();
        _temporaryEntity = _repository.Get(StaticObjects.AccountId);
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

    public void UpdateEntity()
    {
        _entity = _temporaryEntity;
    }

    public void UpdateRepository()
    {
        _repository.UpdateEntity(_entity);
    }

    private void EnemyKilled(string tag)
    {
        if (tag == StaticObjects.GetUnityTags().Scarab)
        {
            _temporaryEntity.NbScarabsKilled++;
        }
        else if (tag == StaticObjects.GetUnityTags().Bat)
        {
            _temporaryEntity.NbBatsKilled++;
        }
        else if (tag == StaticObjects.GetUnityTags().Skeltal)
        {
            _temporaryEntity.NbSkeltalsKilled++;
        }
    }

    private void EnableKnife()
    {
        _temporaryEntity.KnifeEnabled = true;
    }

    private void EnableAxe()
    {
        _temporaryEntity.AxeEnabled = true;
    }

    private void EnableIronBoots()
    {
        _temporaryEntity.BootsEnabled = true;
    }

    private void EnableFeather()
    {
        _temporaryEntity.FeatherEnabled = true;
    }

    private void EnableBubble()
    {
        _temporaryEntity.BubbleEnabled = true;
    }

    private void EnableFireProofArmor()
    {
        _temporaryEntity.ArmorEnabled = true;
    }

    private void EnableEarthArtefact()
    {
        _temporaryEntity.EarthArtefactEnabled = true;
    }

    private void EnableAirArtefact()
    {
        _temporaryEntity.AirArtefactEnabled = true;
    }

    private void EnableWaterArtefact()
    {
        _temporaryEntity.WaterArtefactEnabled = true;
    }

    private void EnableFireArtefact()
    {
        _temporaryEntity.FireArtefactEnabled = true;
    }
}
