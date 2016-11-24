﻿using UnityEngine;
using System.Collections;

public class ScaleHealthBar : MonoBehaviour
{
    [SerializeField]
    private const float FINAL_SIZE_MARGIN = 0.00015f;
    [SerializeField]
    private const int SCALING_SPEED = 4;

    private Transform _healthBar;
    private Health _health;
    private Vector3 _finalSize;

    private float _healthBarLosingHealthFactor;

    private float _initialRectangleX;

    private void Start()
    {
        _health = StaticObjects.GetPlayer().GetComponent<Health>();
        _healthBar = StaticObjects.GetHealthBar().GetComponent<Transform>();
        _initialRectangleX = _healthBar.localScale.x;

        _finalSize = _healthBar.localScale;
        _healthBarLosingHealthFactor = 1f / _health.MaxHealth;

        _health.OnHealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(int hitPoints)
    {
        _finalSize -= Vector3.left * (hitPoints * _initialRectangleX * _healthBarLosingHealthFactor);
        StartCoroutine(ScaleHealthBarCouroutine());
    }

    private IEnumerator ScaleHealthBarCouroutine()
    {
        while (_healthBar.localScale.x > _finalSize.x + FINAL_SIZE_MARGIN ||
               _healthBar.localScale.x < _finalSize.x - FINAL_SIZE_MARGIN)
        {
            _healthBar.localScale = Vector3.Lerp(_healthBar.localScale, _finalSize, Time.deltaTime * SCALING_SPEED);
            yield return null;
        }
        _healthBar.localScale = _finalSize;
    }
}
