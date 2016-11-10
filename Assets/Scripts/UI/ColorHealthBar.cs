using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ColorHealthBar : MonoBehaviour
{
    [SerializeField]
    public const int MAX_HEALTH = 1000;
    [SerializeField]
    public const float HP_MULTIPLICATOR_FOR_COLOR = 0.0010f;

    [SerializeField]
    private Color _color;

    private Transform _healthBar;
    private Image _healthBarImage;
    private Health _health;

	private void Start()
	{
	    _health = StaticObjects.GetPlayer().GetComponent<Health>();
        _healthBar = StaticObjects.GetHealthBar().GetComponent<Transform>();
	    _healthBarImage = StaticObjects.GetHealthBar().GetComponent<Image>();
	    _color = new Color(0, 1, 0, 1);
	    _healthBarImage.color = _color;

        _health.OnHealthChanged += OnHealthChanged;
	}

    private void OnHealthChanged(int hitPoints)
    {
        // Change la couleur de la barre de vie en fonction du % de vie restant
        if (_health.HealthPoint + hitPoints >= 50f * MAX_HEALTH / 100f)
        {
            Color interpolatedColor = _healthBarImage.color;
            interpolatedColor.r = _healthBarImage.color.r - hitPoints * HP_MULTIPLICATOR_FOR_COLOR;
            interpolatedColor.g = 1;
            interpolatedColor.b = 0;
            interpolatedColor.a = 1;
            _healthBarImage.color = interpolatedColor;
        }
        else
        {
            Color interpolatedColor = _healthBarImage.color;
            interpolatedColor.r = 1;
            interpolatedColor.g = _healthBarImage.color.g + hitPoints * HP_MULTIPLICATOR_FOR_COLOR;
            interpolatedColor.b = 0;
            interpolatedColor.a = 1;
            _healthBarImage.color = interpolatedColor;
        }
    }
}