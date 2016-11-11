using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class XevyMovement : MonoBehaviour
{
    //delet this
    private float timer = 1.0f;
    private float timerCooldown = 1.0f;
    //delet this

    public enum XevyMovementStatus
    {
        NONE,
        WALKING,
        BOUNCING,
        FLEEING,
    }
    private const float HORIZONTAL_DISTANCE_TO_REACH_PLATFORM = 6.5f;
    private const float VERTICAL_DISTANCE_TO_REACH_PLATFORM = 4f;
    private const float BOUNCE_SPEED = 8;

    private FlipBoss _flipBoss;
    private Vector2 _arrivalPosition;
    private Vector2 _startPosition;
    private Vector2[] _platformJumpPositions;
    private List<int>[] _pseudoTree;
    System.Random _rng = new System.Random();

    public XevyMovementStatus MovementStatus { get; private set; }
    private bool _isGoingUp = true;
    private float _bounceApex;
    private int _currentPositionIndex = 3;
    private int _arrivalPositionIndex;
    private float deltaX;
    private float deltaY;

    private void Start()
    {
        MovementStatus = XevyMovementStatus.NONE;
        _flipBoss = GetComponent<FlipBoss>();
        _platformJumpPositions = new Vector2[]
        {
            new Vector2(transform.position.x, transform.position.y + 2 * VERTICAL_DISTANCE_TO_REACH_PLATFORM),
            new Vector2(transform.position.x - 2 * HORIZONTAL_DISTANCE_TO_REACH_PLATFORM, transform.position.y),
            new Vector2(transform.position.x - HORIZONTAL_DISTANCE_TO_REACH_PLATFORM, transform.position.y + VERTICAL_DISTANCE_TO_REACH_PLATFORM),
            new Vector2(transform.position.x, transform.position.y),
            new Vector2(transform.position.x + HORIZONTAL_DISTANCE_TO_REACH_PLATFORM, transform.position.y + VERTICAL_DISTANCE_TO_REACH_PLATFORM),
            new Vector2(transform.position.x + 2 * HORIZONTAL_DISTANCE_TO_REACH_PLATFORM, transform.position.y),
        };
        _pseudoTree = new List<int>[6];
        for (int n = 0; n < _pseudoTree.Length; n++)
        {
            _pseudoTree[n] = new List<int>();
        }
        _pseudoTree[0].Add(2);
        _pseudoTree[0].Add(4);
        _pseudoTree[1].Add(2);
        _pseudoTree[2].Add(0);
        _pseudoTree[2].Add(1);
        _pseudoTree[2].Add(3);
        _pseudoTree[3].Add(2);
        _pseudoTree[3].Add(4);
        _pseudoTree[4].Add(0);
        _pseudoTree[4].Add(3);
        _pseudoTree[4].Add(5);
        _pseudoTree[5].Add(4);
        _startPosition = _platformJumpPositions[_currentPositionIndex];
    }

    public void MovementUpdate()
    {
        if (MovementStatus == XevyMovementStatus.BOUNCING)
        {
            JumpOntoDiffentFloor();
            if (CheckIfBounceCompleted())
            {
                _currentPositionIndex = _arrivalPositionIndex;
                _startPosition = _arrivalPosition;
                MovementStatus = XevyMovementStatus.NONE;
            }
        }
    }

    public void Flee()
    {

    }

    public void Bounce()
    {
        SelectRandomNode();
        MovementStatus = XevyMovementStatus.BOUNCING;
    }

    public void StepBack()
    {

    }

    private void SelectRandomNode()
    {
        int listNodesCount = _pseudoTree[_currentPositionIndex].Count;
        _arrivalPositionIndex = _pseudoTree[_currentPositionIndex][(listNodesCount == 1 ? 0 : _rng.Next() % listNodesCount)];
        _arrivalPosition = _platformJumpPositions[_arrivalPositionIndex];
        _isGoingUp = (_startPosition.y < _arrivalPosition.y);
        _flipBoss.FlipTowardsSpecificPoint(_arrivalPosition);
        deltaX = _arrivalPosition.x - _startPosition.x;
        deltaY = _arrivalPosition.y - _startPosition.y;
        //transform.position = _platformJumpPositions[_arrivalPositionIndex];
    }

    private void JumpOntoDiffentFloor()
    {
        float x = transform.position.x + (_flipBoss.Orientation * BOUNCE_SPEED * Time.fixedDeltaTime) - _startPosition.x + (_isGoingUp ? 0 : deltaX);
        float _newHeight = -0.09467455f * x * (x  - 2 * deltaX) + (_isGoingUp? 0: deltaY) + _startPosition.y;
        transform.position = new Vector2(x + _startPosition.x - +(_isGoingUp ? 0 : deltaX), _newHeight);
    }

    private bool CheckIfBounceCompleted()
    {
        return transform.position.x < _arrivalPosition.x ^ !_flipBoss.IsFacingLeft;
    }
}