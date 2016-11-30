using UnityEngine;
using System.Collections;

public class MoveFireball : MonoBehaviour
{
    [SerializeField]
    private float _horizontalSpeed = 10;

    [SerializeField]
    private float _verticalSpeed = 6;

    [SerializeField]
    private float _vulcanRightPitIndex = 3;

    public GameObject Vulcan { get; set; }

    private BossOrientation _vulcanBossOrientation;
    private Rigidbody2D _rigidbody;
    private Vector3 _direction;

    private bool _criticalStatus = false;

    private void Start()
    {
        _vulcanBossOrientation = Vulcan.GetComponent<BossOrientation>();
        _rigidbody = GetComponent<Rigidbody2D>();
        if (transform.localEulerAngles.z == 0)
        {
            _criticalStatus = true;
        }
        _rigidbody.isKinematic = _criticalStatus;
        if (_criticalStatus)
        {
            _direction = new Vector3(_vulcanBossOrientation.Orientation * _horizontalSpeed * Time.fixedDeltaTime, 0, 0);
        }
        else
        {
            _rigidbody.velocity = new Vector2(_vulcanBossOrientation.Orientation * _horizontalSpeed,
                ((Vulcan.GetComponent<VulcanAI>().CurrentIndex == _vulcanRightPitIndex ? -1 : 1) *
                _vulcanBossOrientation.Orientation * _verticalSpeed));
        }
    }

    private void FixedUpdate()
    {
        if (_criticalStatus)
        {
            transform.position += _direction;
        }
    }
}
