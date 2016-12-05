using System.Collections;
using UnityEngine;

public class NeptuneBodyAI : NeptuneHeadAI
{
    [SerializeField]
    private GameObject _neptune;

    private NeptuneHeadAI _neptuneAI;

	protected override void Start()
    {
        _neptuneAI = GetComponentInParent<NeptuneHeadAI>();
        _horizontalLimit = _neptuneAI.HorizontalLimit;
        _verticalLimit = _neptuneAI.VerticalLimit;
        InitializePoints();
        RotateAndFlip();
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

    protected override void RotateAndFlip()
    {
        _bossOrientation.FlipTowardsSpecificPoint(_pointsToReach[_targetedPointIndex]);
        transform.localScale = new Vector2(transform.localScale.x, -1 * transform.localScale.y);
        transform.rotation = Quaternion.identity;
        transform.Rotate(0, 0, RADIAN_TO_DEGREE * Mathf.Atan((_pointsToReach[_targetedPointIndex].y - transform.position.y) /
            (_pointsToReach[_targetedPointIndex].x - transform.position.x)));
    }
}
