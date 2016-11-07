using UnityEngine;
using System.Collections;

public class ActivateTrigger : MonoBehaviour
{

    private enum ArtefactType
    {
        None,
        Earth,
        Air,
        Water,
        Fire
    }

    [SerializeField]
    private ArtefactType _artefactType;

    [SerializeField]
    private bool _axeCanTrigger = false;
    [SerializeField]
    private bool _knifeCanTrigger = false;
    [SerializeField]
    private bool _playerCanTrigger = false;

    private InventoryManager _playerInventory;

    public delegate void OnTriggerHandler();
    public event OnTriggerHandler OnTrigger;

    private void Start()
    {
        _playerInventory = StaticObjects.GetPlayer().GetComponent<InventoryManager>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (TriggerIsValid(collider) && ArtefactIsUnlocked())
        {
            OnTrigger();
        }
    }

    public void MultipleTriggersActivated()
    {
        OnTrigger();
    }

    private bool ArtefactIsUnlocked()
    {
        return !IsAnArtifactTrigger() || EarthArtefactIsUnlocked() || AirArtefactIsUnlocked() || WaterArtefactIsUnlocked() || FireArtefactIsUnlocked();
    }

    private bool IsAnArtifactTrigger()
    {
        return _artefactType != ArtefactType.None;
    }

    private bool EarthArtefactIsUnlocked()
    {   
        return _artefactType == ArtefactType.Earth && _playerInventory.EarthArtefactEnabled;
    }

    private bool AirArtefactIsUnlocked()
    {
        return _artefactType == ArtefactType.Air && _playerInventory.AirArtefactEnabled;
    }

    private bool WaterArtefactIsUnlocked()
    {
        return _artefactType == ArtefactType.Water && _playerInventory.WaterArtefactEnabled;
    }

    private bool FireArtefactIsUnlocked()
    {
        return _artefactType == ArtefactType.Fire && _playerInventory.FireArtefactEnabled;
    }

    private bool TriggerIsValid(Collider2D collider)
    {
        return AxeCollided(collider) || PlayerCollided(collider) || KnifeCollided(collider);
    }

    private bool PlayerCollided(Collider2D collider)
    {
        return _playerCanTrigger && collider.gameObject.tag == "Player";
    }

    private bool KnifeCollided(Collider2D collider)
    {
        return _knifeCanTrigger && collider.gameObject.tag == "Knife";
    }

    private bool AxeCollided(Collider2D collider)
    {
        return _axeCanTrigger && (collider.gameObject.tag == "AxeBlade" || collider.gameObject.tag == "AxeHandle");
    }

}
