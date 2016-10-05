using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScaleHealthBar : MonoBehaviour
{
    [SerializeField]
    public const int MAX_HEALTH = 1000;

    private float _initialRectangleX;
    private Transform _healthBar;
    private Image _healthBarImage;
    private Health _health;

	private void Start ()
	{
	    _health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        _healthBar = GameObject.Find("HealthBar").GetComponent<Transform>();
	    _healthBarImage = GameObject.Find("HealthBar").GetComponent<Image>();
	    _initialRectangleX = _healthBar.localScale.x;
	    _healthBarImage.color = new Color(0, 1, 0, 1);

        _health.OnHealthChanged += OnHealthChanged;
	}

    private void OnHealthChanged(int hitPoints)
    {
        if (_healthBar.localScale.x > 0)
        {
            _healthBar.localScale = new Vector3(_initialRectangleX - (100 - ((_health.HealthPoint - hitPoints) * 100) / MAX_HEALTH) *  _initialRectangleX / 100,
                _healthBar.localScale.y, _healthBar.localScale.z);

            if (_health.HealthPoint - hitPoints >= 50f*MAX_HEALTH/100f)
            {
                _healthBarImage.color = new Color(_healthBarImage.color.r + (float)(hitPoints * 0.0020), 1, 0, 1);
            }

            else
            {
                _healthBarImage.color = new Color(1, _healthBarImage.color.g - (float)(hitPoints * 0.0020), 0, 1);
            }
        }    
        _health.HealthPoint -= hitPoints;
    }
}
