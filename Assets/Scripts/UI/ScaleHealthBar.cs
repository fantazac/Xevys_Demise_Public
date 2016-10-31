using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScaleHealthBar : MonoBehaviour
{
    [SerializeField]
    public const int MAX_HEALTH = 1000;
    [SerializeField]
    private Color _color;

    private float _initialRectangleX;
    private bool _healthBarIsScaling = false;
    private Transform _healthBar;
    private Image _healthBarImage;
    private Health _health;

	private void Start ()
	{
	    _health = Player.GetPlayer().GetComponent<Health>();
        _healthBar = GameObject.Find("HealthBar").GetComponent<Transform>();
	    _healthBarImage = GameObject.Find("HealthBar").GetComponent<Image>();
	    _initialRectangleX = _healthBar.localScale.x;
	    _color = new Color(0, 1, 0, 1);
	    _healthBarImage.color = _color;

        _health.OnHealthChanged += OnHealthChanged;
	}

    private void FixedUpdate()
    {
        // Ajuste la taille de la barre de vie en fonction du temps
        if (_healthBarIsScaling)
        {
            Vector3 finalSize = new Vector3(_initialRectangleX - (100 - ((_health.HealthPoint - 10)*100)/MAX_HEALTH)*_initialRectangleX/100,
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

            // Change la couleur de la barre de vie en fonction du % de vie restant
            if (_health.HealthPoint - hitPoints >= 50f*MAX_HEALTH/100f)
            {
                Color interpolatedColor = _healthBarImage.color;
                interpolatedColor.r = _healthBarImage.color.r + (float)(hitPoints*0.0020);
                interpolatedColor.g = 1;
                interpolatedColor.b = 0;
                interpolatedColor.a = 1;
                _healthBarImage.color = interpolatedColor;
            }
            else
            {
                Color interpolatedColor = _healthBarImage.color;
                interpolatedColor.r = 1;
                interpolatedColor.g = _healthBarImage.color.g - (float)(hitPoints * 0.0020);
                interpolatedColor.b = 0;
                interpolatedColor.a = 1;
                _healthBarImage.color = interpolatedColor;
            }
        }    
    }
}
