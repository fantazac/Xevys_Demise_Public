using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowEquippedWeapons : MonoBehaviour
{
    private Text _knifeText;
    private Text _axeText;
    private SpriteRenderer _knifeSpriteRenderer;
    private SpriteRenderer _axeSpriteRenderer;

    private void Start()
    {
        _knifeText = GameObject.Find("KnifeAmmo").GetComponent<Text>();
        _axeText = GameObject.Find("AxeAmmo").GetComponent<Text>();
        _knifeSpriteRenderer = GameObject.Find("WeaponSelectKnife").GetComponent<SpriteRenderer>();
        _axeSpriteRenderer = GameObject.Find("WeaponSelectAxe").GetComponent<SpriteRenderer>();

        _knifeText.enabled = false;
        _axeText.enabled = false;
        _knifeSpriteRenderer.enabled = false;
        _axeSpriteRenderer.enabled = false;
    }

    public void OnKnifeAmmoChanged(int total)
    {
        if (!_knifeSpriteRenderer.enabled)
        {
            _knifeSpriteRenderer.enabled = true;
            _knifeText.enabled = true;
        }
        _knifeText.text = total.ToString();
    }

    public void OnAxeAmmoChanged(int total)
    {
        if (!_axeSpriteRenderer.enabled)
        {
            _axeSpriteRenderer.enabled = true;
            _axeText.enabled = true;
        }
        _axeText.text = total.ToString();
    }

}
