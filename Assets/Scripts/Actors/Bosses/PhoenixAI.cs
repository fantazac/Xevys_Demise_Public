using UnityEngine;
using System.Collections;

public class PhoenixAI : MonoBehaviour {

    public enum PhoenixStatus
    {
        fly,
        flee,
        attack,
    }
    [SerializeField]
    private Vector2 _northEastLimit;
    [SerializeField]
    private Vector2 _southEastLimit;
    [SerializeField]
    private Vector2 _southWestLimit;
    [SerializeField]
    private Vector2 _northWestLimit;

    private const float FLIGHT_DELAY = 0.5f;
    private const float ATTACK_DELAY = 5;
    private const float PLAYER_APPROACH_LIMIT = 6;
    private const float SPEED = 5;
    private const float WING_FLAP = 2.675f;

    private Vector2 currentPoint;
    private Vector2 closestPoint;
    private Vector2 playerPosition;
    private Vector2 attackPosition;
    private Rigidbody2D _rigidbody;
    private FlipEnemy _flipEnemy;
    //These components should eventually be placed into a script for all bosses (think heritage) as they are only used for a death status.
    private Health _health;
    private BoxCollider2D _boxCollider;
    private SpriteRenderer _spriteRenderer;

    private System.Random _rng = new System.Random();
    private PhoenixStatus _status;
    private float _flightTimeLeft;
    private float _attackCooldownTimeLeft;
    private float _closestHorizontalPoint;
    private float _closestVerticalPoint;
    //Variables used for attack in a parabolic pattern.The function y = (x - x1)(x - x2) is used.
    //In this context, _attackXAxisFirstPoint, or x1, is always 0, so it's neglectable.
    private float _attackXAxis;
    private float _attackXAxisSecondPoint;

    // Use this for initialization
    private void Start ()
    {
        _status = PhoenixStatus.fly;
        _flightTimeLeft = 0;
        _attackCooldownTimeLeft = 0;
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _flipEnemy = GetComponent<FlipEnemy>();
        currentPoint = _southWestLimit;
	}

    // Update is called once per frame
    private void Update()
    {
        //This status allows Phoenix to watch the player and either charge on him after a few seconds or flee.
        if (_status == PhoenixStatus.fly)
        {
            _attackCooldownTimeLeft += Time.fixedDeltaTime;
            if (_attackCooldownTimeLeft > ATTACK_DELAY)
            {
                attackPosition = transform.position;
                playerPosition = GameObject.Find("Character").transform.position;
                _attackCooldownTimeLeft = 0;
                _rigidbody.isKinematic = true;
                _attackXAxis = 0;
                _attackXAxisSecondPoint = (playerPosition.x - attackPosition.x) * 2;
                _status = PhoenixStatus.attack;
            }
            else
            {
                float playerDistance = Vector2.Distance(GameObject.Find("Character").transform.position, transform.position);
                if (playerDistance < PLAYER_APPROACH_LIMIT)
                {
                    int randomNumber = _rng.Next();
                    if (currentPoint.Equals(_northEastLimit) || currentPoint.Equals(_southWestLimit))
                    {
                        closestPoint = (randomNumber % 2 == 0 ? _northWestLimit : _southEastLimit);
                    }
                    else
                    {
                        closestPoint = (randomNumber % 2 == 0 ? _northEastLimit : _southWestLimit);
                    }
                    _rigidbody.isKinematic = true;
                    _status = PhoenixStatus.flee;
                }
                else
                {
                    if (_flightTimeLeft > 0)
                    {
                        _flightTimeLeft -= Time.fixedDeltaTime;
                    }
                    else
                    {
                        _flightTimeLeft = FLIGHT_DELAY;
                        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, WING_FLAP);
                    }
                }
            }
        }
        //Flee status makes Phoenix go to a neighbouring point in order to avoid the player.
        else if (_status == PhoenixStatus.flee)
        {
            transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), closestPoint, SPEED * Time.fixedDeltaTime);
            CheckClosestPoint();
        }
        //In this status, Phoenix dives on the player in a parabolic path, allowing the latter to strike its head.
        else if (_status == PhoenixStatus.attack)
        {
            //At each frame, Phoenix must move a bit from its origin point.
            _attackXAxis += _flipEnemy.Orientation * Time.fixedDeltaTime;
            //Then, he must be set up at its relative point in the map.
            float a = Mathf.Abs(playerPosition.y / playerPosition.x / (playerPosition.x - _attackXAxisSecondPoint));
            float attackYaxis = (a * _attackXAxis * (_attackXAxis - _attackXAxisSecondPoint));
            float newHorizontalPosition = attackPosition.x + _attackXAxis;
            float newVerticalPosition = attackPosition.y + attackYaxis;
            transform.position = new Vector2(newHorizontalPosition, newHorizontalPosition);
            //transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), playerPosition, SPEED * Time.fixedDeltaTime);

            if (Vector2.Distance(transform.position, playerPosition) < 3)
            {
                closestPoint = currentPoint;
                while (closestPoint.Equals(currentPoint))
                {
                    int pointToFleeIndex = _rng.Next() % 4;
                    if (pointToFleeIndex == 0)
                    {
                        closestPoint = _northEastLimit;
                    }
                    else if (pointToFleeIndex == 1)
                    {
                        closestPoint = _southEastLimit;
                    }
                    else if (pointToFleeIndex == 2)
                    {
                        closestPoint = _southWestLimit;
                    }
                    else if (pointToFleeIndex == 3)
                    {
                        closestPoint = _northWestLimit;
                    }
                }
                _rigidbody.isKinematic = true;
                _status = PhoenixStatus.flee;
            }
        }

        if (_status == PhoenixStatus.fly || _status == PhoenixStatus.flee)
        {
            _flipEnemy.CheckPlayerPosition();
        }
	}

    private void CheckClosestPoint()
    {
        float closestPointDistance = Vector2.Distance(closestPoint, transform.position);
        if (closestPointDistance < 1)
        {
            currentPoint = closestPoint;
            _rigidbody.isKinematic = false;
            _status = PhoenixStatus.fly;
        }
    }
}
