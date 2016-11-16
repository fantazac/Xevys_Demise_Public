using UnityEngine;
using System.Collections;

public class ItemHover : MonoBehaviour
{
    [SerializeField]
    private float _speed = 0.11f;

    private const float WAVE_LENGTH = 0.08f;

    private float _sinCount = 0;

    private float _initialYPosition;

    private void Start()
    {
        _initialYPosition = transform.position.y;
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, _initialYPosition + WAVE_LENGTH * Mathf.Sin(_sinCount) * Time.deltaTime * 60, 0);
        _sinCount += _speed;
    }
}
