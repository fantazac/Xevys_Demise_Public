using System.Collections;
using UnityEngine;

public class NeptuneBodyAI : NeptuneHeadAI
{

    private const float VERTICAL_AJUSTMENT = 0.75f;
    private const float HORIZONTAL_AJUSTMENT = 0.45f;
    [SerializeField]
    private GameObject _neptune;

    private NeptuneHeadAI _neptuneAI;

    public bool IsTail { get; set; }

    private int _index;
    private int _indexOddOrEvenModifier;
    private bool _isPointToReachOdd;

	protected override void Start()
    {
        _neptuneAI = GetComponentInParent<NeptuneHeadAI>();
        _horizontalLimit = _neptuneAI.HorizontalLimit;
        _verticalLimit = _neptuneAI.VerticalLimit;
        InitializePoints();
        RotateAndFlip();
        FlipTail();
        SetVerticalAjustment();
        StartCoroutine(UpdateWhenLiving());
    }
	
	private IEnumerator UpdateWhenLiving()
    {
        while (!_isDead)
        {
            yield return null;
            MoveInTrajectory();
        }
    }

    protected override void MoveInTrajectory()
    {
        if (_isPointToReachOdd)
        {
            transform.position += Vector3.up * _speed * Time.deltaTime;
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, _pointsToReach[_targetedPointIndex], _speed * Time.deltaTime);
        }
        if (CheckIfHasReachedTargetedPoint(_pointsToReach[_targetedPointIndex]))
        {
            _targetedPointIndex = (_targetedPointIndex + 1) % _pointsToReach.Length;
            _numberOfPointsReached++;
            _isPointToReachOdd = (_numberOfPointsReached % 2 == 1);
            RotateAndFlip();
        }
    }


    protected override bool CheckIfHasReachedTargetedPoint(Vector2 point)
    {
        if (_isPointToReachOdd)
        {
            return transform.position.y - ((_index % 2 == 0 ? 1 : -1) * VERTICAL_AJUSTMENT) >= _pointsToReach[_targetedPointIndex].y;
        }
        else
        {
            return 0 == Vector2.Distance(transform.position, _pointsToReach[_targetedPointIndex]);
        }
    }

    protected override void RotateAndFlip()
    {
        _bossOrientation.FlipTowardsSpecificPoint(_pointsToReach[_targetedPointIndex]);
        transform.rotation = Quaternion.identity;
        FlipTail();
        transform.Rotate(0, 0, RADIAN_TO_DEGREE * Mathf.Atan((_pointsToReach[_targetedPointIndex].y - transform.position.y) /
            (_pointsToReach[_targetedPointIndex].x - transform.position.x)));
        if (!_isPointToReachOdd)
        {
            SetDiagonalAjustment();
        }
        else
        {
            SetVerticalAjustment();
        }
    }

    private void SetVerticalAjustment()
    {
        transform.position += _indexOddOrEvenModifier * ( Vector3.up * VERTICAL_AJUSTMENT + Vector3.right * HORIZONTAL_AJUSTMENT);
    }

    private void SetDiagonalAjustment()
    {
        transform.position -= _indexOddOrEvenModifier * (Vector3.up * VERTICAL_AJUSTMENT + Vector3.right * HORIZONTAL_AJUSTMENT);
    }

    public void SetIndex(int index)
    {
        _index = index;
        _indexOddOrEvenModifier = (_index % 2 == 0 ? 1 : -1);
    }

    private void FlipTail()
    {
        if (IsTail)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
