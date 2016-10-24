using UnityEngine;
using System.Collections;

public class BehemothAI : MonoBehaviour
{
    /* BEN_REVIEW
     * 
     * Le nom des valeurs de l'enum doivent être en UPPER_CASE_CASING.
     */
    private enum BehemothStatus
    {
        wait,
        charge,
        struck,
        stun,
        dead,
    }

    [SerializeField]
    private GameObject _leftWall;

    [SerializeField]
    private GameObject _rightWall;

    [SerializeField]
    private float _speed = 25;

    private const int STUN_TIME = 3;
    private const float FEIGN_TIME = 0.33f;
    private const int CHARGE_TIME = 5;

    private GameObject _aimedWall;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private FlipBoss _flipBoss;
    //These components should eventually be placed into a script for all bosses (think heritage) as they are only used for a death status.
    private Health _health;
    private BoxCollider2D _boxCollider;

    private System.Random _rng = new System.Random();
    private BehemothStatus _status = BehemothStatus.wait;
    private float _timeLeft = CHARGE_TIME;
    private bool _isCharging;
    //Death status also include this boolean.
    private bool _isDead;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _flipBoss = GetComponent<FlipBoss>();
        _animator = GetComponent<Animator>();
        _health = GetComponent<Health>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    /* BEN_REVIEW
     * 
     * À découper en méthode. Probablement une pour chaque état.
     */
    private void Update()
    {
        if (!_isDead && _health.HealthPoint <= 0)
        {
            _isDead = true;
            _animator.SetBool("IsDead", true);
            Destroy(_rigidbody);
            _boxCollider.enabled = false;
        }
        else
        {
            //Wait allows Behemoth to face the player and prepare to charge.
            if (_status == BehemothStatus.wait)
            {
                _flipBoss.CheckPlayerPosition();
                if (_timeLeft > 0)
                {
                    _timeLeft -= Time.fixedDeltaTime;
                    if (_timeLeft < 2)
                    {
                        _animator.SetInteger("State", 1);
                    }
                }
                //Upon countdown expired, Behemoth either feigns charging or charges.
                //The time charging depends from seconds specified in FEIGN_TIME or this amount of time plus the amount specified in CHARGE_TIME;
                else
                {
                    _animator.SetInteger("State", 2);
                    _isCharging = (_rng.Next() % 2 == 0 ? true : false);
                    _timeLeft = FEIGN_TIME + (_isCharging ? CHARGE_TIME : 0);
                    _status = BehemothStatus.charge;
                    _aimedWall = (_flipBoss.IsFacingRight ? _leftWall : _rightWall);
                }
            }
            //Charge status makes Behemoth aims for the wall for the amount of time in seconds decided above.
            //Notice that Behemoth feigning really close to the wall makes him directly crash into it instead.
            else if (_status == BehemothStatus.charge)
            {
                _timeLeft -= Time.fixedDeltaTime;
                if (_timeLeft > 0)
                {
                    _rigidbody.velocity = new Vector2(_speed * _flipBoss.Orientation, _rigidbody.velocity.y);
                    if (_flipBoss.IsFacingRight ?
                        _aimedWall.transform.position.x + _aimedWall.GetComponent<SpriteRenderer>().bounds.size.x / 2 >= transform.position.x - GetComponent<SpriteRenderer>().bounds.size.x / 2 :
                        _aimedWall.transform.position.x - _aimedWall.GetComponent<SpriteRenderer>().bounds.size.x / 2 <= transform.position.x + GetComponent<SpriteRenderer>().bounds.size.x / 2)
                    {
                        _timeLeft = 1;
                        _animator.SetInteger("State", 3);
                        _status = BehemothStatus.struck;
                    }
                }
                else
                {
                    SetWaitStatus();
                }
            }
            //Stuck is a status of one second during which Behemoth backs off and then goes into the Stun status.
            else if (_status == BehemothStatus.struck)
            {
                if (_timeLeft > 0)
                {
                    _timeLeft -= Time.fixedDeltaTime;
                    _rigidbody.velocity = new Vector2(-_speed / 10 * _flipBoss.Orientation, _rigidbody.velocity.y);
                }
                else
                {
                    _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
                    _timeLeft = STUN_TIME;
                    _animator.SetInteger("State", 4);
                    _status = BehemothStatus.stun;
                }
            }
            //Stun status allows the player to attack Behemoth for the amount of seconds specified in STUN_TIME.
            else if (_status == BehemothStatus.stun)
            {
                _timeLeft -= Time.fixedDeltaTime;
                if (_timeLeft <= 0)
                {
                    SetWaitStatus();
                }
            }
        }
    }

    private void SetWaitStatus()
    {
        _animator.SetInteger("State", 0);
        _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
        _status = BehemothStatus.wait;
        _timeLeft = _rng.Next(5, 10);
    }
}
