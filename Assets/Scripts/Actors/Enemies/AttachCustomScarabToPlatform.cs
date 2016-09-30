using UnityEngine;
using System.Collections;

public class AttachCustomScarabToPlatform : MonoBehaviour
{

    [SerializeField]
    private Vector3[] _points;

    [SerializeField]
    private bool allowBacktracking = false;

    private bool goesBackwards = false;

    private int _currentPoint;

    private Vector3 _target;


    private void Start()
    {
        if (_points.Length > 0)
        {
            _currentPoint = Random.Range(0, _points.Length - 1);

            _target = _points[_currentPoint];
            transform.position = _points[_currentPoint];
            FindTarget();
        }

    }

    private void Update()
    {
        if (_target != transform.position)
            transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), _target, 1 * Time.deltaTime);
        else
            FindTarget();
    }

    private void FindTarget()
    {
        if (_target == transform.position)
            if (allowBacktracking)
                if (goesBackwards)
                {
                    _target = _points[--_currentPoint];
                    if (_currentPoint == 0)
                        goesBackwards = false;
                }
                else
                {
                    _target = _points[++_currentPoint];
                    if (_currentPoint == _points.Length - 1)
                        goesBackwards = true;
                }
            else
                _target = _points[(++_currentPoint) % _points.Length];
    }
}
