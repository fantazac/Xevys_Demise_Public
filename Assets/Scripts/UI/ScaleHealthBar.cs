using UnityEngine;
using System.Collections;

public class ScaleHealthBar : MonoBehaviour
{
    [SerializeField]
    private int _scalingSpeed = 4;

    private Transform _healthBar;
    private Health _health;
    private Vector3 _finalSize;

    private float _healthBarLosingHealthFactor;

    private float _initialRectangleX;

    private void Start()
    {
        _health = StaticObjects.GetPlayer().GetComponent<Health>();
        _healthBar = StaticObjects.GetHealthBar().transform;
        _initialRectangleX = _healthBar.localScale.x;

        _finalSize = _healthBar.localScale;
        _healthBarLosingHealthFactor = 1f / _health.MaxHealth;

        _health.OnHealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(int hitPoints)
    {
        _finalSize -= Vector3.left * hitPoints * _initialRectangleX * _healthBarLosingHealthFactor;
        StartCoroutine(SetHealthBarSize());
    }

    private IEnumerator SetHealthBarSize()
    {
        while (_healthBar.localScale.x != _finalSize.x)
        {
            _healthBar.localScale = Vector3.Lerp(_healthBar.localScale, _finalSize, Time.deltaTime * _scalingSpeed);
            yield return null;
        }
        _healthBar.localScale = _finalSize;
    }
}
