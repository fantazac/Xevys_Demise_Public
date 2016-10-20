using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowItems : MonoBehaviour
{
    private Text _knifeText;
    private Text _axeText;
    private SpriteRenderer _selectedWeaponHighlight;
    private SpriteRenderer _selectedIronBootsHighlight;
    private SpriteRenderer _knifeSpriteRenderer;
    private SpriteRenderer _axeSpriteRenderer;
    private SpriteRenderer _featherSpriteRenderer;
    private SpriteRenderer _ironBootsSpriteRenderer;
    private InventoryManager _inventoryManager;

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
        _inventoryManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryManager>();

        _knifeText.enabled = false;
        _axeText.enabled = false;
        _selectedWeaponHighlight.enabled = false;
        _selectedIronBootsHighlight.enabled = false;
        _knifeSpriteRenderer.enabled = false;
        _axeSpriteRenderer.enabled = false;
        _featherSpriteRenderer.enabled = false;
        _ironBootsSpriteRenderer.enabled = false;
    }

    public void OnKnifeAmmoChanged(int total)
    {
        if (!_inventoryManager.KnifeEnabled)
        {
            _inventoryManager.EnableKnife();
            _knifeSpriteRenderer.enabled = true;
            _knifeText.enabled = true;
            OnKnifeSelected();
        }
        _knifeText.text = total.ToString();
    }

    public void OnAxeAmmoChanged(int total)
    {
        if (!_inventoryManager.AxeEnabled)
        {
            _inventoryManager.EnableAxe();
            _axeSpriteRenderer.enabled = true;
            _axeText.enabled = true;
            OnAxeSelected();
        }
        _axeText.text = total.ToString();
    }

    public void OnKnifeSelected()
    {
        if (!_selectedWeaponHighlight.enabled)
        {
            _selectedWeaponHighlight.enabled = true;
        }
        _selectedWeaponHighlight.transform.position = new Vector3(_knifeSpriteRenderer.transform.position.x,
            _knifeSpriteRenderer.transform.position.y, _knifeSpriteRenderer.transform.position.z + 5);
    }

    public void OnAxeSelected()
    {
        if (!_selectedWeaponHighlight.enabled)
        {
            _selectedWeaponHighlight.enabled = true;
        }
        _selectedWeaponHighlight.transform.position = new Vector3(_axeSpriteRenderer.transform.position.x,
            _axeSpriteRenderer.transform.position.y, _axeSpriteRenderer.transform.position.z + 5);
    }

    public void OnIronBootsSelected()
    {
        if (!_selectedIronBootsHighlight.enabled)
        {
            _selectedWeaponHighlight.enabled = true;
        }
        _selectedWeaponHighlight.transform.position = new Vector3(_axeSpriteRenderer.transform.position.x,
            _axeSpriteRenderer.transform.position.y, _axeSpriteRenderer.transform.position.z + 5);
    }

    public void OnFeatherEnabled()
    {
        _featherSpriteRenderer.enabled = true;
    }

    public void OnIronBootsEnabled()
    {
        _ironBootsSpriteRenderer.enabled = true;
    }

}
