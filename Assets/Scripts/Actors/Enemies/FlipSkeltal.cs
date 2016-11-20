using UnityEngine;
using System.Collections;

/*
 * BEN_REVIEW
 * 
 * Il y a tellement de code en commentaires que je me demande si cela sert encore.
 */
public class FlipSkeltal : ActorOrientation
{
    private SkeltalBehaviour _skeltalBehavior;

    //public bool IsFacingRight { get; private set; }

    protected override void Start()
    {
        //IsFacingRight = false;

        _skeltalBehavior = GetComponent<SkeltalBehaviour>();
        _skeltalBehavior.OnSkeltalMovementStart += Flip;
    }

    /*private void Flip()
    {
        IsFacingRight = !IsFacingRight;
        transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);
    }*/
}
