using UnityEngine;
using System.Collections;

public class PhoenixAI : MonoBehaviour {

    public enum prout
    {
        fly,
        flee,

    }
    [SerializeField]
    private float _upperLimit;
    [SerializeField]
    private float _rightLimit;
    [SerializeField]
    private float _lowerLimit;
    [SerializeField]
    private float _leftLimit;

    private const float FLIGHT_DELAY = 0.5f;
    private const float SPEED = 0.5f;

    private Rigidbody2D _rigidbody;

    private float _timeLeft;

    //In upcoming development, it would be wise to implement this variable and property into a Component.
    private bool _isFacingLeft;
    private int Orientation { get { return (_isFacingLeft ? 1 : -1); } }

    // Use this for initialization
    void Start ()
    {
        _timeLeft = 0;
        _rigidbody = GetComponent<Rigidbody2D>();
	}

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), new Vector2(10, transform.position.y), SPEED * Time.deltaTime);

        if (_timeLeft > 0)
        {
            _timeLeft -= Time.deltaTime;
        }
        else
        {
            _timeLeft = FLIGHT_DELAY;
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 2.675f);
        }
	}
}
