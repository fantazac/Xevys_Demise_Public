using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ColorHealthBar : MonoBehaviour
{
    private Image _healthBarImage;
    private Health _health;

    private float _halfHealth;

	private void Start()
	{
	    _health = StaticObjects.GetPlayer().GetComponent<Health>();
	    _healthBarImage = StaticObjects.GetHealthBar().GetComponent<Image>();
	    _healthBarImage.color = new Color(0, 1, 0, 1);
        _halfHealth = _health.MaxHealth * 0.5f;

        _health.OnHealthChanged += OnHealthChanged;
	}

    private void OnHealthChanged(int hitPoints)
    {
        if (_health.HealthPoint >= _halfHealth)
        {
            Color interpolatedColor;
            interpolatedColor.r = 1 - ((_health.HealthPoint - _halfHealth) / _halfHealth);
            interpolatedColor.g = 1;
            interpolatedColor.b = 0;
            interpolatedColor.a = 1;
            _healthBarImage.color = interpolatedColor;
        }
        else
        {
            Color interpolatedColor;
            interpolatedColor.r = 1;
            interpolatedColor.g = 1 - ((_halfHealth - _health.HealthPoint) / _halfHealth);
            interpolatedColor.b = 0;
            interpolatedColor.a = 1;
            _healthBarImage.color = interpolatedColor;
        }
    }
}