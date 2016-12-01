using UnityEngine;
using System.Collections;

public class InitializeAxe : MonoBehaviour
{
    [SerializeField]
    private float _initialHeight = 1f;

    [SerializeField]
    private float _horizontalSpeed = 6f;

    [SerializeField]
    private float _verticalSpeed = 14.5f;

    [SerializeField]
    private float _initialRotation = 90f;

    [SerializeField]
    private float _waterSpeedModifier = 0.48f;

    private int _flipAxe = 0;

    private const float NORMAL_GRAVITY = 3f;
    private const float WATER_GRAVITY = 2f;

    private Rigidbody2D _rigidbody;
    private AxeWaterInteraction _waterInteraction;
    private bool _isInWater = false;

    private void Start()
    {
        _waterInteraction = GetComponent<AxeWaterInteraction>();
        _waterInteraction.OnEnterWater += EnterWater;
        _waterInteraction.OnExitWater += ExitWater;

        _rigidbody = GetComponent<Rigidbody2D>();
        _flipAxe = StaticObjects.GetPlayer().GetComponent<ActorOrientation>().Orientation;
        transform.position = new Vector2(transform.position.x, transform.position.y + _initialHeight);
        transform.eulerAngles = new Vector3(0, 0, _initialRotation);
        GetComponent<Rigidbody2D>().velocity += (Vector2.right * _horizontalSpeed * _flipAxe) + (Vector2.up * _verticalSpeed);
        if (_isInWater)
        {
            GetComponent<Rigidbody2D>().velocity *= _waterSpeedModifier;
        }
        transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y * _flipAxe);
    }

    private void EnterWater()
    {
        _isInWater = true;
        if(_rigidbody.gravityScale != WATER_GRAVITY)
        {
            _rigidbody.gravityScale = WATER_GRAVITY;
            _rigidbody.velocity *= _waterSpeedModifier;
        }
    }

    private void ExitWater()
    {
        _isInWater = false;
        if(_rigidbody.gravityScale != NORMAL_GRAVITY)
        {
            _rigidbody.gravityScale = NORMAL_GRAVITY;
        }      
    }
}
