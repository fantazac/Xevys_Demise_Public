using UnityEngine;

public class MoveFlame : MonoBehaviour
{
    [SerializeField]
    private float _speed = 0.15f;

    [SerializeField]
    private int _upwardDirectionAngle = 45;

    [SerializeField]
    private int _rightDirectionAngle = 135;

    [SerializeField]
    private int _downwardDirectionAngle = 225;

    [SerializeField]
    private int _leftDirectionAngle = 315;

    private Vector3 _direction;

    private void Start()
    {
        if (transform.eulerAngles.z <= _upwardDirectionAngle + 1 || transform.eulerAngles.z >= _leftDirectionAngle)
        {
            _direction = new Vector3(_direction.x, _speed);
        }
        else if (transform.eulerAngles.z >= _rightDirectionAngle && transform.eulerAngles.z <= _downwardDirectionAngle)
        {
            _direction = new Vector3(_direction.x, -_speed);
        }
        if (transform.eulerAngles.z >= _upwardDirectionAngle && transform.eulerAngles.z <= _rightDirectionAngle)
        {
            _direction = new Vector3(_speed, _direction.y);
        }
        else if (transform.eulerAngles.z >= _downwardDirectionAngle - 1 && transform.eulerAngles.z <= _leftDirectionAngle)
        {
            _direction = new Vector3(-_speed, _direction.y);
        }
    }

    private void FixedUpdate()
    {
        transform.position += _direction;
    }
}
