using UnityEngine;

public class NeptuneBodyAI : NeptuneHeadAI
{
    [SerializeField]
    private GameObject _neptuneHead;

    private NeptuneHeadAI _headAi;

	protected override void Start()
    {
        _headAi = _neptuneHead.GetComponent<NeptuneHeadAI>();
        _horizontalLimit = _headAi.HorizontalLimit;
        _verticalLimit = _headAi.VerticalLimit;
        InitializePoints();
        RotateAndFlip();
    }
	
	private void FixedUpdate()
    {
        MoveInTrajectory();
	}

    protected override void RotateAndFlip()
    {
        _bossOrientation.FlipTowardsSpecificPoint(_pointsToReach[_targetedPointIndex]);
        transform.localScale = new Vector2(transform.localScale.x, -1 * transform.localScale.y);
        transform.rotation = Quaternion.identity;
        transform.Rotate(0, 0, RADIAN_TO_DEGREE * Mathf.Atan((_pointsToReach[_targetedPointIndex].y - transform.position.y) /
            (_pointsToReach[_targetedPointIndex].x - transform.position.x)));
    }
}
