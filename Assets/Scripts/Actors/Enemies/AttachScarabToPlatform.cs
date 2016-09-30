using UnityEngine;
using System.Collections;

public class AttachScarabToPlatform : MonoBehaviour
{

    [SerializeField]
    private GameObject _attachedWall;

    private const float MAX_ROTATION = 90;

    private Vector3 _wallPosition;
    private Vector3 _wallScale;

    private int _currentPoint;

    private bool _rotate = false;
    private float _rotateCount = 0;

    private Vector3 _target;
    private Vector3[] _points;

    private void Start()
    {
        if (_attachedWall != null)
        {
            _points = new Vector3[4];

            _wallPosition = _attachedWall.GetComponent<Transform>().position;
            _wallScale = _attachedWall.GetComponent<Transform>().localScale;

            _points[0] = new Vector2(_wallPosition.x - _wallScale.x / 2 - transform.localScale.x / 2, _wallPosition.y - _wallScale.y / 2 - transform.localScale.y / 2);
            _points[1] = new Vector2(_wallPosition.x + _wallScale.x / 2 + transform.localScale.x / 2, _wallPosition.y - _wallScale.y / 2 - transform.localScale.y / 2);
            _points[2] = new Vector2(_wallPosition.x + _wallScale.x / 2 + transform.localScale.x / 2, _wallPosition.y + _wallScale.y / 2 + transform.localScale.y / 2);
            _points[3] = new Vector2(_wallPosition.x - _wallScale.x / 2 - transform.localScale.x / 2, _wallPosition.y + _wallScale.y / 2 + transform.localScale.y / 2);

            _currentPoint = Random.Range(0, _points.Length - 1);

            _target = _points[_currentPoint];
            transform.position = _points[_currentPoint];
            FindTarget();
        }

    }

    private void Update()
    {
        if (_target != transform.position)
        {
            transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), _target, 1 * Time.deltaTime);
            if (Vector3.Distance(_target, transform.position) <= transform.localScale.x / 2)
                _rotate = true;
        }
        else
            FindTarget();

        if (_rotate)
            if (_rotateCount < MAX_ROTATION)
            {
                _rotateCount += 2;
                transform.Rotate(Vector3.forward * 2);
            }

            else
            {
                _rotate = false;
                _rotateCount = 0;
            }


    }

    private void FindTarget()
    {
        if (_target == transform.position)
            _target = _points[(++_currentPoint) % _points.Length];
    }

}
