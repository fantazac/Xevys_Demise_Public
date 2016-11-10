using UnityEngine;
using System.Collections;

public class ScaleHealthBar : MonoBehaviour
{

    private float _initialRectangleX;
    private bool _healthBarIsScaling = false;
    private Transform _healthBar;
    private Health _health;

    private void Start()
    {
        _health = StaticObjects.GetPlayer().GetComponent<Health>();
        _healthBar = StaticObjects.GetHealthBar().GetComponent<Transform>();
        _initialRectangleX = _healthBar.localScale.x;

        _health.OnHealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(int hitPoints)
    {
        StartCoroutine("ScaleHealthBarCouroutine", 
            new Vector3(_initialRectangleX - (100 - ((_health.HealthPoint - 10) * 100) / 1000) * _initialRectangleX / 100,
                   _healthBar.localScale.y, _healthBar.localScale.z));
    }

    private IEnumerator ScaleHealthBarCouroutine(Vector3 finalSize)
    {
        while (_healthBar.localScale != finalSize)
        {
            _healthBar.localScale = Vector3.Lerp(_healthBar.localScale, finalSize, Time.fixedDeltaTime);
            yield return null;
        }
    }
}
