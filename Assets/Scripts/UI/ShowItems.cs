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
    
    private Image _selectedWeaponHighlight;
    private Image _selectedIronBootsHighlight;
    private Image _knifeSpriteRenderer;
    private Image _axeSpriteRenderer;
    private Image _featherSpriteRenderer;
    private Image _ironBootsSpriteRenderer;
    private Image _bubbleSpriteRenderer;
    private Image _fireProofArmorRenderer;
    private Image _earthArtefactRenderer;
    private Image _airArtefactRenderer;
    private Image _waterArtefactRenderer;
    private Image _fireArtefactRenderer;

    private InventoryManager _inventoryManager;
    private PlayerThrowingWeaponsMunitions _munitions;

    private void Start()
    {
        _knifeText = GameObject.Find("KnifeAmmo").GetComponent<Text>();
        _axeText = GameObject.Find("AxeAmmo").GetComponent<Text>();

        GameObject itemCanvas = GameObject.Find("ItemCanvas");
        _knifeSpriteRenderer = itemCanvas.transform.GetChild(0).GetComponent<Image>();  
        _axeSpriteRenderer = itemCanvas.transform.GetChild(1).GetComponent<Image>();
        _featherSpriteRenderer = itemCanvas.transform.GetChild(2).GetComponent<Image>();
        _ironBootsSpriteRenderer = itemCanvas.transform.GetChild(3).GetComponent<Image>();
        _bubbleSpriteRenderer = itemCanvas.transform.GetChild(4).GetComponent<Image>();
        _fireProofArmorRenderer = itemCanvas.transform.GetChild(5).GetComponent<Image>();
        _earthArtefactRenderer = itemCanvas.transform.GetChild(6).GetComponent<Image>();
        _airArtefactRenderer = itemCanvas.transform.GetChild(7).GetComponent<Image>();
        _waterArtefactRenderer = itemCanvas.transform.GetChild(8).GetComponent<Image>();
        _fireArtefactRenderer = itemCanvas.transform.GetChild(9).GetComponent<Image>();

        /*_selectedWeaponHighlight = itemCanvas.transform.GetChild(10).GetComponent<Image>();
        _selectedIronBootsHighlight = itemCanvas.transform.GetChild(11).GetComponent<Image>();*/

        _inventoryManager = StaticObjects.GetPlayer().GetComponent<InventoryManager>();
        _munitions = StaticObjects.GetPlayer().GetComponent<PlayerThrowingWeaponsMunitions>();

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
        /*_selectedWeaponHighlight.enabled = false;
        _selectedIronBootsHighlight.enabled = false;*/
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

        /*_selectedIronBootsHighlight.transform.position = new Vector3(_ironBootsSpriteRenderer.transform.position.x,
            _ironBootsSpriteRenderer.transform.position.y, _ironBootsSpriteRenderer.transform.position.z + Z_OFFSET_ITEM_SELECT);*/
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
        /*_selectedWeaponHighlight.transform.position = new Vector3(_knifeSpriteRenderer.transform.position.x,
            _knifeSpriteRenderer.transform.position.y, _knifeSpriteRenderer.transform.position.z + Z_OFFSET_ITEM_SELECT);*/
    }

    public void SelectAxe()
    {
        /*_selectedWeaponHighlight.transform.position = new Vector3(_axeSpriteRenderer.transform.position.x,
            _axeSpriteRenderer.transform.position.y, _axeSpriteRenderer.transform.position.z + Z_OFFSET_ITEM_SELECT);*/
    }

    private void OnEnableKnife()
    {
        /*_selectedWeaponHighlight.enabled = true;*/
        _knifeSpriteRenderer.enabled = true;
        _knifeText.enabled = true;
        SelectKnife();
    }

    private void OnEnableAxe()
    {
        /*_selectedWeaponHighlight.enabled = true;*/
        _axeSpriteRenderer.enabled = true;
        _axeText.enabled = true;
        SelectAxe();
    }

    public void IronBootsSelect()
    {
        /*_selectedIronBootsHighlight.enabled = !_selectedIronBootsHighlight.enabled;*/
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
