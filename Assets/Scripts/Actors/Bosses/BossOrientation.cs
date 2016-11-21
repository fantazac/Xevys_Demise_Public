using UnityEngine;

public class BossOrientation : ActorOrientation
{
    GameObject _player;

    private void Start()
    {
        _player = StaticObjects.GetPlayer();
    }

    public bool FlipTowardsSpecificPoint(Vector2 point)
    {
        if (point.x > transform.position.x && !IsFacingRight)
        {
            Flip();
            return true;
        }
        else if (point.x < transform.position.x && IsFacingRight)
        {
            Flip();
            return true;
        }
        return false;
    }

    public bool FlipTowardsPlayer()
    {
        return FlipTowardsSpecificPoint(_player.transform.position);
    }
}
