using UnityEngine;
using System.Collections;

public class MoveFireball : MonoBehaviour
{
    private const float HORIZONTAL_SPEED = 10;
    private const float VERTICAL_SPEED = 6;
    private const float VULCAN_RIGHT_PIT_INDEX = 3;

    private GameObject _vulcan;
    private BossOrientation _vulcanBossOrientation;
    private Rigidbody2D _rigidbody;
    private Vector3 _direction;

    bool _criticalStatus = false;

    private void Start()
    {
        _vulcan = GameObject.Find(StaticObjects.GetFindTags().Vulcan);
        _vulcanBossOrientation = _vulcan.GetComponent<BossOrientation>();
        if (transform.localEulerAngles.z == 0)
        {
            _criticalStatus = true;
        }
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.isKinematic = _criticalStatus;
        if (_criticalStatus)
        {
            _direction = new Vector3(_vulcanBossOrientation.Orientation * HORIZONTAL_SPEED * Time.fixedDeltaTime, 0, 0);
        }
        else
        {
            _rigidbody.velocity = new Vector2(_vulcanBossOrientation.Orientation * HORIZONTAL_SPEED,
                (_vulcan.GetComponent<VulcanAI>().CurrentIndex == VULCAN_RIGHT_PIT_INDEX ^
                _vulcanBossOrientation.IsFacingRight ? VERTICAL_SPEED : -VERTICAL_SPEED));
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
