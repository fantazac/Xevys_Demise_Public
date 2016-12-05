using UnityEngine;

public class BossOrientation : ActorOrientation
{
    GameObject _player;

    public delegate void OnBossFlippedHandler();
    public event OnBossFlippedHandler OnBossFlipped;

    private void Start()
    {
        _player = StaticObjects.GetPlayer();
    }

    public void FlipTowardsSpecificPoint(Vector2 point)
    {
        if (point.x > transform.position.x ^ IsFacingRight)
        {
            Flip();
            if (OnBossFlipped != null)
            {
                OnBossFlipped();
            }
        }
    }

    public void FlipTowardsPlayer()
    {
        FlipTowardsSpecificPoint(_player.transform.position);
    }
}
