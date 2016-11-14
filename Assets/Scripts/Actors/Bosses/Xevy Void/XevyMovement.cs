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
        FLEEING,
    }
    private const float HORIZONTAL_DISTANCE_TO_REACH_PLATFORM = 6.5f;
    private const float VERTICAL_DISTANCE_TO_REACH_PLATFORM = 4f;
    private const float BOUNCE_SPEED = 8;
    private const float FLEE_SPEED = 5;
    private const float STEP_BACK_SPEED = 1;

    private FlipBoss _flipBoss;
    private Vector2 _arrivalPosition;
    private Vector2 _startPosition;
    private Vector2[] _referencePoints;
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
        if (MovementStatus == XevyMovementStatus.BOUNCING)
        {
            UpdateWhenBouncing();
        }
        else if (MovementStatus == XevyMovementStatus.RETREATING)
        {
            UpdateWhenRoaming(STEP_BACK_SPEED, -_flipBoss.Orientation);
        }
        else if (MovementStatus == XevyMovementStatus.FLEEING)
        {
            UpdateWhenRoaming(FLEE_SPEED, _flipBoss.Orientation);
        }
    }

    public void Bounce()
    {
        SelectBouncePointRandomly();
        _flipBoss.FlipTowardsSpecificPoint(_arrivalPosition);
        MovementStatus = XevyMovementStatus.BOUNCING;
    }

    public void StepBack()
    {
        _startPosition = transform.position;
        _arrivalPosition = new Vector2(transform.position.x + (_flipBoss.Orientation * -2), transform.position.y);
        MovementStatus = XevyMovementStatus.RETREATING;
    }

    public void Flee()
    {
        SelectFleePointRandomly();
        _flipBoss.FlipTowardsSpecificPoint(_arrivalPosition);
        MovementStatus = XevyMovementStatus.FLEEING;
    }

    private void SelectFleePointRandomly()
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
        _arrivalPositionIndex = indexClosestPosition;
        while (_arrivalPositionIndex == indexClosestPosition)
        {
            _arrivalPositionIndex = _rng.Next() % 3 * 2 + 1;
        }
        _arrivalPosition = _referencePoints[_arrivalPositionIndex];
    }

    private void SelectBouncePointRandomly()
    {
        int listNodesCount = _referencePointsConnections[_currentPositionIndex].Count;
        _arrivalPositionIndex = _referencePointsConnections[_currentPositionIndex][(listNodesCount == 1 ? 0 : _rng.Next() % listNodesCount)];
        _arrivalPosition = _referencePoints[_arrivalPositionIndex];
        _isGoingUp = (_startPosition.y < _arrivalPosition.y);
        _flipBoss.FlipTowardsSpecificPoint(_arrivalPosition);
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
        if (CheckIfMovementCompleted(orientation == _flipBoss.Orientation))
        {
            MovementStatus = XevyMovementStatus.NONE;
        }
    }

    private void UpdateWhenBouncing()
    {
        float x = transform.position.x + (_flipBoss.Orientation * BOUNCE_SPEED * Time.fixedDeltaTime) - _startPosition.x + (_isGoingUp ? 0 : deltaX);
        float _newHeight = -0.09467455f * x * (x - 2 * deltaX) + (_isGoingUp ? 0 : deltaY) + _startPosition.y;
        transform.position = new Vector2(x + _startPosition.x - +(_isGoingUp ? 0 : deltaX), _newHeight);
        if (CheckIfMovementCompleted(true))
        {
            _currentPositionIndex = _arrivalPositionIndex;
            _startPosition = _arrivalPosition;
            MovementStatus = XevyMovementStatus.NONE;
        }
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
}