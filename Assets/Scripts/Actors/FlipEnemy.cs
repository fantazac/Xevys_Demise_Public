using UnityEngine;
using System.Collections;

public class FlipEnemy : MonoBehaviour
{
    [SerializeField]
    private bool _isFacingLeft;

    public bool IsFacingRight { get { return _isFacingLeft; } }
    public int Orientation { get { return (_isFacingLeft ? -1 : 1); } }

    public void CheckPlayerPosition()
    {
        if (GameObject.Find("Character").transform.position.x > transform.position.x)
        {
            if (_isFacingLeft)
            {
                Flip();
            }
        }
        else if (GameObject.Find("Character").transform.position.x < transform.position.x)
        {
            if (!_isFacingLeft)
            {
                Flip();
            }
        }
    }

    //In upcoming development, it would be wise to implement this method into a Component.
    private void Flip()
    {
        _isFacingLeft = !_isFacingLeft;
        transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);
    }
}

