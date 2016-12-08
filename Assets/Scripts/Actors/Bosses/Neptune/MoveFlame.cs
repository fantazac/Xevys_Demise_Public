using System.Collections;
using UnityEngine;

public class MoveFlame : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5;

    [SerializeField]
    private int _upwardDirectionAngle = 45;

    [SerializeField]
    private int _rightDirectionAngle = 135;

    [SerializeField]
    private int _downwardDirectionAngle = 225;

    [SerializeField]
    private int _leftDirectionAngle = 315;

    private Vector3 _direction;
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        if (transform.eulerAngles.z <= _upwardDirectionAngle + 1 || transform.eulerAngles.z >= _leftDirectionAngle)
        {
            _direction = Vector2.up * _speed;
        }
        if (transform.eulerAngles.z >= _rightDirectionAngle && transform.eulerAngles.z <= _downwardDirectionAngle)
        {
            _direction = Vector2.down * _speed;
        }
        if (transform.eulerAngles.z >= _upwardDirectionAngle && transform.eulerAngles.z <= _rightDirectionAngle)
        {
            _direction = new Vector3(_speed, _direction.y);
        }
        if (transform.eulerAngles.z >= _downwardDirectionAngle - 1 && transform.eulerAngles.z <= _leftDirectionAngle)
        {
            _direction = new Vector3(-_speed, _direction.y);
        }
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (true)
        {
            yield return null;
            transform.position += _direction * Time.deltaTime;
        }
    }
}
