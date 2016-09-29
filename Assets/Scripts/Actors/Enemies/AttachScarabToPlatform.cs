using UnityEngine;
using System.Collections;

public class AttachScarabToPlatform : MonoBehaviour
{

    [SerializeField]
    private GameObject _attachedWall;

    private Vector3 _wallPosition;
    private Vector3 _wallScale;

    private int currentPoint = 0;

    private bool _rotate = false;

    private Vector2 _target;
    private Vector2[] _points;

    private void Start()
    {
        if(_attachedWall != null)
        {
            _points = new Vector2[4];

            _wallPosition = _attachedWall.GetComponent<Transform>().position;
            _wallScale = _attachedWall.GetComponent<Transform>().localScale;

            _points[0] = new Vector2(_wallPosition.x - _wallScale.x / 2, _wallPosition.y - _wallScale.y);
            _points[1] = new Vector2(_wallPosition.x + _wallScale.x / 2, _wallPosition.y - _wallScale.y);
            _points[2] = new Vector2(_wallPosition.x + _wallScale.x / 2, _wallPosition.y + _wallScale.y);
            _points[3] = new Vector2(_wallPosition.x - _wallScale.x / 2, _wallPosition.y + _wallScale.y);

            FindTarget();
        }
        
    }

    private void Update()
    {
        
    }

    private void FindTarget()
    {
        //if()
    }

}
