using UnityEngine;
using System.Collections;

public class MoveFlame : MonoBehaviour
{
    private const float SPEED = 0.15f;

    private Vector3 _direction;

    private void Start()
    {
        if (transform.eulerAngles.z == 0)
        {
            _direction = new Vector3(0, SPEED, transform.position.z);
        }
        else if (transform.eulerAngles.z <= 89)
        {
            _direction = new Vector3(SPEED, SPEED, transform.position.z);
        }
        else if (transform.eulerAngles.z <= 90)
        {
            _direction = new Vector3(SPEED, 0, transform.position.z);
        }
        else if (transform.eulerAngles.z <= 135)
        {
            _direction = new Vector3(SPEED, -SPEED, transform.position.z);
        }
        else if (transform.eulerAngles.z <= 180)
        {
            _direction = new Vector3(0, -SPEED, transform.position.z);
        }
        else if (transform.eulerAngles.z <= 225)
        {
            _direction = new Vector3(-SPEED, -SPEED, transform.position.z);
        }
        else if (transform.eulerAngles.z <= 270)
        {
            _direction = new Vector3(-SPEED, 0, transform.position.z);
        }
        else
        {
            _direction = new Vector3(-SPEED, SPEED, transform.position.z);
        }
    }

    private void FixedUpdate()
    {
        transform.position += _direction;
    }
}
