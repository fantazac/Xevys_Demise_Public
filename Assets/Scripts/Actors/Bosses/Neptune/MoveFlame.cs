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

    private void Start()
    {
        float mathematicalRotation = transform.eulerAngles.z;
        if (mathematicalRotation <= _upwardDirectionAngle + 1 || mathematicalRotation >= _leftDirectionAngle)
        {
            _direction = Vector2.up * _speed;
        }
        if (mathematicalRotation >= _rightDirectionAngle && mathematicalRotation <= _downwardDirectionAngle)
        {
            _direction = Vector2.down * _speed;
        }
        if (mathematicalRotation >= _upwardDirectionAngle && mathematicalRotation <= _rightDirectionAngle)
        {
            _direction = new Vector3(_speed, _direction.y);
        }
        if (mathematicalRotation >= _downwardDirectionAngle - 1 && mathematicalRotation <= _leftDirectionAngle)
        {
            _direction = new Vector3(-_speed, _direction.y);
        }
        transform.rotation = Quaternion.identity;
        transform.Rotate(0, 0, -mathematicalRotation);
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

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
