using UnityEngine;

public class BossOrientation : ActorOrientation
{
    GameObject _player;

    protected override void Start()
    {
        _player = StaticObjects.GetPlayer();
    }

    /*
     * BEN_REVIEW
     * 
     * Le retour de cette méthode n'est jamais utilisé.
     * 
     * Il y a un principe en programmation qui dit "Tell, don't ask". Cela veut dire que lorsque l'on appelle
     * une méthode, on le fait soit :
     *   1. Pour demander de faire quelque chose. (Tell)
     *   2. Pour demander une information. (Ask)
     * 
     * Par contre, on essaie de ne jamais faire les deux en même temps.
     */
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
