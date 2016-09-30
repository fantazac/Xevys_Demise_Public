using UnityEngine;
using System.Collections;

public class MoveBatInDelimitedArea : MonoBehaviour {

    private const int SPEED = 2;

    private bool _isPlayerUnderneath;
    private bool _isGoingDown;
    private float _leftLimit;
    private float _rightLimit;
    private Vector2 _newHangingPoint;
    private System.Random _rng = new System.Random();
    private Transform childObject;

    public bool IsPlayerUnderneath
    {
        get
        {
            return _isPlayerUnderneath;
        }
        set
        {
            _isPlayerUnderneath = value;
        }
    }
	// Use this for initialization
	void Start () {
        _newHangingPoint = transform.position;
        _leftLimit = transform.position.x - 2;
        _rightLimit = transform.position.x + 2;
        _isPlayerUnderneath = false;
        _isGoingDown = true;
        childObject = transform.Find("GameObject");
	}
	
	// Update is called once per frame
	void Update () {
	    if (_isPlayerUnderneath)
        {
            if (_isGoingDown)
            {
                GetComponent<SpriteRenderer>().color = Color.red;
                transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), new Vector2(transform.position.x, transform.position.y - 1000), SPEED * Time.deltaTime);
                childObject.transform.position = Vector2.MoveTowards(new Vector2(childObject.transform.position.x, childObject.transform.position.y), new Vector2(childObject.transform.position.x, childObject.transform.position.y - 1000), SPEED * Time.deltaTime);
            }
            else
            {
                GetComponent<SpriteRenderer>().color = Color.blue;
                transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), new Vector2(_newHangingPoint.x, _newHangingPoint.y), SPEED * Time.deltaTime);
                childObject.transform.position = Vector2.MoveTowards(new Vector2(childObject.transform.position.x, childObject.transform.position.y), new Vector2(_newHangingPoint.x, _newHangingPoint.y), SPEED * Time.deltaTime);
                if (transform.position.y == _newHangingPoint.y)
                {
                    _isPlayerUnderneath = false;
                }
            }
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.green;
        }
	}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        _isGoingDown = false;
        _newHangingPoint = new Vector2(_rng.Next((int)_leftLimit, (int)_rightLimit), _newHangingPoint.y);
    }
}
