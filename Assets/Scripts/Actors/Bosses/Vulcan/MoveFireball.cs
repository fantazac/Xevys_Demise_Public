using UnityEngine;
using System.Collections;

public class MoveFireball : MonoBehaviour
{
    private const float HORIZONTAL_SPEED = 100f;
    private const float VERTICAL_SPEED = 100f;

    private Rigidbody2D _rigidbody;
    private Vector3 _direction;

    bool _criticalStatus = false;

	private void Start ()
    {
        if (transform.localEulerAngles.z == 0)
        {
            _criticalStatus = true;
        }
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.isKinematic = _criticalStatus;
        if (_criticalStatus)
        {
            _direction = new Vector3(GameObject.Find("Vulcan").GetComponent<FlipBoss>().Orientation * HORIZONTAL_SPEED/100, 0, 0);
        }
    }
	
	private void FixedUpdate ()
    {
        if (_criticalStatus)
        {
            transform.position += _direction;
        }
        else
        {
            _rigidbody.AddForce(transform.right * GetComponent<FlipBoss>().Orientation * HORIZONTAL_SPEED);
            _rigidbody.AddForce(transform.up * VERTICAL_SPEED);
        }
	}

    private void 
}
