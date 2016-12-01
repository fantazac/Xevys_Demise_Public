using UnityEngine;
using System.Collections;

public class InitializeAxe : MonoBehaviour
{
    [SerializeField]
    private float _initialHeight = 1f;

    [SerializeField]
    private float _horinzontalSpeed = 6f;

    [SerializeField]
    private float _verticalSpeed = 14.5f;

    [SerializeField]
    private float _initialRotation = 90f;

    private int _flipAxe = 0;

    private void Start()
    {
        _flipAxe = StaticObjects.GetPlayer().GetComponent<ActorOrientation>().Orientation;
        transform.position = new Vector2(transform.position.x, transform.position.y + _initialHeight);
        transform.eulerAngles = new Vector3(0, 0, _initialRotation);
        GetComponent<Rigidbody2D>().velocity = new Vector2(_horinzontalSpeed * _flipAxe, _verticalSpeed);
        transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y * _flipAxe);
    }
}
