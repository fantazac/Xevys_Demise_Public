using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIInventoryView : MonoBehaviour
{
    private Text _knifeText;
    private Text _axeText;
    
    private Image _knifeHighlight;
    private Image _knifeImage;
    private Image _axeHighlight;
    private Image _axeImage;
    private Image _featherImage;
    private Image _ironBootsHighlight;
    private Image _ironBootsImage;
    private Image _bubbleImage;
    private Image _fireProofArmorImage;
    private Image _earthArtefactImage;
    private Image _airArtefactImage;
    private Image _waterArtefactImage;
    private Image _fireArtefactImage;

    private InventoryManager _inventoryManager;
    private PlayerWeaponAmmo _munitions;

    private void Start()
    {
        GameObject itemCanvas = StaticObjects.GetItemCanvas();

        // Ne pas changer l'ordre : c'est celui des GameObjects de ItemCanvas
        _knifeHighlight = itemCanvas.transform.GetChild(0).GetComponent<Image>();
        _knifeImage = itemCanvas.transform.GetChild(1).GetComponent<Image>();
        _knifeText = itemCanvas.transform.GetChild(1).GetComponentInChildren<Text>();
        _axeHighlight = itemCanvas.transform.GetChild(2).GetComponent<Image>();
        _axeImage = itemCanvas.transform.GetChild(3).GetComponent<Image>();
        _axeText = itemCanvas.transform.GetChild(3).GetComponentInChildren<Text>();
        _featherImage = itemCanvas.transform.GetChild(4).GetComponent<Image>();
        _ironBootsHighlight = itemCanvas.transform.GetChild(5).GetComponent<Image>();
        _ironBootsImage = itemCanvas.transform.GetChild(6).GetComponent<Image>();
        _bubbleImage = itemCanvas.transform.GetChild(7).GetComponent<Image>();
        _fireProofArmorImage = itemCanvas.transform.GetChild(8).GetComponent<Image>();
        _earthArtefactImage = itemCanvas.transform.GetChild(9).GetComponent<Image>();
        _airArtefactImage = itemCanvas.transform.GetChild(10).GetComponent<Image>();
        _waterArtefactImage = itemCanvas.transform.GetChild(11).GetComponent<Image>();
        _fireArtefactImage = itemCanvas.transform.GetChild(12).GetComponent<Image>();

        _inventoryManager = StaticObjects.GetPlayer().GetComponent<InventoryManager>();
        _munitions = StaticObjects.GetPlayer().GetComponent<PlayerWeaponAmmo>();

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
        _knifeHighlight.enabled = false;
        _axeHighlight.enabled = false;
        _ironBootsHighlight.enabled = false;
        _knifeImage.enabled = false;
        _axeImage.enabled = false;
        _featherImage.enabled = false;
        _ironBootsImage.enabled = false;
        _bubbleImage.enabled = false;
        _fireProofArmorImage.enabled = false;
        _earthArtefactImage.enabled = false;
        _airArtefactImage.enabled = false;
        _waterArtefactImage.enabled = false;
        _fireArtefactImage.enabled = false;
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
        _knifeHighlight.enabled = true;
        _axeHighlight.enabled = false;
    }

    public void SelectAxe()
    {
        _axeHighlight.enabled = true;
        _knifeHighlight.enabled = false;
    }

    private void OnEnableKnife()
    {
        _knifeImage.enabled = true;
        _knifeText.enabled = true;
        SelectKnife();
    }

    private void OnEnableAxe()
    {
        _axeImage.enabled = true;
        _axeText.enabled = true;
        SelectAxe();
    }

    public void IronBootsSelect()
    {
        _ironBootsHighlight.enabled = !_ironBootsHighlight.enabled;
    }

    private void OnEnableFeather()
    {
        _featherImage.enabled = true;
    }

    private void OnEnableIronBoots()
    {
        _ironBootsImage.enabled = true;
    }

    private void OnEnableBubble()
    {
        _bubbleImage.enabled = true;
    }

    private void OnEnableFireProofArmor()
    {
        _fireProofArmorImage.enabled = true;
    }

    private void OnEnableEarthArtefact()
    {
        _earthArtefactImage.enabled = true;
    }

    private void OnEnableAirArtefact()
    {
        _airArtefactImage.enabled = true;
    }

    private void OnEnableWaterArtefact()
    {
        _waterArtefactImage.enabled = true;
    }

    private void OnEnableFireArtefact()
    {
        _fireArtefactImage.enabled = true;
    }
}
