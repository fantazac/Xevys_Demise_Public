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
        _healthBar = GameObject.Find("HealthBar").GetComponent<Transform>();
        _initialRectangleX = _healthBar.localScale.x;

        _health.OnHealthChanged += OnHealthChanged;
    }

    private void FixedUpdate()
    {
        if (_healthBarIsScaling)
        {
            Vector3 finalSize = new Vector3(_initialRectangleX - (100 - ((_health.HealthPoint - 10) * 100) / 1000) * _initialRectangleX / 100,
                    _healthBar.localScale.y, _healthBar.localScale.z);
            _healthBar.localScale = Vector3.Lerp(_healthBar.localScale, finalSize, Time.fixedDeltaTime);

            if (_healthBar.localScale == finalSize)
            {
                _healthBarIsScaling = false;
            }
        }
    }

    private void OnHealthChanged(int hitPoints)
    {
        if (_healthBar.localScale.x > 0)
        {
            _healthBarIsScaling = true;
        }
    }
}
