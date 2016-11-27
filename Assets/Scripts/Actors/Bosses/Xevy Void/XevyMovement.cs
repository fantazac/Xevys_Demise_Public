using UnityEngine;
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

    /* BEN COUNTER-CORRECTION
     * Lorsque la variable _numberNodes est sérialisée, l'erreur suivante apparaît:
     * error CS0150: A constant value is expected
     * Qu'est-ce qui est moins pire? Un jeu qui ne compile pas ou des 
     * points perdus parce que ces variables ne sont pas sérialisées?
     */
    private const int NUMBER_NODES = 6;
    private const int CENTRAL_NODE = 3;
    private const int NUMBER_GROUND_POSITIONS = 3;

    [SerializeField]
    private float _fleeSpeed = 5;

    [SerializeField]
    private float _bounceSpeed = 8;

    [SerializeField]
    private float _stepBackSpeed = 1;

    [SerializeField]
    private float _chargeSpeed = 3;

    [SerializeField]
    private float _chargeDistance = 3;

    [SerializeField]
    private float _stepBackDistance = 2;

    [SerializeField]
    private float _verticalDistanceToReachPlatform = 4;

    [SerializeField]
    private float _horizontalDistanceToReachPlatform = 6.5f;

    [SerializeField]
    private float _misalignmentMargin = 0.5f;

    [SerializeField]
    private float _bounceModifier = -0.09467455f;

    private Animator _animator;
    private BossOrientation _bossOrientation;
    private BossDirection _actorDirection;
    private Rigidbody2D _rigidbody;
    private Vector2 _arrivalPosition;
    private Vector2 _startPosition;
    private Vector2[] _referencePoints;
    private Stack<XevyMovementCommand> _commandStack;
    //Un tableau de listes d'entiers. Oui, oui, oui, tu as bien lu; un tableau de listes d'entiers!
    private List<int>[] _referencePointsConnections;
    private System.Random _random = new System.Random();

    public XevyMovementStatus MovementStatus { get; private set; }
    private bool _isGoingUp = true;
    private float _bounceApex;
    private int _currentPositionIndex;
    private int _arrivalPositionIndex;
    private float deltaX;
    private float deltaY;

    private void Start()
    {
        _currentPositionIndex = CENTRAL_NODE;
        MovementStatus = XevyMovementStatus.NONE;
        
        _commandStack = new Stack<XevyMovementCommand>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _actorDirection = GetComponent<BossDirection>();
        _bossOrientation = GetComponent<BossOrientation>();
        _bossOrientation.OnBossFlipped += OnBossFlipped;
        _referencePoints = new Vector2[NUMBER_NODES]
        {
            new Vector2(transform.position.x, transform.position.y + 2 * _verticalDistanceToReachPlatform),
            new Vector2(transform.position.x - 2 * _horizontalDistanceToReachPlatform, transform.position.y),
            new Vector2(transform.position.x - _horizontalDistanceToReachPlatform, transform.position.y + _verticalDistanceToReachPlatform),
            new Vector2(transform.position.x, transform.position.y),
            new Vector2(transform.position.x + _horizontalDistanceToReachPlatform, transform.position.y + _verticalDistanceToReachPlatform),
            new Vector2(transform.position.x + 2 * _horizontalDistanceToReachPlatform, transform.position.y),
        };
        _referencePointsConnections = new List<int>[NUMBER_NODES];
        for (int n = 0; n < _referencePointsConnections.Length; n++)
        {
            _referencePointsConnections[n] = new List<int>();
        }
        //Ajout des points adjacents à chacun des index de positions du tableau.
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
    }

    public void MovementUpdate()
    {
        switch (MovementStatus)
        {
            case XevyMovementStatus.BOUNCING:
                UpdateWhenBouncing();
                break;
            case XevyMovementStatus.RETREATING:
                UpdateWhenRoaming(_stepBackSpeed, -_bossOrientation.Orientation);
                break;
            case XevyMovementStatus.FLEEING:
                UpdateWhenRoaming(_fleeSpeed, _bossOrientation.Orientation);
                break;
            case XevyMovementStatus.CHARGING:
                UpdateWhenRoaming(_chargeSpeed, _bossOrientation.Orientation);
                break;
        }
    }

    public void StepBack()
    {
        _animator.SetInteger("State", 3);
        _actorDirection.IsGoingForward = false;
        _arrivalPosition = new Vector2(transform.position.x - (_bossOrientation.Orientation * _stepBackDistance), transform.position.y);
        MovementStatus = XevyMovementStatus.RETREATING;
    }

    public void ChargeForward()
    {
        _arrivalPosition = new Vector2(transform.position.x + (_bossOrientation.Orientation * _chargeDistance), transform.position.y);
        MovementStatus = XevyMovementStatus.CHARGING;
    }

    public void BounceTowardsRandomPoint()
    {
        SelectBouncePointRandomly();
        if (Vector2.Distance(transform.position, _startPosition) > _misalignmentMargin)
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

    public void FleeTowardsRandomPoint()
    {
        SelectFleePointRandomly();
        Flee();
    }

    private void Bounce()
    {
        _animator.SetInteger("State", 4);
        _rigidbody.isKinematic = true;
        _bossOrientation.FlipTowardsSpecificPoint(_arrivalPosition);
        MovementStatus = XevyMovementStatus.BOUNCING;
    }

    private void Flee()
    {
        _animator.SetInteger("State", 3);
        _bossOrientation.FlipTowardsSpecificPoint(_arrivalPosition);
        MovementStatus = XevyMovementStatus.FLEEING;
    }

    private void SelectFleePointRandomly()
    {
        int indexClosestPosition = FindClosestPoint();
        _arrivalPositionIndex = indexClosestPosition;
        while (_arrivalPositionIndex == indexClosestPosition)
        {
            //Les positions terrestres sont sur des nombres impairs, il faut donc ajuster le _random en conséquence.
            _arrivalPositionIndex = _random.Next() % NUMBER_GROUND_POSITIONS * 2 + 1;
        }
        _arrivalPosition = _referencePoints[_arrivalPositionIndex];
    }

    private void SelectBouncePointRandomly()
    {
        int listNodesCount = _referencePointsConnections[_currentPositionIndex].Count;
        _arrivalPositionIndex = _referencePointsConnections[_currentPositionIndex][(listNodesCount == 1 ? 0 : _random.Next() % listNodesCount)];
        _arrivalPosition = _referencePoints[_arrivalPositionIndex];
        _isGoingUp = (_startPosition.y < _arrivalPosition.y);
        deltaX = _arrivalPosition.x - _startPosition.x;
        deltaY = _arrivalPosition.y - _startPosition.y;
    }

    private int FindClosestPoint()
    {
        float distanceToClosestPoint = float.MaxValue;
        int indexClosestPosition = -1;
        //Les nombres pairs sont ignorés parce qu'ils sont attribués aux positions aériennes.
        for (int n = 1; n < _referencePoints.Length; n+=2)
        {
            float distanceToSpecificPoint = Vector2.Distance(_referencePoints[n], transform.position);
            if (distanceToSpecificPoint < distanceToClosestPoint)
            {
                distanceToClosestPoint = distanceToSpecificPoint;
                indexClosestPosition = n;
            }
        }
        return indexClosestPosition;
    }
    /* BEN COUNTER-CORRECTION
     * 
     * Le verbe idéal ici est 走 (zǒu). Malheureusement, c'est en Chinois.
     * Malheureusement, en Anglais, il n'y a pas de verbe pour un déplacement terrestre indifférent de la vitesse.
     * "Walk" et "Run" sont trop spécifiques et impliquent une vitesse (hypernymes) et "Move" est indifférent de 
     * la méthode de locomotion (hyponyme); cela inclut la marche, le saut, la nage, la course, le vol, le bond et plus encore.
     * Ainsi, "Roaming" a été choisi.
     */
    private void UpdateWhenRoaming(float speed, int orientation)
    {
        transform.position = new Vector2(transform.position.x + speed * Time.fixedDeltaTime * orientation, transform.position.y);
        if (CheckIfMovementCompleted())
        {
            _startPosition = _referencePoints[FindClosestPoint()];
            if (speed == _fleeSpeed)
            {
                SetCurrentIndexToArrivalPosition();
            }
            UpdateMovementStatus();
        }
    }

    private void UpdateWhenBouncing()
    {
        float x = transform.position.x + (_bossOrientation.Orientation * _bounceSpeed * Time.fixedDeltaTime) - _startPosition.x + (_isGoingUp ? 0 : deltaX);
        float _newHeight = _bounceModifier * x * (x - 2 * deltaX) + (_isGoingUp ? 0 : deltaY) + _startPosition.y;
        transform.position = new Vector2(x + _startPosition.x -(_isGoingUp ? 0 : deltaX), _newHeight);
        if (CheckIfMovementCompleted())
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

    private bool CheckIfMovementCompleted()
    {
        return ((transform.position.x > _arrivalPosition.x) ^ _actorDirection.IsGoingForward) ^ _bossOrientation.IsFacingRight;
    }

    private void OnBossFlipped()
    {
        if (MovementStatus == XevyMovementStatus.RETREATING)
        {
            _arrivalPosition.x -= _stepBackDistance * (_arrivalPosition.x - _startPosition.x);
        }
    }

    private void UpdateMovementStatus()
    {
        _actorDirection.IsGoingForward = true;
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
            _bossOrientation.FlipTowardsSpecificPoint(_arrivalPosition);
            if (MovementStatus == XevyMovementStatus.BOUNCING)
            {
                _animator.SetInteger("State", 4);
                _rigidbody.isKinematic = true;
            }
        }
    }
}