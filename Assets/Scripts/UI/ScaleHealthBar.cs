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

    private const float BAR_SCALING_MARGIN = 0.00015f;

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
        StopAllCoroutines();
        StartCoroutine(SetHealthBarSize());
    }

    private IEnumerator SetHealthBarSize()
    {
        while (_healthBar.localScale.x < _finalSize.x - BAR_SCALING_MARGIN || 
            _healthBar.localScale.x > _finalSize.x + BAR_SCALING_MARGIN)
        {
            _healthBar.localScale = Vector3.Lerp(_healthBar.localScale, _finalSize, Time.deltaTime * _scalingSpeed);
            yield return null;
        }
        _healthBar.localScale = _finalSize;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
