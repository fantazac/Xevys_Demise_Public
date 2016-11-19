using UnityEngine;

public class MoveFlame : MonoBehaviour
{
    private const float SPEED = 0.15f;

    private const int FIRST_ANGLE = 45;
    private const int SECOND_ANGLE = 135;
    private const int THIRD_ANGLE = 225;
    private const int FOURTH_ANGLE = 315;

    private Vector3 _direction;

    private void Start()
    {
        if (transform.eulerAngles.z <= FIRST_ANGLE + 1 || transform.eulerAngles.z >= FOURTH_ANGLE)
        {
            _direction = new Vector3(_direction.x, SPEED);
        }
        else if (transform.eulerAngles.z >= SECOND_ANGLE && transform.eulerAngles.z <= THIRD_ANGLE)
        {
            _direction = new Vector3(_direction.x, -SPEED);
        }
        if (transform.eulerAngles.z >= FIRST_ANGLE && transform.eulerAngles.z <= SECOND_ANGLE)
        {
            _direction = new Vector3(SPEED, _direction.y);
        }
        else if (transform.eulerAngles.z >= THIRD_ANGLE - 1 && transform.eulerAngles.z <= FOURTH_ANGLE)
        {
            _direction = new Vector3(-SPEED, _direction.y);
        }
    }

    private void FixedUpdate()
    {
        transform.position += _direction;
    }
}
