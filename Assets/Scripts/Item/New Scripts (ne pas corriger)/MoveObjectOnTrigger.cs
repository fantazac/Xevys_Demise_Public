using UnityEngine;
using System.Collections;

public class MoveObjectOnTrigger : MonoBehaviour
{
    private enum MoveDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    [SerializeField]
    private bool _isAnArtifactTrigger = false;

    [SerializeField]
    private GameObject _triggerActivationObject;

    [SerializeField]
    private MoveDirection _moveDirection;

    [SerializeField]
    private float _distanceToMoveObject;

    [SerializeField]
    private float _speedInUnitsPerSecond;

    private float _distanceMade = 0;

    private Vector3[] _directionalVectors;
    private Vector3 _directionalVector;
    private Vector3 _finalPosition;

    private ActivateTrigger _trigger;

    public delegate void OnFinishedMovingHandler();
    public event OnFinishedMovingHandler OnFinishedMoving;

    private void Start()
    {
       
        _trigger = _triggerActivationObject.GetComponent<ActivateTrigger>();
        _trigger.OnTrigger += StartObjectMovement;

        _directionalVectors = new Vector3[] { Vector3.up, Vector3.down, Vector3.left, Vector3.right };
        _directionalVector = _directionalVectors[(int)_moveDirection];
        _finalPosition = transform.position + (_directionalVector * _distanceToMoveObject);
    }

    private void StartObjectMovement()
    {
        StartCoroutine("MoveObject");
    }

    private IEnumerator MoveObject()
    {
        while (true)
        {
            gameObject.transform.position += _directionalVector * _speedInUnitsPerSecond * Time.deltaTime;
            _distanceMade += _speedInUnitsPerSecond * Time.deltaTime;

            if (_distanceMade >= _distanceToMoveObject)
            {
                gameObject.transform.position = _finalPosition;
                break;
            }

            yield return null;
        }
        //OnFinishedMoving();
        yield return null;
    }
}
