using UnityEngine;
using System.Collections;

public class ItemHover : MonoBehaviour
{

    private const float SPEED = 0.11f;
    private const float WAVE_LENGTH = 0.08f;

    private float _sinCount = 0;


    private float _initialYPosition;

    private void Start()
    {
        _initialYPosition = transform.position.y;
    }

    private void Update()
    {
        transform.position = new Vector2(transform.position.x, _initialYPosition + WAVE_LENGTH * Mathf.Sin(_sinCount));
        _sinCount += SPEED;
    }
}
