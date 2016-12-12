using System.Collections;
using UnityEngine;

public class VulcanAI : MonoBehaviour
{
    [SerializeField]
    GameObject _fireball;

    [SerializeField]
    private float _loweredTime = 2;

    [SerializeField]
    private float _standingTime = 5;

    [SerializeField]
    private float _horizontalSpeed = 20;

    [SerializeField]
    private float _raisingUpwardForce = 1650;

    [SerializeField]
    private GameObject[] _excludedObjets;

    private Health _health;
    private Rigidbody2D _rigidbody;
    private GameObject _vulcanSkull;
    private BossOrientation _bossOrientation;
    private DisableCollidersOnBossDefeated _onBossDefeated;
    private PolygonCollider2D _polygonHitbox;

    private System.Random _random = new System.Random();
    private bool _criticalStatus = true;
    private float[] _positionsForRaise;
    private float _attackCooldownTimeLeft;
    private float _bodyHeight;
    private float _initialHeight;
    private float _standingTimeLeft;
    private float _halfHealth;
    private bool _hasShotFireball = false;

    private WaitForSeconds _delayLowered;

    public int CurrentIndex { get; private set; }

    private void Start()
    {
        _delayLowered = new WaitForSeconds(_loweredTime);

        _health = GetComponent<Health>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _bossOrientation = GetComponent<BossOrientation>();
        _health.OnDeath += OnVulcanDefeated;
        _polygonHitbox = GetComponent<PolygonCollider2D>();
        _vulcanSkull = transform.FindChild("Vulcan Skull").gameObject;
        InitializeVulcan();

        SetLoweredStatus();
    }

    private void InitializeVulcan()
    {
        _health.HealthPoint = _health.MaxHealth;
        _halfHealth = _health.HealthPoint / 2;
        _polygonHitbox.enabled = false;
        _initialHeight = transform.position.y;
        _bodyHeight = transform.localScale.y;
        _positionsForRaise = new float[5];
        for (int x = -2; x < 3; x++)
        {
            _positionsForRaise[x + 2] = transform.position.x + x * transform.localScale.x;
        }
    }

    private void SelectPointClosestToPlayer()
    {
        float horizontalDistanceToClosestPoint = float.MaxValue;
        int closestPointIndex = -1;
        for (int n = 0; n < _positionsForRaise.Length; n += 2)
        {
            float horizontalDistanceToSpecificPoint = Mathf.Abs(StaticObjects.GetPlayer().transform.position.x - _positionsForRaise[n]);
            if (horizontalDistanceToClosestPoint > horizontalDistanceToSpecificPoint)
            {
                horizontalDistanceToClosestPoint = horizontalDistanceToSpecificPoint;
                closestPointIndex = n;
            }
        }
        transform.position = new Vector3(_positionsForRaise[closestPointIndex], transform.position.y);
    }

    private IEnumerator UpdateWhenLowered()
    {
        yield return _delayLowered;

        SetRaisingStatus();
    }

    private IEnumerator UpdateWhenRaising()
    {
        while (transform.position.y < _initialHeight + _bodyHeight)
        {
            yield return null;
        }
        SetStandingStatus();
    }
    private IEnumerator UpdateWhenStanding()
    {
        while (_standingTimeLeft > 0)
        {
            _bossOrientation.FlipTowardsPlayer();
            if (_standingTimeLeft < 2 && !_hasShotFireball)
            {
                GameObject fireball = (GameObject)Instantiate(_fireball, new Vector3(transform.position.x + _bossOrientation.Orientation * 4.5f,
                    transform.position.y + 1.7f + (_criticalStatus ? 0 : 1.8f)), Quaternion.identity);
                fireball.GetComponent<DestroyBossProjectile>().ExcludedObjects = _excludedObjets;
                if (!_criticalStatus)
                {
                    fireball.transform.Rotate(0, 0, 60 * _bossOrientation.Orientation);
                    fireball.GetComponent<SpriteRenderer>().flipX = !_bossOrientation.IsFacingRight;
                }
                fireball.GetComponent<MoveFireball>().Vulcan = gameObject;
                fireball.SetActive(true);
                _hasShotFireball = true;
            }
            _standingTimeLeft -= Time.deltaTime;

            yield return null;
        }

        SetRetreatingStatus();
    }

    private IEnumerator UpdateWhenRetreating()
    {
        while (transform.position.y > _initialHeight)
        {
            if (_health.HealthPoint <= _halfHealth)
            {
                _bossOrientation.FlipTowardsPlayer();
                _rigidbody.AddForce(transform.right * _bossOrientation.Orientation * _horizontalSpeed);
            }

            yield return null;
        }
        SetLoweredStatus();
    }

    private void SetRaisingStatus()
    {
        StopAllCoroutines();
        _criticalStatus = (_health.HealthPoint <= _halfHealth);
        if (_criticalStatus)
        {
            SelectPointClosestToPlayer();
        }
        else
        {
            CurrentIndex = _random.Next() % 2 * 2 + 1;
            transform.position = new Vector3(_positionsForRaise[CurrentIndex], transform.position.y, transform.position.z);
        }
        _bossOrientation.FlipTowardsPlayer();
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(transform.up * _raisingUpwardForce);
        _standingTimeLeft = _standingTime;
        StartCoroutine(UpdateWhenRaising());
    }

    private void SetStandingStatus()
    {
        StopAllCoroutines();
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.isKinematic = true;
        StartCoroutine(UpdateWhenStanding());
    }

    private void SetRetreatingStatus()
    {
        StopAllCoroutines();
        _vulcanSkull.SetActive(true);
        _hasShotFireball = false;
        _rigidbody.isKinematic = false;
        _polygonHitbox.enabled = true;
        StartCoroutine(UpdateWhenRetreating());
    }

    private void SetLoweredStatus()
    {
        StopAllCoroutines();
        _rigidbody.velocity = Vector2.zero;
        _vulcanSkull.SetActive(false);
        _rigidbody.isKinematic = true;
        _polygonHitbox.enabled = false;
        StartCoroutine(UpdateWhenLowered());
    }

    private void OnVulcanDefeated()
    {
        _vulcanSkull.SetActive(false);
        _rigidbody.isKinematic = false;
        StopAllCoroutines();
    }
}
