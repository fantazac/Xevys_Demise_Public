using UnityEngine;
using System.Collections;

public class MoveJumpySkeltal : SkeltalBehaviour
{
    [SerializeField]
    protected float _maximumHeight = 2.5f;

    [SerializeField]
    protected float _maximumTimeInAir = 1.5f;

    protected float _timeInAir;

    protected override void Start()
    {
        base.Start();
        _timeInAir = 0;
    }

    protected override bool UpdateSkeltal()
    {
        _timeInAir += Time.deltaTime;
        float _newHeight;
        _newHeight = -_maximumHeight * (_timeInAir) * (_timeInAir - _maximumTimeInAir) + _initialPosition.y;
        transform.position = new Vector2(transform.position.x, Mathf.Max(_initialPosition.y, _newHeight));

        /* BEN_CORRECTION
         * 
         * Et si le jump fait qu'elle se retrouve plus haut ? Est-ce possible ?
         * 
         * Et si vous dites que c'est impossible, en êtes vous vraiment certain ?
         */
        if (transform.position.y <= _initialPosition.y)
        {
            _timeInAir = 0;
            return true;
        }

        return false;
    }
}
