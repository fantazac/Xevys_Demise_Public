using UnityEngine;
using System.Collections;

public class VulcanAI : MonoBehaviour
{
    private enum VulcanStatus
    {
        LOWERED,
        RAISING,
        STANDING,
        RETREATING,
        DEAD,
    }

    private const float LOWERED_TIME = 2;
    private const float STANDING_TIME = 5;
    private const float HORIZONTAL_SPEED = 20;
    private const float VERTICAL_SPEED = 1650;

    [SerializeField]
    GameObject _fireball;

    private Health _health;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private FlipBoss _flipBoss;
    private OnBossDefeated _onBossDefeated;

    private System.Random _rng = new System.Random();
    private VulcanStatus _status;
    private bool _criticalStatus = true;
    private float[] _spawnPositions;
    private float _attackCooldownTimeLeft;
    private float _bodyHeight;
    private float _initialHeight;
    private float _timeLeft = LOWERED_TIME;
    private float _halfHealth;
    private bool _hasShotFireball = false;

    private void Start ()
    {
        _halfHealth = GetComponent<Health>().HealthPoint / 2;
        _status = VulcanStatus.LOWERED;
        _initialHeight = transform.position.y;
        _bodyHeight = transform.localScale.y;
        _spawnPositions = new float[5];
        for (int x  = -2; x < 3; x++)
        {
            _spawnPositions[x + 2] = transform.position.x + x * transform.localScale.x;
        }
        _health = GetComponent<Health>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _flipBoss = GetComponent<FlipBoss>();
        _animator = GetComponent<Animator>();
        _onBossDefeated = GetComponent<OnBossDefeated>();
        _onBossDefeated.onDefeated += OnVulcanDefeated;
    }

    private void OnDestroy()
    {
        _onBossDefeated.onDefeated -= OnVulcanDefeated;
    }

    private void FixedUpdate()
    {
        _flipBoss.CheckPlayerPosition();
        if (_status == VulcanStatus.LOWERED)
        {
            if (_timeLeft > 0)
            {
                _timeLeft -= Time.fixedDeltaTime;
            }
            else
            {
                _criticalStatus = true;// (_health.HealthPoint <= _halfHealth);
                if (_criticalStatus)
                {
                    if (Player.GetPlayer().transform.position.x < _spawnPositions[0] / 2)
                    {
                        transform.position = new Vector3(_spawnPositions[0], transform.position.y, transform.position.z);
                    }
                    else if (Player.GetPlayer().transform.position.x > _spawnPositions[4] / 2)
                    {
                        transform.position = new Vector3(_spawnPositions[4], transform.position.y, transform.position.z);
                    }
                    else
                    {
                        transform.position = new Vector3(_spawnPositions[2], transform.position.y, transform.position.z);
                    }
                }
                else
                {
                    int randomPosition = _rng.Next() % 2 * 2 + 1;
                    transform.position = new Vector3(_spawnPositions[randomPosition], transform.position.y, transform.position.z);
                }
                //_animator.SetInteger("State", 2);
                _rigidbody.isKinematic = false;
                _rigidbody.AddForce(transform.up * VERTICAL_SPEED);
                _timeLeft = STANDING_TIME;
                _status = VulcanStatus.RAISING;
            }
        }
        else if (_status == VulcanStatus.RAISING)
        {
            if (transform.position.y >= _initialHeight + _bodyHeight)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
                _rigidbody.isKinematic = true;
                _status = VulcanStatus.STANDING;
            }
        }
        else if (_status == VulcanStatus.STANDING)
        {
            if (_timeLeft > 0)
            {
                _timeLeft -= Time.fixedDeltaTime;
                if (_timeLeft < 2 && !_hasShotFireball)
                {
                    var fireball = Instantiate(_fireball, new Vector3(transform.position.x + _flipBoss.Orientation * 4.5f , transform.position.y + 1.7f + (_criticalStatus ? 0 : 1.8f)), Quaternion.identity);
                    if (!_criticalStatus)
                    {
                        ((GameObject)fireball).transform.Rotate(0, 0, _flipBoss.Orientation * 60);
                    }
                    ((GameObject)fireball).SetActive(true);
                    _hasShotFireball = true;
                }

            }
            else
            {
                _hasShotFireball = false;
                _rigidbody.isKinematic = false;
                _status = VulcanStatus.RETREATING;
            }
        }
        else if (_status == VulcanStatus.RETREATING)
        {
            //When Vulcan has only half his vitality left, he lowers towards the player.
            if (transform.position.y > _initialHeight)
            {
                if (_health.HealthPoint <= _halfHealth)
                {
                    _flipBoss.CheckPlayerPosition();
                    _rigidbody.AddForce(transform.right * _flipBoss.Orientation * HORIZONTAL_SPEED);
                }
            }
            else
            {
                //_health.HealthPoint -= 100;
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
                _timeLeft = LOWERED_TIME;
                _rigidbody.isKinematic = true;
                _status = VulcanStatus.LOWERED;
            }
        }
    }

    private void OnVulcanDefeated()
    {
        _status = VulcanStatus.DEAD;
        _animator.SetBool("IsDead", true);
        _rigidbody.isKinematic = false;
    }


}
