using UnityEngine;
using System.Collections;

public class MoveFireball : MonoBehaviour
{
    private const float HORIZONTAL_SPEED = 10;
    private const float VERTICAL_SPEED = 6;
    private const float VULCAN_RIGHT_PIT_INDEX = 3;

    private GameObject _vulcan;
    private FlipBoss _vulcanFlipBoss;
    private Rigidbody2D _rigidbody;
    private Vector3 _direction;

    bool _criticalStatus = false;

    private void Start()
    {
        _vulcan = GameObject.Find("Vulcan");
        _vulcanFlipBoss = _vulcan.GetComponent<FlipBoss>();
        if (transform.localEulerAngles.z == 0)
        {
            _criticalStatus = true;
        }
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.isKinematic = _criticalStatus;
        if (_criticalStatus)
        {
            _direction = new Vector3(_vulcanFlipBoss.Orientation * HORIZONTAL_SPEED * Time.fixedDeltaTime, 0, 0);
        }
        else
        {
            _rigidbody.velocity = new Vector2(_vulcanFlipBoss.Orientation * HORIZONTAL_SPEED,
                (GameObject.Find("Vulcan").GetComponent<VulcanAI>().CurrentIndex == VULCAN_RIGHT_PIT_INDEX ^
                _vulcanFlipBoss.IsFacingLeft ? -VERTICAL_SPEED : VERTICAL_SPEED));
        }
    }

    private void FixedUpdate()
    {
        if (_criticalStatus)
        {
            transform.position += _direction;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
