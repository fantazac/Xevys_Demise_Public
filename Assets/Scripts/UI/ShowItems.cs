﻿using System;
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
    private SpriteRenderer _bubbleSpriteRenderer;
    private SpriteRenderer _fireProofArmorRenderer;
    private SpriteRenderer _earthKeyRenderer;
    private SpriteRenderer _airKeyRenderer;
    private SpriteRenderer _waterKeyRenderer;
    private SpriteRenderer _fireKeyRenderer;
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
        _bubbleSpriteRenderer = GameObject.Find("BubbleFrame").GetComponent<SpriteRenderer>();
        _fireProofArmorRenderer = GameObject.Find("FireProofArmorFrame").GetComponent<SpriteRenderer>();
        _earthKeyRenderer = GameObject.Find("EarthKeyFrame").GetComponent<SpriteRenderer>();
        _airKeyRenderer = GameObject.Find("AirKeyFrame").GetComponent<SpriteRenderer>();
        _waterKeyRenderer = GameObject.Find("WaterKeyFrame").GetComponent<SpriteRenderer>();
        _fireKeyRenderer = GameObject.Find("FireKeyFrame").GetComponent<SpriteRenderer>();

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
        _earthKeyRenderer.enabled = false;
        _airKeyRenderer.enabled = false;
        _waterKeyRenderer.enabled = false;
        _fireKeyRenderer.enabled = false;

        _selectedIronBootsHighlight.transform.position = new Vector3(_ironBootsSpriteRenderer.transform.position.x,
            _ironBootsSpriteRenderer.transform.position.y, _ironBootsSpriteRenderer.transform.position.z + 5);
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
        _selectedIronBootsHighlight.enabled = !_selectedIronBootsHighlight.enabled;
    }

    public void OnFeatherEnabled()
    {
        _featherSpriteRenderer.enabled = true;
    }

    public void OnIronBootsEnabled()
    {
        _ironBootsSpriteRenderer.enabled = true;
    }

    public void OnBubbleEnabled()
    {
        _bubbleSpriteRenderer.enabled = true;
    }

    public void OnFireProofArmorEnabled()
    {
        _fireProofArmorRenderer.enabled = true;
    }

    public void OnEarthKeyEnabled()
    {
        _earthKeyRenderer.enabled = true;
    }

    public void OnAirKeyEnabled()
    {
        _airKeyRenderer.enabled = true;
    }

    public void OnWaterKeyEnabled()
    {
        _waterKeyRenderer.enabled = true;
    }

    public void OnFireKeyEnabled()
    {
        _fireKeyRenderer.enabled = true;
    }
}
