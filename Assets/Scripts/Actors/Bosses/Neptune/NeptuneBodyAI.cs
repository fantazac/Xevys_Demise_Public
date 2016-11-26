using UnityEngine;

public class NeptuneBodyAI : NeptuneHeadAI
{
    private NeptuneHeadAI _neptuneHead;

	protected override void Start()
    {
        _neptuneHead = GameObject.Find(StaticObjects.GetFindTags().NeptuneHead).GetComponent<NeptuneHeadAI>();
        _horizontalLimit = _neptuneHead.HorizontalLimit;
        _verticalLimit = _neptuneHead.VerticalLimit;
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
