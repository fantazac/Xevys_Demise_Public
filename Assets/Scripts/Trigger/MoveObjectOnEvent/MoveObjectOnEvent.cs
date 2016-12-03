using UnityEngine;
using System.Collections;

/* BEN_CORRECTION
 * 
 * Cette classe devrait être abstraite, car c'est la base du pattern "Template Method".
 * 
 * Voir https://en.wikipedia.org/wiki/Template_method_pattern#Example_in_Java
 */
public class MoveObjectOnEvent : MonoBehaviour
{
    [SerializeField]
    protected MoveDirection _moveDirection;

    [SerializeField]
    protected float _distanceToMoveObject;

    [SerializeField]
    protected float _speedInUnitsPerSecond;

    protected float _distanceMade = 0;

    protected Vector3[] _directionalVectors;
    protected Vector3 _directionalVector;
    protected Vector3 _finalPosition;

    public delegate void OnFinishedMovingHandler();
    public event OnFinishedMovingHandler OnFinishedMoving;

    protected virtual void Start()
    {
        _directionalVectors = new Vector3[] { Vector3.up, Vector3.down, Vector3.left, Vector3.right };
        _directionalVector = _directionalVectors[(int)_moveDirection];
    }

    protected void StartObjectMovement()
    {
        _finalPosition = transform.position + (_directionalVector * _distanceToMoveObject);
        StartCoroutine(MoveObject());
    }

    protected IEnumerator MoveObject()
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
        _distanceMade = 0;
        if (OnFinishedMoving != null)
        {
            OnFinishedMoving();
        }
    }
}
