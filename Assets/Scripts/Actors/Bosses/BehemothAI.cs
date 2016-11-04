﻿using UnityEngine;
using System.Collections;

public class BehemothAI : MonoBehaviour
{
    private enum BehemothStatus
    {
        WAIT,
        CHARGE,
        STRUCK,
        STUN,
        DEAD,
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
    private OnBossDefeated _onBossDefeated;

    /* BEN_CORRECTION
     * 
     * « _random » peut-être ? Ne faites pas d'abbréviations sauf si l'abbréviation elle même est
     * plus utilisée que les mots qu'elle représente (tel que HTML, XML, http, etc...).
     */
    private System.Random _rng = new System.Random();
    private BehemothStatus _status = BehemothStatus.WAIT;
    private float _timeLeft = CHARGE_TIME;
    private bool _isCharging;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _flipBoss = GetComponent<FlipBoss>();
        _animator = GetComponent<Animator>();
        _onBossDefeated = GetComponent<OnBossDefeated>();
        _onBossDefeated.onDefeated += OnBehemothDefeated;
    }

    private void OnDestroy()
    {
        _onBossDefeated.onDefeated -= OnBehemothDefeated;
    }

    private void Update()
    {
        //Wait allows Behemoth to face the player and prepare to charge.
        if (_status == BehemothStatus.WAIT)
        {
            WaitUpdate();
        }
        //Charge status makes Behemoth aims for the wall for the amount of time in seconds decided above.
        //Notice that Behemoth feigning really close to the wall makes him directly crash into it instead.
        else if (_status == BehemothStatus.CHARGE)
        {
            ChargeUpdate();
        }
        //Stuck is a status of one second during which Behemoth backs off and then goes into the Stun status.
        else if (_status == BehemothStatus.STRUCK)
        {
            StruckUpdate();
        }
        //Stun status allows the player to attack Behemoth for the amount of seconds specified in STUN_TIME.
        else if (_status == BehemothStatus.STUN)
        {
            StunUpdate();
        }
    }

    private void WaitUpdate()
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
            _status = BehemothStatus.CHARGE;
            _aimedWall = (_flipBoss.IsFacingLeft ? _leftWall : _rightWall);
        }
    }

    private void ChargeUpdate()
    {
        _timeLeft -= Time.fixedDeltaTime;
        if (_timeLeft > 0)
        {
            _rigidbody.velocity = new Vector2(_speed * _flipBoss.Orientation, _rigidbody.velocity.y);
            if (_flipBoss.IsFacingLeft ?
                _aimedWall.transform.position.x + _aimedWall.GetComponent<SpriteRenderer>().bounds.size.x / 2 >= transform.position.x - GetComponent<SpriteRenderer>().bounds.size.x / 2 :
                _aimedWall.transform.position.x - _aimedWall.GetComponent<SpriteRenderer>().bounds.size.x / 2 <= transform.position.x + GetComponent<SpriteRenderer>().bounds.size.x / 2)
            {
                _timeLeft = 1;
                _animator.SetInteger("State", 3);
                _status = BehemothStatus.STRUCK;
            }
        }
        else
        {
            SetWaitStatus();
        }
    }

    private void StruckUpdate()
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
            _status = BehemothStatus.STUN;
        }
    }

    /* BEN_CORRECTION
     * 
     * Update ? Ça dit pas grand chose. Rend ça plus clair, quitte à ce que le nom de la méthode soit
     * un peu plus long.
     * 
     * On est pas en 1990 où le nombre de caractère d'une ligne de code était limité à 80 et le nom des
     * variables ne dépassait pas 8 caractères!
     */
    private void StunUpdate()
    {
        _timeLeft -= Time.fixedDeltaTime;
        if (_timeLeft <= 0)
        {
            SetWaitStatus();
        }
    }

    private void SetWaitStatus()
    {
        _animator.SetInteger("State", 0);
        _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
        _status = BehemothStatus.WAIT;
        _timeLeft = _rng.Next(5, 10);
    }

    public void OnBehemothDefeated()
    {
        _status = BehemothStatus.DEAD;
        _animator.SetBool("IsDead", true);
        Destroy(_rigidbody);
    }
}
