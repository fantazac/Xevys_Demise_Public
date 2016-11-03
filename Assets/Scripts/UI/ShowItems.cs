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

        _inventoryManager.OnEnableAxe += EnableAxe;
        _inventoryManager.OnEnableKnife += EnableKnife;

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

    public void KnifeAmmoChange(int total)
    {
        _knifeText.text = total.ToString();
    }

    public void AxeAmmoChange(int total)
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

    private void EnableKnife()
    {
        _selectedWeaponHighlight.enabled = true;
        _knifeSpriteRenderer.enabled = true;
        _knifeText.enabled = true;
        SelectKnife();
    }

    private void EnableAxe()
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

    public void FeatherEnable()
    {
        _featherSpriteRenderer.enabled = true;
    }

    public void IronBootsEnable()
    {
        _ironBootsSpriteRenderer.enabled = true;
    }

    public void BubbleEnable()
    {
        _bubbleSpriteRenderer.enabled = true;
    }

    public void FireProofArmorEnable()
    {
        _fireProofArmorRenderer.enabled = true;
    }

    public void EarthArtefactEnable()
    {
        _earthArtefactRenderer.enabled = true;
    }

    public void AirArtefactEnable()
    {
        _airArtefactRenderer.enabled = true;
    }

    public void WaterArtefactEnable()
    {
        _waterArtefactRenderer.enabled = true;
    }

    public void FireArtefactEnable()
    {
        _fireArtefactRenderer.enabled = true;
    }
}
