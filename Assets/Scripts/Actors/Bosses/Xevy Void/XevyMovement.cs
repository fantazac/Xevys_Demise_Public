using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class XevyMovement : MonoBehaviour
{
    public enum XevyMovementStatus
    {
        NONE,
        BOUNCING,
        RETREATING,
        CHARGING,
        FLEEING,
    }

    private const float FLEE_SPEED = 5;
    private const float BOUNCE_SPEED = 8;
    private const float STEP_BACK_SPEED = 1;
    private const float CHARGE_FORWARD_SPEED = 3;
    private const float BOUNCE_MODIFIER = -0.09467455f;
    private const float VERTICAL_DISTANCE_TO_REACH_PLATFORM = 4f;
    private const float HORIZONTAL_DISTANCE_TO_REACH_PLATFORM = 6.5f;

    private FlipBoss _flipBoss;
    private Rigidbody2D _rigidbody;
    private Vector2 _arrivalPosition;
    private Vector2 _startPosition;
    private Vector2[] _referencePoints;
    private Stack<XevyMovementCommand> _commandStack;
    private List<int>[] _referencePointsConnections;
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
        _commandStack = new Stack<XevyMovementCommand>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _flipBoss = GetComponent<FlipBoss>();
        _referencePoints = new Vector2[]
        {
            new Vector2(transform.position.x, transform.position.y + 2 * VERTICAL_DISTANCE_TO_REACH_PLATFORM),
            new Vector2(transform.position.x - 2 * HORIZONTAL_DISTANCE_TO_REACH_PLATFORM, transform.position.y),
            new Vector2(transform.position.x - HORIZONTAL_DISTANCE_TO_REACH_PLATFORM, transform.position.y + VERTICAL_DISTANCE_TO_REACH_PLATFORM),
            new Vector2(transform.position.x, transform.position.y),
            new Vector2(transform.position.x + HORIZONTAL_DISTANCE_TO_REACH_PLATFORM, transform.position.y + VERTICAL_DISTANCE_TO_REACH_PLATFORM),
            new Vector2(transform.position.x + 2 * HORIZONTAL_DISTANCE_TO_REACH_PLATFORM, transform.position.y),
        };
        _referencePointsConnections = new List<int>[6];
        for (int n = 0; n < _referencePointsConnections.Length; n++)
        {
            _referencePointsConnections[n] = new List<int>();
        }
        _referencePointsConnections[0].Add(2);
        _referencePointsConnections[0].Add(4);
        _referencePointsConnections[1].Add(2);
        _referencePointsConnections[2].Add(0);
        _referencePointsConnections[2].Add(1);
        _referencePointsConnections[2].Add(3);
        _referencePointsConnections[3].Add(2);
        _referencePointsConnections[3].Add(4);
        _referencePointsConnections[4].Add(0);
        _referencePointsConnections[4].Add(3);
        _referencePointsConnections[4].Add(5);
        _referencePointsConnections[5].Add(4);
        _startPosition = _referencePoints[_currentPositionIndex];
        GetComponent<XevyPlayerInteraction>().OnBossFlipped += OnBossFlipped;
    }

    public void MovementUpdate()
    {
        switch (MovementStatus)
        {
            case XevyMovementStatus.BOUNCING:
                UpdateWhenBouncing();
                break;
            case XevyMovementStatus.RETREATING:
                UpdateWhenRoaming(STEP_BACK_SPEED, -_flipBoss.Orientation);
                break;
            case XevyMovementStatus.FLEEING:
                UpdateWhenRoaming(FLEE_SPEED, _flipBoss.Orientation);
                break;
            case XevyMovementStatus.CHARGING:
                UpdateWhenRoaming(CHARGE_FORWARD_SPEED, _flipBoss.Orientation);
                break;
        }
    }

    public void StepBack()
    {
        _arrivalPosition = new Vector2(transform.position.x + (_flipBoss.Orientation * -2), transform.position.y);
        MovementStatus = XevyMovementStatus.RETREATING;
    }

    public void ChargeForward()
    {
        _arrivalPosition = new Vector2(transform.position.x + (_flipBoss.Orientation * 3), transform.position.y);
        MovementStatus = XevyMovementStatus.CHARGING;
    }

    public void BounceTowardsRandomPoint()
    {
        SelectBouncePointRandomly();
        if (Vector2.Distance(transform.position, _startPosition) > 0.5f)
        {
            _commandStack.Push(new XevyMovementCommand(_startPosition, _arrivalPosition, XevyMovementStatus.BOUNCING));
            _arrivalPosition = _startPosition;
            Flee();
        }
        else
        {
            Bounce();
        }
    }

    private void Bounce()
    {
        _rigidbody.isKinematic = true;
        _flipBoss.FlipTowardsSpecificPoint(_arrivalPosition);
        MovementStatus = XevyMovementStatus.BOUNCING;
    }

    private void Flee()
    {

        _flipBoss.FlipTowardsSpecificPoint(_arrivalPosition);
        MovementStatus = XevyMovementStatus.FLEEING;
    }

    public void FleeTowardsRandomPoint()
    {
        SelectFleePointRandomly();
        Flee();
    }

    private void SelectFleePointRandomly()
    {
        int indexClosestPosition = FindClosestPoint();
        _arrivalPositionIndex = indexClosestPosition;
        while (_arrivalPositionIndex == indexClosestPosition)
        {
            _arrivalPositionIndex = _rng.Next() % 3 * 2 + 1;
        }
        _arrivalPosition = _referencePoints[_arrivalPositionIndex];
    }

    private int FindClosestPoint()
    {
        float distanceToClosestPoint = float.MaxValue;
        int indexClosestPosition = -1;
        for (int n = 1; n < _referencePoints.Length; n++)
        {
            float distanceToSpecificPoint = Vector2.Distance(_referencePoints[n], transform.position);
            if (distanceToSpecificPoint < distanceToClosestPoint)
            {
                distanceToClosestPoint = distanceToSpecificPoint;
                indexClosestPosition = n;
            }
            //Because we must only consider ground positions, air positions (even numbers) are ignored.
            n++;
        }
        return indexClosestPosition;
    }

    private void SelectBouncePointRandomly()
    {
        int listNodesCount = _referencePointsConnections[_currentPositionIndex].Count;
        _arrivalPositionIndex = _referencePointsConnections[_currentPositionIndex][(listNodesCount == 1 ? 0 : _rng.Next() % listNodesCount)];
        _arrivalPosition = _referencePoints[_arrivalPositionIndex];
        _isGoingUp = (_startPosition.y < _arrivalPosition.y);
        deltaX = _arrivalPosition.x - _startPosition.x;
        deltaY = _arrivalPosition.y - _startPosition.y;
    }
    /* BEN COUNTER-CORRECTION
     * 
     * The optimal continuous action verb should be 走 (zǒu). However, it is in Chinese.
     * Unfortunately, in English, there is no specific word for "Move on the ground" that is indifferent of speed. 
     * "Walk" and "run" are too specific concepts (hypernyms) and "Move" is indifferent of locomotion method (hyponym);
     * it includes jumping, falling, running, flying, and more. Thus, "Roaming" has been taken.
     */
    private void UpdateWhenRoaming(float speed, int orientation)
    {
        transform.position = new Vector2(transform.position.x + speed * Time.fixedDeltaTime * orientation, transform.position.y);
        bool isFleeing = (speed == FLEE_SPEED);
        if (CheckIfMovementCompleted(orientation == _flipBoss.Orientation))
        {
            _startPosition = _referencePoints[FindClosestPoint()];
            if (isFleeing)
            {
                SetCurrentIndexToArrivalPosition();
            }
            UpdateMovementStatus();
        }
    }

    private void UpdateWhenBouncing()
    {
        float x = transform.position.x + (_flipBoss.Orientation * BOUNCE_SPEED * Time.fixedDeltaTime) - _startPosition.x + (_isGoingUp ? 0 : deltaX);
        float _newHeight = BOUNCE_MODIFIER * x * (x - 2 * deltaX) + (_isGoingUp ? 0 : deltaY) + _startPosition.y;
        transform.position = new Vector2(x + _startPosition.x - +(_isGoingUp ? 0 : deltaX), _newHeight);
        if (CheckIfMovementCompleted(true))
        {
            SetCurrentIndexToArrivalPosition();
            UpdateMovementStatus();
        }
    }

    private void SetCurrentIndexToArrivalPosition()
    {
        _currentPositionIndex = _arrivalPositionIndex;
        _startPosition = _arrivalPosition;
    }

    private bool CheckIfMovementCompleted(bool isGoingForward)
    {
        return ((transform.position.x < _arrivalPosition.x) ^ isGoingForward) ^ _flipBoss.IsFacingLeft;
    }

    public void OnBossFlipped()
    {
        if (MovementStatus == XevyMovementStatus.RETREATING)
        {
            _arrivalPosition.x -= 2 * (_arrivalPosition.x - _startPosition.x);
        }
    }

    private void UpdateMovementStatus()
    {
        if (_commandStack.Count == 0)
        {
            _rigidbody.isKinematic = false;
            MovementStatus = XevyMovementStatus.NONE;
        }
        else
        {
            XevyMovementCommand commandInStack = _commandStack.Pop();
            _startPosition = commandInStack._startPosition;
            _arrivalPosition = commandInStack._arrivalPosition;
            MovementStatus = commandInStack._status;
            _flipBoss.FlipTowardsSpecificPoint(_arrivalPosition);
            if (MovementStatus == XevyMovementStatus.BOUNCING)
            {
                _rigidbody.isKinematic = true;
            }
        }
    }
}