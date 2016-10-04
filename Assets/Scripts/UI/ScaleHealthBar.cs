using UnityEngine;
using System.Collections;

public class ScaleHealthBar : MonoBehaviour
{
    [SerializeField]
    public const int MAX_HEALTH = 1000;

    private float _initialTriangleX;
    private float _initialRectangleX;
    private Transform _healthTriangle;
    private Transform _healthRectangle;
    private Health _health;

	private void Start ()
	{
	    _health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
	    _healthTriangle = GameObject.Find("HealthBarTriangle").transform;
        _healthRectangle = GameObject.Find("HealthBarRectangle").transform;
	    _initialRectangleX = _healthTriangle.localScale.x;
	    _initialTriangleX = _healthRectangle.localScale.x;

	    _health.OnHealthChanged += OnHealthChanged;
	}
	
	private void Update ()
	{
	    
	}

    private void OnHealthChanged(int hitPoints)
    {
        // Si la vie est à 90% du max ou plus, on travaille sur le triangle
        if (_healthTriangle.localScale.x > 0)
        {         
            _healthTriangle.localScale = new Vector2(_healthTriangle.localScale.x - (100 - ((_health.HealthPoint - hitPoints) * 100) / MAX_HEALTH) * _initialTriangleX / 100, 
                _healthTriangle.localScale.y);          
        }

        if (_healthTriangle.localScale.x <= 0 && _healthRectangle.localScale.x > 0)
        {
            _healthTriangle.localScale = Vector2.zero;
            _healthRectangle.localScale = new Vector2(_healthRectangle.localScale.x - (100 - ((_health.HealthPoint - hitPoints) * 100) / MAX_HEALTH) *  _initialRectangleX / 100,
                _healthRectangle.localScale.y);
            _healthRectangle.localPosition = new Vector2();
        }

        _health.HealthPoint -= hitPoints;
    }
}
