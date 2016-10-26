using UnityEngine;
using System.Collections;

public class NeptuneBodyAI : NeptuneHeadAI {

    NeptuneHeadAI _neptuneHead;

	// Use this for initialization
	protected override void Start ()
    {
        _neptuneHead = GameObject.FindGameObjectWithTag("Neptune").GetComponent<NeptuneHeadAI>();
        _horizontalLimit = _neptuneHead.HorizontalLimit;
        _verticalLimit = _neptuneHead.VerticalLimit;
        InitializePoints();
    }
	
	// Update is called once per frame
	private void Update()
    {
        MoveInTrajectory();
        RotateAndFlip();
	}

    protected override void RotateAndFlip()
    {
        transform.Rotate(0, 0, RADIAN_TO_DEGREE * Mathf.Atan((_targetedPoint.y - transform.position.y) / (_targetedPoint.x - transform.position.x)) + (_flipBoss.IsFacingLeft ? 0 : 0));
    }
}
