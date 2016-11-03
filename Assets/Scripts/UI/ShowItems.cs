using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowItems : MonoBehaviour
{
    [SerializeField]
    private const int Z_OFFSET_ITEM_SELECT = 5;

    private Text _knifeText;
    private Text _axeText;
    
    private SpriteRenderer _selectedWeaponHighlight;
    private SpriteRenderer _selectedIronBootsHighlight;
    private SpriteRenderer _knifeSpriteRenderer;
    private SpriteRenderer _axeSpriteRenderer;
    private SpriteRenderer _featherSpriteRenderer;
    private SpriteRenderer _ironBootsSpriteRenderer;
    private SpriteRenderer _bubbleSpriteRenderer;
    private SpriteRenderer _fireProofArmorRenderer;
    private SpriteRenderer _earthArtefactRenderer;
    private SpriteRenderer _airArtefactRenderer;
    private SpriteRenderer _waterArtefactRenderer;
    private SpriteRenderer _fireArtefactRenderer;

    private InventoryManager _inventoryManager;
    private PlayerThrowingWeaponsMunitions _munitions;

    private void Start()
    {
        _knifeText = GameObject.Find("KnifeAmmo").GetComponent<Text>();
        _axeText = GameObject.Find("AxeAmmo").GetComponent<Text>();
        _selectedWeaponHighlight = GameObject.Find("WeaponSelectHighlight").GetComponent<SpriteRenderer>();
        _selectedIronBootsHighlight = GameObject.Find("IronBootsHighlight").GetComponent<SpriteRenderer>();
        _knifeSpriteRenderer = GameObject.Find("WeaponSelectKnife").GetComponent<SpriteRenderer>();
        _axeSpriteRenderer = GameObject.Find("WeaponSelectAxe").GetComponent<SpriteRenderer>();
        _featherSpriteRenderer = GameObject.Find("FeatherFrame").GetComponent<SpriteRenderer>();
        _ironBootsSpriteRenderer = GameObject.Find("IronBootsFrame").GetComponent<SpriteRenderer>();
        _inventoryManager = Player.GetPlayer().GetComponent<InventoryManager>();
        _munitions = Player.GetPlayer().GetComponent<PlayerThrowingWeaponsMunitions>();
        _bubbleSpriteRenderer = GameObject.Find("BubbleFrame").GetComponent<SpriteRenderer>();
        _fireProofArmorRenderer = GameObject.Find("FireProofArmorFrame").GetComponent<SpriteRenderer>();
        _earthArtefactRenderer = GameObject.Find("EarthArtefactFrame").GetComponent<SpriteRenderer>();
        _airArtefactRenderer = GameObject.Find("AirArtefactFrame").GetComponent<SpriteRenderer>();
        _waterArtefactRenderer = GameObject.Find("WaterArtefactFrame").GetComponent<SpriteRenderer>();
        _fireArtefactRenderer = GameObject.Find("FireArtefactFrame").GetComponent<SpriteRenderer>();

        _inventoryManager.OnEnableAxe += OnEnableAxe;
        _inventoryManager.OnEnableKnife += OnEnableKnife;
        _inventoryManager.OnEnableFeather += OnEnableFeather;
        _inventoryManager.OnEnableIronBoots += OnEnableIronBoots;
        _inventoryManager.OnEnableBubble += OnEnableBubble;
        _inventoryManager.OnEnableFireProofArmor += OnEnableFireProofArmor;
        _inventoryManager.OnEnableAirArtefact += OnEnableAirArtefact;
        _inventoryManager.OnEnableEarthArtefact += OnEnableEarthArtefact;
        _inventoryManager.OnEnableWaterArtefact += OnEnableWaterArtefact;
        _inventoryManager.OnEnableFireArtefact += OnEnableFireArtefact;

        _munitions.OnAxeAmmoChanged += AxeAmmoChange;
        _munitions.OnKnifeAmmoChanged += KnifeAmmoChange;

        _knifeText.enabled = false;
        _axeText.enabled = false;
        _selectedWeaponHighlight.enabled = false;
        _selectedIronBootsHighlight.enabled = false;
        _knifeSpriteRenderer.enabled = false;
        _axeSpriteRenderer.enabled = false;
        _featherSpriteRenderer.enabled = false;
        _ironBootsSpriteRenderer.enabled = false;
        _bubbleSpriteRenderer.enabled = false;
        _fireProofArmorRenderer.enabled = false;
        _earthArtefactRenderer.enabled = false;
        _airArtefactRenderer.enabled = false;
        _waterArtefactRenderer.enabled = false;
        _fireArtefactRenderer.enabled = false;

        _selectedIronBootsHighlight.transform.position = new Vector3(_ironBootsSpriteRenderer.transform.position.x,
            _ironBootsSpriteRenderer.transform.position.y, _ironBootsSpriteRenderer.transform.position.z + Z_OFFSET_ITEM_SELECT);
    }

    private void KnifeAmmoChange(int total)
    {
        _knifeText.text = total.ToString();
    }

    private void AxeAmmoChange(int total)
    {
        _axeText.text = total.ToString();
    }

    public void SelectKnife()
    {
        _selectedWeaponHighlight.transform.position = new Vector3(_knifeSpriteRenderer.transform.position.x,
            _knifeSpriteRenderer.transform.position.y, _knifeSpriteRenderer.transform.position.z + Z_OFFSET_ITEM_SELECT);
    }

    public void SelectAxe()
    {
        _selectedWeaponHighlight.transform.position = new Vector3(_axeSpriteRenderer.transform.position.x,
            _axeSpriteRenderer.transform.position.y, _axeSpriteRenderer.transform.position.z + Z_OFFSET_ITEM_SELECT);
    }

    private void OnEnableKnife()
    {
        _selectedWeaponHighlight.enabled = true;
        _knifeSpriteRenderer.enabled = true;
        _knifeText.enabled = true;
        SelectKnife();
    }

    private void OnEnableAxe()
    {
        _selectedWeaponHighlight.enabled = true;
        _axeSpriteRenderer.enabled = true;
        _axeText.enabled = true;
        SelectAxe();
    }

    public void IronBootsSelect()
    {
        _selectedIronBootsHighlight.enabled = !_selectedIronBootsHighlight.enabled;
    }

    private void OnEnableFeather()
    {
        _featherSpriteRenderer.enabled = true;
    }

    private void OnEnableIronBoots()
    {
        _ironBootsSpriteRenderer.enabled = true;
    }

    private void OnEnableBubble()
    {
        _bubbleSpriteRenderer.enabled = true;
    }

    private void OnEnableFireProofArmor()
    {
        _fireProofArmorRenderer.enabled = true;
    }

    private void OnEnableEarthArtefact()
    {
        _earthArtefactRenderer.enabled = true;
    }

    private void OnEnableAirArtefact()
    {
        _airArtefactRenderer.enabled = true;
    }

    private void OnEnableWaterArtefact()
    {
        _waterArtefactRenderer.enabled = true;
    }

    private void OnEnableFireArtefact()
    {
        _fireArtefactRenderer.enabled = true;
    }
}
