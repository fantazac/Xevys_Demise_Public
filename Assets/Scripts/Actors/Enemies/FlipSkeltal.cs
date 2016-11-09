using UnityEngine;
using System.Collections;

public class FlipSkeltal : MonoBehaviour
{
    private SkeltalBehaviour _skeltalBehavior;

    public bool IsFacingRight { get; private set; }

    private void Start()
    {
        IsFacingRight = false;

        _skeltalBehavior = GetComponent<SkeltalBehaviour>();
        _skeltalBehavior.OnSkeltalMovementStart += Flip;
    }

    private void Flip()
    {
        IsFacingRight = !IsFacingRight;
        transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);
    }
}
