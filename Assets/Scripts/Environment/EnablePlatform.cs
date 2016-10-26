using UnityEngine;
using System.Collections;

public class EnablePlatform : MonoBehaviour
{

    [SerializeField]
    private bool _moveLeft = false;

    [SerializeField]
    private float _finalXPosition;

    private const float MOVE_AMOUNT = 5f;
    private const float MOVE_SPEED = 0.1f;

    private bool _move = false;
    private float _moveCount = 0;

    public bool Move { set { _move = value; } }

    private void Update()
    {
        if (_move)
        {
            if (_moveCount < MOVE_AMOUNT)
            {
                if (_moveLeft)
                {
                    transform.position = new Vector3(transform.position.x - MOVE_SPEED, transform.position.y, transform.position.z);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x + MOVE_SPEED, transform.position.y, transform.position.z);
                }
                _moveCount += MOVE_SPEED;
            }
            else
            {
                transform.position = new Vector3(_finalXPosition, transform.position.y, transform.position.z);
                _move = false;
            }
        }
    }

}
