using UnityEngine;
using System.Collections;

public class NeptuneBodyAI : NeptuneHeadAI {

    NeptuneHeadAI _neptuneHead;

    private bool _isLastPart;

	// Use this for initialization
	protected override void Start ()
    {
        _isLastPart = false;
        _neptuneHead = GameObject.FindGameObjectWithTag("Neptune").GetComponent<NeptuneHeadAI>();
        _horizontalLimit = _neptuneHead.HorizontalLimit;
        _verticalLimit = _neptuneHead.VerticalLimit;
        InitializePoints();
        RotateAndFlip();
    }
	
	// Update is called once per frame
	private void Update()
    {
        MoveInTrajectory();
	}

    protected override void RotateAndFlip()
    {
        _flipBoss.CheckSpecificPointForFlip(_targetedPoint);
        transform.localScale = new Vector2(transform.localScale.x, -1 * transform.localScale.y);
        //GetComponent<SpriteRenderer>().flipY = !GetComponent<SpriteRenderer>().flipY;
        transform.rotation = Quaternion.identity;
        transform.Rotate(0, 0, RADIAN_TO_DEGREE * Mathf.Atan((_targetedPoint.y - transform.position.y) / (_targetedPoint.x - transform.position.x) + (_isLastPart ? 90 : 0)));
    }

    public void SetLastPart()
    {
        _isLastPart = true;
    }
}
