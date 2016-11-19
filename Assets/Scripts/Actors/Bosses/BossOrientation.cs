using UnityEngine;

public class BossOrientation : ActorOrientation
{
    GameObject _player;

    protected override void Start()
    {
        _player = StaticObjects.GetPlayer();
    }

    public bool FlipTowardsSpecificPoint(Vector2 point)
    {
        if (point.x > transform.position.x)
        {
            if (!_isFacingRight)
            {
                Flip();
                return true;
            }
        }
        else if (point.x < transform.position.x)
        {
            if (_isFacingRight)
            {
                Flip();
                return true;
            }
        }
        return false;
    }

    public bool FlipTowardsPlayer()
    {
        return FlipTowardsSpecificPoint(_player.transform.position);
    }
}
