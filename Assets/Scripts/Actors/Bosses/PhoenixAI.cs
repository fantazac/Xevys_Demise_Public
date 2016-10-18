using UnityEngine;
using System.Collections;

public class PhoenixAI : MonoBehaviour {

    public enum PhoenixStatus
    {
        fly,
        flee,
        charge,
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

    private Rigidbody2D _rigidbody;
    private Vector2 currentPoint;
    private Vector2 closestPoint;
    private Vector2 playerPosition;
    private Vector2 attackPosition;
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

    //In upcoming development, it would be wise to implement this variable and property into a Component.
    private bool _isFacingLeft;
    private int Orientation { get { return (_isFacingLeft ? -1 : 1); } }

    // Use this for initialization
    private void Start ()
    {
        _status = PhoenixStatus.fly;
        _flightTimeLeft = 0;
        _attackCooldownTimeLeft = 0;
        _rigidbody = GetComponent<Rigidbody2D>();
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
                _status = PhoenixStatus.charge;
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
        //In this status, Phoenix dives on the player, allowing the latter to strike its head.
        else if (_status == PhoenixStatus.charge)
        {
            int diveOrientation = (playerPosition.y < attackPosition.y ? 1 : -1);
            float newHorizontalPosition = transform.position.x + Orientation * Time.fixedDeltaTime;

            float x = Orientation * (newHorizontalPosition - attackPosition.x);
            float x2 = (playerPosition.x - attackPosition.x) * 2;
            float a = Mathf.Abs(playerPosition.y / playerPosition.x / (playerPosition.x - x2));

            float newVerticalPosition = (diveOrientation * a * newHorizontalPosition * (newHorizontalPosition - x2)) - attackPosition.y;
            transform.position = new Vector2(newHorizontalPosition, newHorizontalPosition);
            //Not a problem
            if (Orientation == 1 ^ playerPosition.x > transform.position.x)
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
            //This snippet of code can be eventually placed in a component as well; it is identical to Behemoth.
            if (GameObject.Find("Character").transform.position.x > transform.position.x)
            {
                if (_isFacingLeft)
                {
                    Flip();
                }
            }
            else if (GameObject.Find("Character").transform.position.x < transform.position.x)
            {
                if (!_isFacingLeft)
                {
                    Flip();
                }
            }
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

    //In upcoming development, it would be wise to implement this method into a Component.
    private void Flip()
    {
        _isFacingLeft = !_isFacingLeft;
        transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);
    }
}
