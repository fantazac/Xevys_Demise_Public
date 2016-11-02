using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ColorHealthBar : MonoBehaviour
{
    [SerializeField]
    public const int MAX_HEALTH = 1000;
    [SerializeField]
    public const float HP_MULTIPLICATOR_FOR_COLOR = 0.0020f;

    [SerializeField]
    private Color _color;

    private Transform _healthBar;
    private Image _healthBarImage;
    private Health _health;

	private void Start ()
	{
	    _health = Player.GetPlayer().GetComponent<Health>();
        _healthBar = GameObject.Find("HealthBar").GetComponent<Transform>();
	    _healthBarImage = GameObject.Find("HealthBar").GetComponent<Image>();
	    _color = new Color(0, 1, 0, 1);
	    _healthBarImage.color = _color;

        _health.OnHealthChanged += OnHealthChanged;
	}

    private void OnHealthChanged(int healthPoints)
    {
        if (_healthBar.localScale.x > 0)
        {
            // Change la couleur de la barre de vie en fonction du % de vie restant
            if (_health.HealthPoint + healthPoints >= 50f * MAX_HEALTH / 100f)
            {
                Color interpolatedColor = _healthBarImage.color;
                interpolatedColor.r = _healthBarImage.color.r + (healthPoints * HP_MULTIPLICATOR_FOR_COLOR);
                interpolatedColor.g = 1;
                interpolatedColor.b = 0;
                interpolatedColor.a = 1;
                _healthBarImage.color = interpolatedColor;
            }
            else
            {
                Color interpolatedColor = _healthBarImage.color;
                interpolatedColor.r = 1;
                interpolatedColor.g = _healthBarImage.color.g - (healthPoints * HP_MULTIPLICATOR_FOR_COLOR);
                interpolatedColor.b = 0;
                interpolatedColor.a = 1;
                _healthBarImage.color = interpolatedColor;
            }
        }    
    }
}
